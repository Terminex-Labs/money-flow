import { Component, EventEmitter, inject, model, Output, signal } from "@angular/core";
import { ComboBoxComponent, ComboBoxOption } from "../../../../shared/ui/combobox/combobox.component";
import { CurrencyService } from "../../services/currency.service";
import { TypeAccountService } from "../../services/type-account.service";
import { AccountService } from "../../services/account.service";
import { Account, AccountResponse, CreateAccountRequest, CurrencyResponse, TypeAccountResponse } from "../../models/account.model";
import { ToggleComponent } from "../../../../shared/ui/toggle/toggle.component";
import { Router } from "@angular/router";

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
    private readonly router = inject(Router);

    @Output() accountCreated = new EventEmitter<void>();

    data = signal<Account>({
        name: '',
        typeAccountId: '',
        currencyId: '',
        balance: 0,
        isActive: false
    });

    constructor() {     
        this.initializationAsync();   
    }

    // accountData = signal<AccountResponse[] | null>(null);
    isLoading = signal(false);
    currenciesData = signal<ComboBoxOption<string>[]>([]);
    typeAccountsData = signal<ComboBoxOption<string>[]>([]);

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

    async onCreate() {
        if (!this.data().name.trim() || !this.data().typeAccountId || !this.data().currencyId) {
            alert('Пожалуйста, заполните все обязательные поля');
            return;
        }

        this.isLoading.set(true);

        const request: CreateAccountRequest = {
            name: this.data().name.trim(),
            typeAccountId: this.data().typeAccountId!,
            currencyId: this.data().currencyId!,
            balance: Number(this.data().balance) || 0,
            isActive: this.data().isActive
        };
        
        await this.accountService.createAccount(request);
        this.router.navigate(['/account']);

        // try {
        //     await this.accountService.createAccount(request);
        //     this.accountCreated.emit(); // Уведомляем родителя об успехе
        // } catch (error) {
        //     console.error('Ошибка при создании счета:', error);
        //     alert('Не удалось создать счет. Попробуйте позже.');
        // } finally {
        //     this.isLoading.set(false);
        // }
    }
}