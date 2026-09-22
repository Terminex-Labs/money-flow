import { Injectable } from "@angular/core";

@Injectable({providedIn: 'root'})

export class AccountIdStateService {
    private accountId: string | null = null;
    private isEditForm: boolean = false;

    set(accountId: string) {
        this.accountId = accountId;

        if (!accountId)
            this.isEditForm = false;
        else
            this.isEditForm = true;
    }

    get() : string | null {
        return this.accountId;
    }

    canEdit() : boolean {
        return this.isEditForm;
    }

    clear() {
        this.accountId = null;
        this.isEditForm = false;
    }
}