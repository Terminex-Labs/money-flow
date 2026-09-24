import { CanActivateFn, Router } from "@angular/router";
import { AdminService } from "../services/admin.service";
import { inject } from "@angular/core";

export const adminGuard: CanActivateFn = async () => {
    console.log('[adminGuard] вызван');
    const adminService = inject(AdminService);
    const router = inject(Router);
    
    const isAdmin = await adminService.check();
    console.log('[adminGuard] isAdmin =', isAdmin);

    if (!isAdmin)
        return router.createUrlTree(['/dashboard']);

    return true;
}