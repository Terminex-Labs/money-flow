import { Routes } from "@angular/router";

export const OVERVIEW_ROUTES: Routes = [
    { path: 'dashboard', loadComponent: () => import('./pages/dashboard/dashboard.component').then(component => component.DashboardComponent) }
];