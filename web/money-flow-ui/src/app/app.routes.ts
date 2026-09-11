import { Routes } from '@angular/router';

export const routes: Routes = 
[
    { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
    { path: '', loadChildren: () => import('../features/overview/overview.routes').then(router => router.OVERVIEW_ROUTES) }
];
