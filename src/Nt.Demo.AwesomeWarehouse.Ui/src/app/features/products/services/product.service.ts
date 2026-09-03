import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product, CreateProductRequest, UpdateProductRequest, FindProductsResponse } from '../model/product';

@Injectable({ providedIn: 'root' })
export class ProductService {
  private http = inject(HttpClient);
  private baseUrl = '/api/products/';

  getProduct(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.baseUrl}${id}`);
  }

  findProducts(filter: string, pageNumber: number, pageSize: number): Observable<FindProductsResponse> {
    return this.http.get<FindProductsResponse>(this.baseUrl, {
            params: new HttpParams()
                .set('filter', filter)
                .set('pageNumber', pageNumber.toString())
                .set('pageSize', pageSize.toString())
        });
  }

  createProduct(product: CreateProductRequest): Observable<Product> {
    return this.http.post<Product>(this.baseUrl, product);
  }

  updateProduct(id: number, product: UpdateProductRequest): Observable<Product> {
    return this.http.put<Product>(`${this.baseUrl}${id}`, product);
  }

  deleteProduct(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}${id}`);
  }
}
