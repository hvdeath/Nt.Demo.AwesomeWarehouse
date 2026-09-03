import { Component, DestroyRef, ElementRef, inject, OnInit, ViewChild } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { CurrencyPipe, DatePipe, DecimalPipe, SlicePipe } from '@angular/common';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatDialog } from '@angular/material/dialog';
import { IncreaseStock } from '../../components/increase-stock/increase-stock';
import { DecreaseStock } from '../../components/decrease-stock/decrease-stock';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatTooltipModule } from '@angular/material/tooltip';
import { Product } from '../../model/product';
import { MatPaginator } from '@angular/material/paginator';
import { debounceTime, distinctUntilChanged, fromEvent, tap } from 'rxjs';
import { MatInputModule } from '@angular/material/input';
import { ProductStore } from '../../store/product.store';

@Component({
  imports: [
    RouterLink,
    RouterOutlet,
    MatTableModule,
    CurrencyPipe,
    MatButtonModule,
    MatDividerModule,
    MatIconModule,
    MatTooltipModule,
    SlicePipe,
    MatPaginator,
    MatInputModule,
    DecimalPipe,
    DatePipe,
  ],
  selector: 'app-stock-list',
  styleUrl: './stock-list.scss',
  templateUrl: './stock-list.html',
})
export class StockListComponent implements OnInit {
  readonly DEFAULT_PAGE_SIZE = 10;

  productService = inject(ProductService);
  private destroyRef = inject(DestroyRef);
  router = inject(Router);
  readonly dialog = inject(MatDialog);

  productStore = inject(ProductStore);

  columnsToDisplay = [
    'name',
    'description',
    'weight',
    'unitPrice',
    'unitPriceInEuros',
    'quantity',
    'modified',
    'actions',
  ];

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild('input') input!: ElementRef;

  ngOnInit(): void {
    this.productStore.findProducts({ filter: '', pageIndex: 0, pageSize: this.DEFAULT_PAGE_SIZE });
  }

  ngAfterViewInit() {
    fromEvent(this.input.nativeElement, 'keyup')
      .pipe(
        debounceTime(150),
        distinctUntilChanged(),
        tap(() => {
          this.paginator.pageIndex = 0;
          this.loadProductPage();
        }),
      )
      .subscribe();

    this.paginator.page.pipe(tap(() => this.loadProductPage())).subscribe();
  }

  loadProductPage() {
    this.productStore.findProducts({
      filter: this.input.nativeElement.value,
      pageIndex: this.paginator.pageIndex,
      pageSize: this.paginator.pageSize,
    });
  }

  onDelete(product: Product) {
    if (confirm(`Are you sure to delete '${product.name}'?`)) {
      this.productStore.deleteProduct(product.id);
    }
  }

  onDecrease(product: Product) {
    this.adjustStockDialog(product, DecreaseStock, (result: number) =>
      this.productStore.decreaseStock({ id: product.id, amount: result }),
    );
  }

  onIncrease(product: Product) {
    this.adjustStockDialog(product, IncreaseStock, (result: number) =>
      this.productStore.increaseStock({ id: product.id, amount: result }),
    );
  }

  private adjustStockDialog(
    product: Product,
    component: any,
    stockAdjustmentFn: (result: number) => void,
  ) {
    const dialogRef = this.dialog.open(component, {
      data: product,
    });

    dialogRef
      .afterClosed()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((result) => {
        console.log('The dialog was closed');
        if (result !== undefined) {
          stockAdjustmentFn(+result);
        }
      });
  }
}
