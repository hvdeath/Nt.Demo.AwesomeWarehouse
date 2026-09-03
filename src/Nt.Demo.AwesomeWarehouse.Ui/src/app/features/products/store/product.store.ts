import { patchState, signalStore, withMethods, withState } from '@ngrx/signals';
import { CreateProductRequest, Product } from '../model/product';
import { rxMethod } from '@ngrx/signals/rxjs-interop';
import { pipe, switchMap } from 'rxjs';
import { tapResponse } from '@ngrx/operators';
import { ProductService } from '../services/product.service';
import { inject } from '@angular/core';
import { GlobalStore } from '../../../core/store/global.store';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SnackbarErrorComponent } from '../../../shared/components/snackbar-error-component/snackbar-error.component';
import { Router } from '@angular/router';

type ProductStoreState = {
  products: Product[];
  selectedProduct: Product;
  count: number;
};

const initialState: ProductStoreState = {
  products: [],
  selectedProduct: undefined as unknown as Product,
  count: 0,
};

export const ProductStore = signalStore(
  { providedIn: 'root' },
  withState(initialState),
  withMethods(
    (
      store,
      productService = inject(ProductService),
      globalStore = inject(GlobalStore),
      snackBar = inject(MatSnackBar),
    ) => ({
      findProducts: rxMethod<{ filter: string; pageIndex: number; pageSize: number }>(
        pipe(
          switchMap(({ filter, pageIndex, pageSize }) => {
            globalStore.startLoading();
            return productService.findProducts(filter, pageIndex, pageSize).pipe(
              tapResponse({
                next: (response) => {
                  globalStore.stopLoading();
                  patchState(store, { products: response.data, count: response.count });
                },
                error: (err) => {
                  console.error('Error fetching products:', err);
                  snackBar.openFromComponent(SnackbarErrorComponent, {
                    verticalPosition: 'bottom',
                    horizontalPosition: 'right',
                    panelClass: 'error-snackbar',
                    data: 'Error fetching products',
                  });
                  globalStore.stopLoading();
                  patchState(store, { products: [], count: 0 });
                },
              }),
            );
          }),
        ),
      ),
    }),
  ),
  withMethods(
    (
      store,
      productService = inject(ProductService),
      globalStore = inject(GlobalStore),
      snackBar = inject(MatSnackBar),
      router = inject(Router),
    ) => ({
      clearSelectedProduct: () => {
        patchState(store, { selectedProduct: undefined as unknown as Product });
      },
      deleteProduct: rxMethod<number>(
        pipe(
          switchMap((productId) => {
            globalStore.startLoading();
            return productService.deleteProduct(productId).pipe(
              tapResponse({
                next: () => {
                  globalStore.stopLoading();
                  patchState(store, {
                    products: store.products().filter((p) => p.id !== productId),
                  });
                  router.navigate(['/products']);
                },
                error: (err) => {
                  console.error('Error deleting product:', err);
                  snackBar.openFromComponent(SnackbarErrorComponent, {
                    verticalPosition: 'bottom',
                    horizontalPosition: 'right',
                    panelClass: 'error-snackbar',
                    data: 'Error deleting product',
                  });
                  globalStore.stopLoading();
                },
              }),
            );
          }),
        ),
      ),
      getProduct: rxMethod<number>(
        pipe(
          switchMap((productId) => {
            globalStore.startLoading();
            return productService.getProduct(productId).pipe(
              tapResponse({
                next: (product) => {
                  globalStore.stopLoading();
                  patchState(store, {
                    selectedProduct: product,
                  });
                },
                error: (err) => {
                  console.error('Error getting product:', err);
                  snackBar.openFromComponent(SnackbarErrorComponent, {
                    verticalPosition: 'bottom',
                    horizontalPosition: 'right',
                    panelClass: 'error-snackbar',
                    data: 'Error getting product',
                  });
                  globalStore.stopLoading();
                },
              }),
            );
          }),
        ),
      ),
      createProduct: rxMethod<{ product: CreateProductRequest }>(
        pipe(
          switchMap(({ product }) => {
            globalStore.startLoading();
            return productService.createProduct(product).pipe(
              tapResponse({
                next: (response) => {
                  globalStore.stopLoading();
                  patchState(store, {
                    products: store.products().concat(response),
                    count: store.count() + 1,
                  });
                  router.navigate(['/products']);
                },
                error: (err) => {
                  console.error('Error updating product:', err);
                  snackBar.openFromComponent(SnackbarErrorComponent, {
                    verticalPosition: 'bottom',
                    horizontalPosition: 'right',
                    panelClass: 'error-snackbar',
                    data: 'Error updating product',
                  });
                  globalStore.stopLoading();
                },
              }),
            );
          }),
        ),
      ),
      updateProduct: rxMethod<{ id: number; product: Partial<Product> }>(
        pipe(
          switchMap(({ id, product }) => {
            globalStore.startLoading();
            return productService
              .updateProduct(id, {
                name: product.name!,
                description: product.description!,
                unitPrice: product.unitPrice!,
                weight: product.weight!,
                quantity: product.quantity!,
                version: product.version!,
              })
              .pipe(
                tapResponse({
                  next: (response) => {
                    globalStore.stopLoading();
                    patchState(store, {
                      products: store
                        .products()
                        .map((p) => (p.id === id ? { ...p, ...response } : p)),
                      selectedProduct: undefined as unknown as Product,
                    });
                    router.navigate(['/products']);
                  },
                  error: (err) => {
                    console.error('Error updating product:', err);
                    snackBar.openFromComponent(SnackbarErrorComponent, {
                      verticalPosition: 'bottom',
                      horizontalPosition: 'right',
                      panelClass: 'error-snackbar',
                      data: 'Error updating product',
                    });
                    globalStore.stopLoading();
                  },
                }),
              );
          }),
        ),
      ),
      increaseStock: rxMethod<{ id: number; amount: number }>(
        pipe(
          switchMap(({ id, amount }) => {
            globalStore.startLoading();
            const product = store.products().find((p) => p.id === id);
            if (!product) {
              globalStore.stopLoading();
              throw new Error(`Product with id ${id} not found`);
            }
            return productService
              .updateProduct(id, {
                name: product.name,
                description: product.description,
                quantity: product.quantity + amount,
                unitPrice: product.unitPrice,
                weight: product.weight,
                version: product.version,
              })
              .pipe(
                tapResponse({
                  next: (response) => {
                    globalStore.stopLoading();
                    patchState(store, {
                      products: store
                        .products()
                        .map((p) => (p.id === id ? { ...p, ...response } : p)),
                    });
                  },
                  error: (err) => {
                    console.error('Error increasing stock:', err);
                    snackBar.openFromComponent(SnackbarErrorComponent, {
                      verticalPosition: 'bottom',
                      horizontalPosition: 'right',
                      panelClass: 'error-snackbar',
                      data: 'Error increasing stock',
                    });
                    globalStore.stopLoading();
                  },
                }),
              );
          }),
        ),
      ),
      decreaseStock: rxMethod<{ id: number; amount: number }>(
        pipe(
          switchMap(({ id, amount }) => {
            globalStore.startLoading();
            const product = store.products().find((p) => p.id === id);
            if (!product) {
              globalStore.stopLoading();
              throw new Error(`Product with id ${id} not found`);
            }
            return productService
              .updateProduct(id, {
                name: product.name,
                description: product.description,
                quantity: product.quantity - amount,
                unitPrice: product.unitPrice,
                weight: product.weight,
                version: product.version,
              })
              .pipe(
                tapResponse({
                  next: (response) => {
                    globalStore.stopLoading();
                    patchState(store, {
                      products: store
                        .products()
                        .map((p) => (p.id === id ? { ...p, ...response } : p)),
                    });
                  },
                  error: (err) => {
                    globalStore.stopLoading();
                    snackBar.openFromComponent(SnackbarErrorComponent, {
                      verticalPosition: 'bottom',
                      horizontalPosition: 'right',
                      panelClass: 'error-snackbar',
                      data: 'Error decreasing stock',
                    });
                    console.error('Error decreasing stock:', err);
                  },
                }),
              );
          }),
        ),
      ),
    }),
  ),
);
