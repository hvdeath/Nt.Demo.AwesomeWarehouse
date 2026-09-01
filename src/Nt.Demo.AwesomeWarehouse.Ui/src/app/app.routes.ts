import { Routes } from '@angular/router';
import { ShellComponent } from './layout/shell-component/shell-component';
import { HomeComponent } from './features/global/pages/home/home';
import { StockListComponent } from './features/products/pages/stock-list/stock-list';
import { DashboardComponent } from './features/reports/pages/dashboard/dashboard';
import { EditProductComponent } from './features/products/components/edit-product/edit-product';

export const routes: Routes = [
  {
    path: '',
    component: ShellComponent,
    children: [
      {
        path: '',
        title: 'Home',
        component: HomeComponent,
      },
      {
        path: 'products',
        title: 'Stocks',
        children: [
          {
            path: '',
            title: 'Product list',
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
        ],
      },
      {
        path: 'reports',
        title: 'Reports',
        component: DashboardComponent,
      },
    //   {
    //     path: '**',
    //     redirectTo: '',
    //   },
    ],
  },
];
