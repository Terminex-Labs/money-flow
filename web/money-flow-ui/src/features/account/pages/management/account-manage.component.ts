import { Component, inject, signal } from "@angular/core";
import { Router } from "@angular/router";
import { AccountService } from "../../services/account.service";
import { AccountResponse } from "../../models/account.model";

@Component({
    selector: 'app-account-manage',
    templateUrl: 'account-manage.component.html',
    styleUrls: ['account-manage.component.scss'],
    standalone: true
})

export class AccountManageComponent {
    private readonly accountService = inject(AccountService);
    private router = inject(Router);

    accountData = signal<AccountResponse[] | null>([]);

    constructor() {
        this.initializationAsync();
    }
    
    async initializationAsync() {
        this.accountData.set(await this.accountService.getAllAccount());
    }

    navigate() {
        this.router.navigate(['/account/form']);
    }
}