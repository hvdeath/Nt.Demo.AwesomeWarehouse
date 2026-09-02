import { Routes } from '@angular/router';
import { ShellComponent } from './layout/shell-component/shell-component';
import { HomeComponent } from './features/global/pages/home/home';
import { StockListComponent } from './features/products/pages/stock-list/stock-list';
import { DashboardComponent } from './features/reports/pages/dashboard/dashboard';
import { EditProductComponent } from './features/products/components/edit-product/edit-product';
import { productRoutes } from './features/products/product.routes';

export const routes: Routes = [
  {
    path: '',
    component: ShellComponent,
    children: [
      {
        path: '',
        title: 'Warehouse command center',
        component: HomeComponent,
      },
      {
        path: 'products',
        title: 'Stocks',
        children: productRoutes,
      },
      {
        path: 'reports',
        title: 'Dashboard',
        //loadComponent: () => import('./features/reports/pages/dashboard/dashboard').then((m) => m.DashboardComponent),
        component: DashboardComponent,
      },
      {
        path: '**',
        redirectTo: '',
      }
    ]
  },
];
