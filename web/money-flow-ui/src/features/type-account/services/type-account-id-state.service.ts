import { Injectable } from "@angular/core";

@Injectable({providedIn: 'root'})

export class TypeAccountIdStateService {
    private typeAccountId: string | null = null;
    private isEditForm: boolean = false;

    set(typeAccountId: string) {
        this.typeAccountId = typeAccountId;

        if (!typeAccountId)
            this.isEditForm = false;
        else
            this.isEditForm = true;
    }

    get() : string | null {
        return this.typeAccountId;
    }

    canEdit() : boolean {
        return this.isEditForm;
    }

    clear() {
        this.typeAccountId = null;
        this.isEditForm = false;
    }
}