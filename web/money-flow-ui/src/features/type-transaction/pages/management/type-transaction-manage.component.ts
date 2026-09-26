import { Component, HostListener, inject, signal } from '@angular/core';
import { TypeTransactionResponse } from '../../models/type-transaction.model';
import { TypeTransactionIdStateService } from '../../services/type-transaction-id-state.service';
import { Router } from '@angular/router';
import { TypeTransactionService } from '../../services/type-transaction.service';

@Component({
    selector: 'app-type-transaction-manage.component',
    templateUrl: 'type-transaction-manage.component.html',
    styleUrl: 'type-transaction-manage.component.scss',
    standalone: true
})

export class TypeTransactionManageComponent {
    private readonly typeTransactionService = inject(TypeTransactionService);
    private readonly typeTransactionIdStateService = inject(TypeTransactionIdStateService);
    private readonly router = inject(Router);

    typeTransactions = signal<TypeTransactionResponse[] | null>([]);
    selectedTypeTransaction = signal<TypeTransactionResponse | null>(null);

    async ngOnInit() {
        await this.initializationAsync();
    }
    
    async initializationAsync() {
        this.typeTransactions.set(await this.typeTransactionService.getAll());
    }

    onCreate() {
        this.typeTransactionIdStateService.clear();
        this.router.navigate(['/admin/type/transaction/form']);
    }

    onEditBtn() {
        const currency = this.selectedTypeTransaction();

        if (!currency)
            return;
        
        this.typeTransactionIdStateService.set(currency.id);
        this.router.navigate(['/admin/type/transaction/form']);
    }
    
    async onDeleteBtn() {
        const currency = this.selectedTypeTransaction();

        if (!currency)
            return;

        const typeTransactionId = currency.id;

        this.typeTransactionService.delete(currency.id);
        this.typeTransactions.update(typeTransactions => typeTransactions !== null ? typeTransactions.filter(typeTransaction => typeTransaction.id !== typeTransactionId) : typeTransactions);
        this.selectedTypeTransaction.set(null);
    }

    @HostListener('document:click', ['$event'])
    onResetSelectedClick(event: MouseEvent) {
        const target = event.target as HTMLElement;
        if (target.closest('.datagrid__row'))
            return;

        this.selectedTypeTransaction.set(null);
    }
}
