import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product, CreateProductRequest, UpdateProductRequest, FindProductsResponse } from '../model/product';

@Injectable({ providedIn: 'root' })
export class ProductService {
  private http = inject(HttpClient);
  private baseUrl = '/api/products/';

  // GET product by ID
  getProduct(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.baseUrl}${id}`);
  }

  getAllProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.baseUrl);
  }

  findProducts(filter: string, pageNumber: number, pageSize: number): Observable<FindProductsResponse> {
    return this.http.get<FindProductsResponse>(this.baseUrl, {
            params: new HttpParams()
                .set('filter', filter)
                .set('pageNumber', pageNumber.toString())
                .set('pageSize', pageSize.toString())
        });
  }

  // POST create new product
  createProduct(product: CreateProductRequest): Observable<Product> {
    return this.http.post<Product>(this.baseUrl, {
      name: product.name,
      description: product.description,
      unitPrice: product.unitPrice,
      weight: product.weight,
    });
  }

  // PUT update existing product
  updateProduct(id: number, product: UpdateProductRequest): Observable<Product> {
    return this.http.put<Product>(`${this.baseUrl}${id}`, product);
  }

  // DELETE product by ID
  deleteProduct(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}${id}`);
  }
}
