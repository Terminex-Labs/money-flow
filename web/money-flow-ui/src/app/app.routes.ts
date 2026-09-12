import { Routes } from '@angular/router';
import { MainLayoutComponent } from '../core/layout/main-layout.component';

export const routes: Routes = 
[
    { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
    { path: '', loadComponent: () => MainLayoutComponent, loadChildren: () => import('../features/overview/overview.routes').then(router => router.OVERVIEW_ROUTES) }
];
