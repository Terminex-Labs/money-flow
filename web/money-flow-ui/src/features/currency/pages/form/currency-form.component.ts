import { Component, inject, signal } from '@angular/core';
import { CurrencyService } from '../../services/currency.service';
import { CurrencyIdStateService } from '../../services/currency-id-state.service';
import { Router } from '@angular/router';
import { CreateCurrencyRequest, CurrencyView, UpdateCurrencyRequest } from '../../models/currency.model';

@Component({
    selector: 'app-currency-form.component',
    templateUrl: 'currency-form.component.html',
    styleUrl: 'currency-form.component.scss',
    standalone: true
})

export class CurrencyFormComponent {
    private readonly currencyService = inject(CurrencyService);
    private readonly currencyIdStateService = inject(CurrencyIdStateService);
    private readonly router = inject(Router);

    data = signal<CurrencyView>({
        id: '',
        shortName: '',
        unicode: '',
        fullName: ''
    });

    isEditMode = signal<boolean>(false);
    isLoading = signal(false);

    async ngOnInit() {     
        if (this.currencyIdStateService.canEdit()) {
            this.isEditMode.set(true);
            await this.loadData(this.currencyIdStateService.get()!);
        }
    }

    async loadData(id: string) {
        const currency = await this.currencyService.getById(id);

        if (!currency)
            this.onCancellation();

        this.data.set({
            id: currency.id,
            shortName: currency.shortName,
            unicode: currency.unicode,
            fullName: currency.fullName
        });
    }

    onCancellation() {
        this.currencyIdStateService.clear();
        this.isLoading.set(false);
        this.router.navigate(['/admin/currency']);
    } 

    async onSave() {
        if (!this.data().shortName.trim() || !this.data().unicode || !this.data().fullName) {
            alert('Пожалуйста, заполните все обязательные поля');
            return;
        }

        this.isLoading.set(true);

        switch (this.isEditMode()) {
            case true:
                await this.onUpdate();
                break;

            case false:
                await this.onCreate();
                break;
        }
        
        this.onCancellation();
    }

    private async onCreate() {
        const request: CreateCurrencyRequest = {
            shortName: this.data().shortName,
            unicode: this.data().unicode,
            fullName: this.data().fullName
        }

        await this.currencyService.create(request);
    }

    private async onUpdate() {
        const request: UpdateCurrencyRequest = {
            id: this.data().id,
            shortName: this.data().shortName,
            unicode: this.data().unicode,
            fullName: this.data().fullName
        }

        await this.currencyService.update(request);
    }

    getButtonText() : string {
        if (this.isLoading())
            return this.currencyIdStateService.canEdit() ? 'Обновление...' : 'Создание...';
        return this.currencyIdStateService.canEdit() ? 'Обновить' : 'Создать';
    }

    getTitle() : string {
        return this.currencyIdStateService.canEdit() ? 'Обновление валюты' : 'Создание валюты';
    }
}
