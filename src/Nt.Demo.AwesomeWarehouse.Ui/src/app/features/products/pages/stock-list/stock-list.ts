import { Component, DestroyRef, ElementRef, inject, OnInit, ViewChild } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { AsyncPipe, CurrencyPipe, SlicePipe } from '@angular/common';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatDialog } from '@angular/material/dialog';
import { IncreaseStock } from '../../components/increase-stock/increase-stock';
import { DecreaseStock } from '../../components/decrease-stock/decrease-stock';
import { MatTableModule } from '@angular/material/table';
import { MatIcon, MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatTooltipModule } from '@angular/material/tooltip';
import { Product } from '../../model/product';
import { ProductsDataSource } from './stock-list.datasource';
import { MatProgressSpinner } from '@angular/material/progress-spinner';
import { MatPaginator } from '@angular/material/paginator';
import { debounceTime, distinctUntilChanged, fromEvent, tap } from 'rxjs';
import { MatInputModule } from '@angular/material/input';

@Component({
  imports: [
    AsyncPipe,
    RouterLink,
    RouterOutlet,
    MatTableModule,
    CurrencyPipe,
    MatButtonModule,
    MatDividerModule,
    MatIconModule,
    MatTooltipModule,
    SlicePipe,
    MatProgressSpinner,
    MatPaginator,
    MatInputModule,
  ],
  selector: 'app-stock-list',
  styleUrl: './stock-list.scss',
  templateUrl: './stock-list.html',
})
export class StockListComponent implements OnInit {
  productService = inject(ProductService);
  private snackBar = inject(MatSnackBar);
  private destroyRef = inject(DestroyRef);
  router = inject(Router);
  readonly dialog = inject(MatDialog);

  products$ = this.productService.getAllProducts();

  public dataSource: ProductsDataSource = new ProductsDataSource(this.productService);
  columnsToDisplay = ['name', 'description', 'weight', 'unitPrice', 'unitPriceInEuros', 'quantity', 'actions'];

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild('input') input!: ElementRef;

  ngOnInit(): void {
    this.dataSource = new ProductsDataSource(this.productService);
    this.dataSource.loadProducts();
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
    this.dataSource.loadProducts(
      this.input.nativeElement.value,
      this.paginator.pageIndex,
      this.paginator.pageSize,
    );
  }

  onDelete(product: Product) {
    if (confirm(`Are you sure to delete '${product.name}'?`)) {
      this.productService
        .deleteProduct(product.id)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe({
          next: () => {
            this.snackBar.open('Saved successfully!');
            this.reloadCurrentRoute();
          },
          error: () => this.snackBar.open('Saving failed!'),
        });
    }
  }

  onDecrease(product: Product) {
    const dialogRef = this.dialog.open(DecreaseStock, {
      data: product,
    });

    dialogRef
      .afterClosed()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((result) => {
        console.log('The dialog was closed');
        if (result !== undefined) {
          this.productService
            .updateProduct(product.id, {
              name: product.name,
              description: product.description,
              unitPrice: product.unitPrice,
              weight: product.weight,
              quantity: product.quantity - +result,
              version: product.version,
            })
            .pipe(takeUntilDestroyed(this.destroyRef))
            .subscribe({
              next: () => {
                this.snackBar.open('Increased successfully!');
                this.reloadCurrentRoute();
              },
              error: () => this.snackBar.open('Saving failed!'),
            });
        }
      });
  }

  onIncrease(product: Product) {
    const dialogRef = this.dialog.open(IncreaseStock, {
      data: product,
    });

    dialogRef
      .afterClosed()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((result) => {
        console.log('The dialog was closed');
        if (result !== undefined) {
          this.productService
            .updateProduct(product.id, {
              name: product.name,
              description: product.description,
              unitPrice: product.unitPrice,
              weight: product.weight,
              quantity: product.quantity + +result,
              version: product.version,
            })
            .pipe(takeUntilDestroyed(this.destroyRef))
            .subscribe({
              next: () => {
                this.snackBar.open('Increased successfully!');
                this.reloadCurrentRoute();
              },
              error: () => this.snackBar.open('Saving failed!'),
            });
        }
      });
  }

  reloadCurrentRoute() {
    const currentUrl = this.router.url;
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate([currentUrl]);
    });
  }
}
