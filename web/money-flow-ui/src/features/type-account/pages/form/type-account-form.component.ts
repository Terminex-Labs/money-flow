import { Component, inject, signal } from '@angular/core';
import { TypeAccountService } from '../../services/type-account.service';
import { TypeAccountIdStateService } from '../../services/type-account-id-state.service';
import { Router } from '@angular/router';
import { CreateTypeAccountRequest, TypeAccountView, UpdateTypeAccountRequest } from '../../models/type-account.model';

@Component({
    selector: 'app-type-account-form.component',
    templateUrl: 'type-account-form.component.html',
    styleUrl: 'type-account-form.component.scss',
    standalone: true
})

export class TypeAccountFormComponent { 
    private readonly typeAccountService = inject(TypeAccountService);
    private readonly typeAccountIdStateService = inject(TypeAccountIdStateService);
    private readonly router = inject(Router);

    data = signal<TypeAccountView>({
        id: '',
        name: ''
    });
    
    isEditMode = signal<boolean>(false);
    isLoading = signal(false);

    async ngOnInit() {     
        if (this.typeAccountIdStateService.canEdit()) {
            this.isEditMode.set(true);
            await this.loadData(this.typeAccountIdStateService.get()!);
        }
    }

    async loadData(id: string) {
        const currency = await this.typeAccountService.getById(id);

        if (!currency)
            this.onCancellation();

        this.data.set({
            id: currency.id,
            name: currency.name
        });
    }

    onCancellation() {
        this.typeAccountIdStateService.clear();
        this.isLoading.set(false);
        this.router.navigate(['/admin/type/account']);
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
        const request: CreateTypeAccountRequest = {
            name: this.data().name
        }

        await this.typeAccountService.create(request);
    }

    private async onUpdate() {
        const request: UpdateTypeAccountRequest = {
            id: this.data().id,
            name: this.data().name
        }

        await this.typeAccountService.update(request);
    }

    getButtonText() : string {
        if (this.isLoading())
            return this.typeAccountIdStateService.canEdit() ? 'Обновление...' : 'Создание...';
        return this.typeAccountIdStateService.canEdit() ? 'Обновить' : 'Создать';
    }

    getTitle() : string {
        return this.typeAccountIdStateService.canEdit() ? 'Обновление типа счета' : 'Создание типа счета';
    }
}
