import { Routes } from '@angular/router';
import { OVERVIEW_ROUTES } from '../features/overview/overview.routes';
import { ACCOUNT_ROUTES } from '../features/account/account.routes';
import { BASE_ROUTES } from '../features/base.routes';
import { CURRENCY_ROUTES } from '../features/currency/currency.routes';

export const routes: Routes = 
[
    { 
        path: '', 
        loadComponent: () => import('../core/layouts/main/main-layout.component').then(m => m.MainLayoutComponent),
        children: 
        [
            ...BASE_ROUTES,
            ...OVERVIEW_ROUTES,
            ...ACCOUNT_ROUTES,
            ...CURRENCY_ROUTES
        ]
    },
    { path: '**', redirectTo: 'dashboard' },
];
