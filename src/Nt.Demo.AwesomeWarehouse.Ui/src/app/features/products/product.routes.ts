import { Routes } from "@angular/router";
import { EditProductComponent } from "./components/edit-product/edit-product";
import { StockListComponent } from "./pages/stock-list/stock-list";

export const productRoutes: Routes = [
  {
    path: '',
    title: 'Inventory',
    component: StockListComponent,
  },
  {
    path: 'new',
    title: 'Create Product',
    component: EditProductComponent,
  },
  {
    path: 'edit/:productId',
    title: 'Edit Product',
    component: EditProductComponent,
  },
];
