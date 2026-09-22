import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { AccountResponse, CreateAccountRequest, CreatedAccountResponse, UpdateAccountRequest } from "../models/account.model";
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

    async getByIdAccount(id: string) : Promise<AccountResponse> {
        return await firstValueFrom(this.httpClient.get<AccountResponse>(`${this.url}/${id}`));
    }

    async updateAccount(request: UpdateAccountRequest) : Promise<void> {
        await firstValueFrom(this.httpClient.patch(this.url, request))
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