import { Injectable } from "@angular/core";

@Injectable({providedIn: 'root'})

export class TypeTransactionIdStateService {
    private typeTransactionId: string | null = null;
    private isEditForm: boolean = false;

    set(typeTransactionId: string) {
        this.typeTransactionId = typeTransactionId;

        if (!typeTransactionId)
            this.isEditForm = false;
        else
            this.isEditForm = true;
    }

    get() : string | null {
        return this.typeTransactionId;
    }

    canEdit() : boolean {
        return this.isEditForm;
    }

    clear() {
        this.typeTransactionId = null;
        this.isEditForm = false;
    }
}