import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { AccountResponse, CreateAccountRequest, CreatedAccountResponse, UpdateAccountNameRequest } from "../models/account.model";
import { firstValueFrom } from "rxjs";

@Injectable({providedIn: 'root'})

export class AccountService {
    private readonly httpClient = inject(HttpClient);
    private readonly url = "api/v1/account";

    async createAccount(request: CreateAccountRequest) : Promise<CreatedAccountResponse> {
        return await firstValueFrom(this.httpClient.post<CreatedAccountResponse>(this.url, request));
    }

    async getAllAccount() : Promise<AccountResponse[]> {
        return await firstValueFrom(this.httpClient.get<AccountResponse[]>(this.url));
    }

    async updateAccountName(request: UpdateAccountNameRequest) : Promise<void> {
        await firstValueFrom(this.httpClient.patch(`${this.url}/name`, request))
    }

    async freeze(id: string) : Promise<void> {
        await firstValueFrom(this.httpClient.patch(`${this.url}/freeze/${id}`, {}));
    }

    async unfreeze(id: string) : Promise<void> {
        await firstValueFrom(this.httpClient.patch(`${this.url}/unfreeze/${id}`, {}));
    }

    async delete(id: string) : Promise<void> {
        await firstValueFrom(this.httpClient.delete(`${this.url}/${id}`));
    }
}