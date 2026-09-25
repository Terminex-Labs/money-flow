import { Component, HostListener, inject, signal } from '@angular/core';
import { CurrencyService } from '../../services/currency.service';
import { Router } from '@angular/router';
import { CurrencyResponse } from '../../models/currency.model';
import { CurrencyIdStateService } from '../../services/currency-id-state.service';

@Component({
    selector: 'app-currency-manage.component',
    templateUrl: 'currency-manage.component.html',
    styleUrls: ['currency-manage.component.scss'],
    standalone: true
})

export class CurrencyManageComponent {
    private readonly currencyService = inject(CurrencyService);
    private readonly currencyIdStateService = inject(CurrencyIdStateService);
    private readonly router = inject(Router);

    currencies = signal<CurrencyResponse[] | null>([]);
    selectedCurrency = signal<CurrencyResponse | null>(null);

    async ngOnInit() {
        await this.initializationAsync();
    }
    
    async initializationAsync() {
        this.currencies.set(await this.currencyService.getAll());
    }

    onCreate() {
        this.currencyIdStateService.clear();
        this.router.navigate(['/admin/currency/form']);
    }

    onEditBtn() {
        const currency = this.selectedCurrency();

        if (!currency)
            return;
        
        this.currencyIdStateService.set(currency.id);
        this.router.navigate(['/admin/currency/form']);
    }
    
    async onDeleteBtn() {
        const currency = this.selectedCurrency();

        if (!currency)
            return;

        const currencyId = currency.id;

        this.currencyService.delete(currency.id);
        this.currencies.update(currencies => currencies !== null ? currencies.filter(currency => currency.id !== currencyId) : currencies);
        this.selectedCurrency.set(null);
    }

    @HostListener('document:click', ['$event'])
    onResetSelectedClick(event: MouseEvent) {
        const target = event.target as HTMLElement;
        if (target.closest('.datagrid__row'))
            return;

        this.selectedCurrency.set(null);
    }
}