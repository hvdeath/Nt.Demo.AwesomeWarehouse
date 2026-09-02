import { CollectionViewer } from '@angular/cdk/collections';
import { DataSource } from '@angular/cdk/table';
import { BehaviorSubject, Observable, catchError, of, finalize } from 'rxjs';
import { Product } from '../../model/product';
import { ProductService } from '../../services/product.service';
import { LoaderService } from '../../../../core/services/loader.service';

export class ProductsDataSource implements DataSource<Product> {
  private porductsSubject = new BehaviorSubject<Product[]>([]);
  private countSubject = new BehaviorSubject<number>(0);

  public countSubject$ = this.countSubject.asObservable();

  constructor(
    private productService: ProductService,
    private loaderService: LoaderService,
  ) {}

  connect(collectionViewer: CollectionViewer): Observable<Product[]> {
    return this.porductsSubject.asObservable();
  }

  disconnect(collectionViewer: CollectionViewer): void {
    this.porductsSubject.complete();
    this.countSubject.complete();
  }

  loadProducts(filter = '', pageIndex = 0, pageSize = 10) {
    this.loaderService.show();

    this.productService
      .findProducts(filter, pageIndex, pageSize)
      .pipe(
        catchError(() => of()),
        finalize(() => this.loaderService.hide()),
      )
      .subscribe((r) => {
        this.porductsSubject.next(r.data);
        this.countSubject.next(r.count);
      });
  }
}
