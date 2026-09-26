import { Component, inject, signal } from '@angular/core';
import { TypeTransactionService } from '../../services/type-transaction.service';
import { TypeTransactionIdStateService } from '../../services/type-transaction-id-state.service';
import { Router } from '@angular/router';
import { TypeTransactionView, CreateTypeTransactionRequest, UpdateTypeTransactionRequest } from '../../models/type-transaction.model';

@Component({
  selector: 'app-type-transaction-form.component',
  templateUrl: 'type-transaction-form.component.html',
  styleUrl: 'type-transaction-form.component.scss',
  standalone: true
})
export class TypeTransactionFormComponent {
    private readonly typeTransactionService = inject(TypeTransactionService);
    private readonly typeTransactionIdStateService = inject(TypeTransactionIdStateService);
    private readonly router = inject(Router);

    data = signal<TypeTransactionView>({
        id: '',
        name: ''
    });
    
    isEditMode = signal<boolean>(false);
    isLoading = signal(false);

    async ngOnInit() {     
        if (this.typeTransactionIdStateService.canEdit()) {
            this.isEditMode.set(true);
            await this.loadData(this.typeTransactionIdStateService.get()!);
        }
    }

    async loadData(id: string) {
        const currency = await this.typeTransactionService.getById(id);

        if (!currency)
            this.onCancellation();

        this.data.set({
            id: currency.id,
            name: currency.name
        });
    }

    onCancellation() {
        this.typeTransactionIdStateService.clear();
        this.isLoading.set(false);
        this.router.navigate(['/admin/type/transaction']);
    } 

    async onSave() {
        if (!this.data().name.trim()) {
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
        const request: CreateTypeTransactionRequest = {
            name: this.data().name
        }

        await this.typeTransactionService.create(request);
    }

    private async onUpdate() {
        const request: UpdateTypeTransactionRequest = {
            id: this.data().id,
            name: this.data().name
        }

        await this.typeTransactionService.update(request);
    }

    getButtonText() : string {
        if (this.isLoading())
            return this.typeTransactionIdStateService.canEdit() ? 'Обновление...' : 'Создание...';
        return this.typeTransactionIdStateService.canEdit() ? 'Обновить' : 'Создать';
    }

    getTitle() : string {
        return this.typeTransactionIdStateService.canEdit() ? 'Обновление типа транзакции' : 'Создание типа транзакции';
    }
}
