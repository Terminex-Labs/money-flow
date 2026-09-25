import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { firstValueFrom } from "rxjs";
import { CreatedTypeAccountResponse, CreateTypeAccountRequest, TypeAccountResponse, UpdateTypeAccountRequest } from "../models/type-account.model";

@Injectable({providedIn: 'root'})

export class TypeAccountService {
    private readonly httpClient = inject(HttpClient);
    private readonly url = 'api/v1/type/account';

    async create(request: CreateTypeAccountRequest) : Promise<CreatedTypeAccountResponse> {
        return await firstValueFrom(this.httpClient.post<CreatedTypeAccountResponse>(this.url, request));
    }

    async getAll() : Promise<TypeAccountResponse[]> {
        return await firstValueFrom(this.httpClient.get<TypeAccountResponse[]>(this.url));
    }

    async getById(id: string) : Promise<TypeAccountResponse> {
        return await firstValueFrom(this.httpClient.get<TypeAccountResponse>(`${this.url}/${id}`));
    }

    async update(request: UpdateTypeAccountRequest) : Promise<void> {
        await firstValueFrom(this.httpClient.patch(this.url, request));
    }

    async delete(id: string) : Promise<void> {
        await firstValueFrom(this.httpClient.delete(`${this.url}/${id}`));
    }
}