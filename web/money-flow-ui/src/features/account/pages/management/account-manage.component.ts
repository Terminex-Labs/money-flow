import { Component, HostListener, inject, signal } from "@angular/core";
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

    accounts = signal<AccountResponse[] | null>([]);
    selectedAccount = signal<AccountResponse | null>(null);

    constructor() {
        this.initializationAsync();
    }
    
    async initializationAsync() {
        this.accounts.set(await this.accountService.getAllAccount());
    }

    navigate() {
        this.router.navigate(['/account/form']);
    }

    onEditBtn() {

    }

    async onFreezeBtn() {
        const account = this.selectedAccount();

        if (!account)
            return;

        const accountId = account.id;

        this.accountService.freeze(account.id);
        this.accounts.update(accounts => accounts !== null ? accounts.map(account => account.id === accountId ? ({ ...account, isActive: false }) : account)  : accounts);
        this.selectedAccount.set(null);
    }

    async onUnfreezeBtn() {
        const account = this.selectedAccount();

        if (!account)
            return;

        const accountId = account.id;

        this.accountService.unfreeze(account.id);
        this.accounts.update(accounts => accounts !== null ? accounts.map(account => account.id === accountId ? ({ ...account, isActive: true }) : account)  : accounts);
        this.selectedAccount.set(null);
    }
    
    async onDeleteBtn() {
        const account = this.selectedAccount();

        if (!account)
            return;

        const accountId = account.id;

        this.accountService.delete(account.id);
        this.accounts.update(accounts => accounts !== null ? accounts.filter(account => account.id !== accountId) : accounts);
        this.selectedAccount.set(null);
    }

    @HostListener('document:click', ['$event'])
    onResetSelectedClick(event: MouseEvent) {
        const target = event.target as HTMLElement;
        if (target.closest('.account-card'))
            return;

        this.selectedAccount.set(null);
    }
}