import { Component, EventEmitter, inject, model, Output, signal } from "@angular/core";
import { ComboBoxComponent, ComboBoxOption } from "../../../../shared/ui/combobox/combobox.component";
import { CurrencyService } from "../../services/currency.service";
import { TypeAccountService } from "../../services/type-account.service";
import { AccountService } from "../../services/account.service";
import { AccountView, AccountResponse, CreateAccountRequest, CurrencyResponse, TypeAccountResponse, UpdateAccountRequest } from "../../models/account.model";
import { ToggleComponent } from "../../../../shared/ui/toggle/toggle.component";
import { Router } from "@angular/router";
import { AccountIdStateService } from "../../services/account-id-state.service";

@Component({
    selector: 'app-account-form',
    templateUrl: 'account-form.component.html',
    styleUrls: ['account-form.component.scss'],
    standalone: true,
    imports: [ComboBoxComponent, ToggleComponent]
})

export class AccountFormComponent {
    private readonly currencyService = inject(CurrencyService);
    private readonly typeAccountService = inject(TypeAccountService);
    private readonly accountService = inject(AccountService);
    private readonly accountIdStateService = inject(AccountIdStateService);
    private readonly router = inject(Router);

    @Output() accountCreated = new EventEmitter<void>();

    data = signal<AccountView>({
        id: '',
        name: '',
        typeAccountId: '',
        currencyId: '',
        balance: 0,
        isActive: false
    });

    async ngOnInit() {     
        await this.initializationAsync();

        if (this.accountIdStateService.canEdit()) {
            this.isEditMode.set(true);
            await this.loadAccountData(this.accountIdStateService.get()!);
        }
    }

    // accountData = signal<AccountResponse[] | null>(null);
    isEditMode = signal<boolean>(false);
    isLoading = signal(false);
    currenciesData = signal<ComboBoxOption<string>[]>([]);
    selectedCurrency = signal<CurrencyResponse | null>(null);
    typeAccountsData = signal<ComboBoxOption<string>[]>([]);
    selectedTypeAccount = signal<TypeAccountResponse | null>(null);

    async initializationAsync() {
        const currencyRaw = await this.currencyService.getAllCurrency();
        const typeAccountRaw = await this.typeAccountService.getAllTypeAccount();

        this.currenciesData.set(currencyRaw.map(currency => ({
            value: currency.id,
            label: `${currency.shortName} - ${currency.unicode} • ${currency.fullName}`
        })));

        this.typeAccountsData.set(typeAccountRaw.map(typeAccount => ({
            value: typeAccount.id,
            label: typeAccount.name
        })));
    }

    async loadAccountData(id: string) {
        const account = await this.accountService.getByIdAccount(id);

        if (!account)
            this.onCancellation();
        
        this.data.set({
            id: this.accountIdStateService.get(),
            name: account.name,
            balance: account.balance,
            isActive: account.isActive,
            currencyId: account.currency?.id ?? '', 
            typeAccountId: account.typeAccount?.id ?? ''
        });
    }

    onCancellation() {
        this.router.navigate(['/account']);
    } 

    async onSave() {
        if (!this.data().name.trim() || !this.data().typeAccountId || !this.data().currencyId) {
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
        
        this.accountIdStateService.clear();
        this.isLoading.set(false);
        this.router.navigate(['/account']);
    }

    private async onCreate() {
        const request: CreateAccountRequest = {
            name: this.data().name.trim(),
            typeAccountId: this.data().typeAccountId!,
            currencyId: this.data().currencyId!,
            balance: Number(this.data().balance) || 0,
            isActive: this.data().isActive
        };
        
        await this.accountService.createAccount(request);
    }

    private async onUpdate() {
        const updateRequest: UpdateAccountRequest = {
            id: this.data().id!,
            name: this.data().name.trim(),
            typeAccountId: this.data().typeAccountId!,
            currencyId: this.data().currencyId!,
            balance: Number(this.data().balance) || 0,
            isActive: this.data().isActive
        };
        
        await this.accountService.updateAccount(updateRequest);
    }

    getButtonText() : string {
        if (this.isLoading())
            return this.accountIdStateService.canEdit() ? 'Обновление...' : 'Создание...';
        return this.accountIdStateService.canEdit() ? 'Обновить' : 'Создать';
    }

    getTitle() : string {
        return this.accountIdStateService.canEdit() ? 'Обновление счёта' : 'Создание счёта';
    }
}