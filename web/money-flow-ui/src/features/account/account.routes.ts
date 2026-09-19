import { Routes } from "@angular/router";

export const ACCOUNT_ROUTES: Routes = [
    { path: 'account', loadComponent: () => import('./pages/management/account-manage.component').then(component => component.AccountManageComponent) },
    { path: 'account/form', loadComponent: () => import('./pages/form/account-form.component').then(component => component.AccountFormComponent) }
];