import { Component, HostListener, inject, signal } from '@angular/core';
import { TypeAccountService } from '../../services/type-account.service';
import { TypeAccountIdStateService } from '../../services/type-account-id-state.service';
import { Router } from '@angular/router';
import { TypeAccountResponse } from '../../models/type-account.model';

@Component({
    selector: 'app-type-account-manage.component',
    templateUrl: 'type-account-manage.component.html',
    styleUrl: 'type-account-manage.component.scss',
    standalone: true
})

export class TypeAccountManageComponent { 
    private readonly typeAccountService = inject(TypeAccountService);
    private readonly typeAccountIdStateService = inject(TypeAccountIdStateService);
    private readonly router = inject(Router);

    typeAccounts = signal<TypeAccountResponse[] | null>([]);
    selectedTypeAccount = signal<TypeAccountResponse | null>(null);

    async ngOnInit() {
        await this.initializationAsync();
    }
    
    async initializationAsync() {
        this.typeAccounts.set(await this.typeAccountService.getAll());
    }

    onCreate() {
        this.typeAccountIdStateService.clear();
        this.router.navigate(['/admin/type/account/form']);
    }

    onEditBtn() {
        const currency = this.selectedTypeAccount();

        if (!currency)
            return;
        
        this.typeAccountIdStateService.set(currency.id);
        this.router.navigate(['/admin/type/account/form']);
    }
    
    async onDeleteBtn() {
        const currency = this.selectedTypeAccount();

        if (!currency)
            return;

        const typeAccountId = currency.id;

        this.typeAccountService.delete(currency.id);
        this.typeAccounts.update(typeAccounts => typeAccounts !== null ? typeAccounts.filter(typeAccount => typeAccount.id !== typeAccountId) : typeAccounts);
        this.selectedTypeAccount.set(null);
    }

    @HostListener('document:click', ['$event'])
    onResetSelectedClick(event: MouseEvent) {
        const target = event.target as HTMLElement;
        if (target.closest('.datagrid__row'))
            return;

        this.selectedTypeAccount.set(null);
    }
}
