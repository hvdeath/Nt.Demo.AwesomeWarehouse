export interface Product {
  id: number;
  name: string;
  description: string;
  unitPrice: number;
  weight: number;
  quantity: number;
  created: Date;
  version: string;
}

export interface CreateProductRequest {
  name: string;
  description: string;
  unitPrice: number;
  weight: number;
  quantity: number;
}

export interface UpdateProductRequest {
  name: string;
  description: string;
  unitPrice: number;
  weight: number;
  quantity: number;
  version: string;
}

export interface FindProductsResponse{
  count:number;
  data: Product[];
}
