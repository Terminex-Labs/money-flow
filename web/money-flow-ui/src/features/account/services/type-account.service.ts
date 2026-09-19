import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { TypeAccountResponse } from "../models/account.model";
import { firstValueFrom } from "rxjs";

@Injectable({providedIn: 'root'})

export class TypeAccountService {
    private readonly httpClient = inject(HttpClient);

    async getAllTypeAccount() : Promise<TypeAccountResponse[]> {
        return await firstValueFrom(this.httpClient.get<TypeAccountResponse[]>("api/v1/type/account"));
    }
}