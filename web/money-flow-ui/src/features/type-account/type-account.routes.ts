import { Routes } from "@angular/router";
import { adminGuard } from "../../core/guards/admin.guard";

export const TYPE_ACCOUNT_ROUTER: Routes = [
    { path: 'admin/type/account', loadComponent: () => import('./pages/management/type-account-manage.component').then(c => c.TypeAccountManageComponent), runGuardsAndResolvers: 'always', canActivate: [adminGuard] },
    { path: 'admin/type/account/form', loadComponent: () => import('./pages/form/type-account-form.component').then(c => c.TypeAccountFormComponent), runGuardsAndResolvers: 'always', canActivate: [adminGuard] }
]