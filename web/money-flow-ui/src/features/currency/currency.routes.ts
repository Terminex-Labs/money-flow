import { Routes } from "@angular/router";
import { adminGuard } from "../../core/guards/admin.guard";

export const CURRENCY_ROUTES: Routes = [
    { path: 'admin/currency', loadComponent: () => import('./pages/management/currency-manage.component').then(c => c.CurrencyManageComponent), runGuardsAndResolvers: 'always', canActivate: [adminGuard] },
    { path: 'admin/currency/form', loadComponent: () => import('./pages/form/currency-form.component').then(c => c.CurrencyFormComponent), runGuardsAndResolvers: 'always', canActivate: [adminGuard] }
];