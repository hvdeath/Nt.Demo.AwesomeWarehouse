import { Routes } from '@angular/router';
import { ShellComponent } from './layout/shell-component/shell-component';
import { HomeComponent } from './features/global/pages/home/home';
import { DashboardComponent } from './features/reports/pages/dashboard/dashboard';
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
        //loadChildren: () => import('./features/products/product.routes').then((m) => m.productRoutes),
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
