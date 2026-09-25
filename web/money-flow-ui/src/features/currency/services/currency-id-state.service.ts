import { Injectable } from "@angular/core";

@Injectable({providedIn: 'root'})

export class CurrencyIdStateService {
    private currencyId: string | null = null;
    private isEditForm: boolean = false;

    set(currencyId: string) {
        this.currencyId = currencyId;

        if (!currencyId)
            this.isEditForm = false;
        else
            this.isEditForm = true;
    }

    get() : string | null {
        return this.currencyId;
    }

    canEdit() : boolean {
        return this.isEditForm;
    }

    clear() {
        this.currencyId = null;
        this.isEditForm = false;
    }
}