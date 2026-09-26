import { Routes } from "@angular/router";
import { adminGuard } from "../../core/guards/admin.guard";

export const TYPE_TRANSACTION_ROUTER: Routes = [
    { path: 'admin/type/transaction', loadComponent: () => import('./pages/management/type-transaction-manage.component').then(c => c.TypeTransactionManageComponent), runGuardsAndResolvers: 'always', canActivate: [adminGuard] },
    { path: 'admin/type/transaction/form', loadComponent: () => import('./pages/form/type-transaction-form.component').then(c => c.TypeTransactionFormComponent), runGuardsAndResolvers: 'always', canActivate: [adminGuard] }
]