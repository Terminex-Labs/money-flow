import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { firstValueFrom } from "rxjs";
import { CreateTypeTransactionRequest, CreatedTypeTransactionResponse, TypeTransactionResponse, UpdateTypeTransactionRequest } from "../models/type-transaction.model";

@Injectable({providedIn: 'root'})

export class TypeTransactionService {
    private readonly httpClient = inject(HttpClient);
    private readonly url = 'api/v1/type/transaction';
    
    async create(request: CreateTypeTransactionRequest) : Promise<CreatedTypeTransactionResponse> {
        return await firstValueFrom(this.httpClient.post<CreatedTypeTransactionResponse>(this.url, request));
    }

    async getAll() : Promise<TypeTransactionResponse[]> {
        return await firstValueFrom(this.httpClient.get<TypeTransactionResponse[]>(this.url));
    }

    async getById(id: string) : Promise<TypeTransactionResponse> {
        return await firstValueFrom(this.httpClient.get<TypeTransactionResponse>(`${this.url}/${id}`));
    }

    async update(request: UpdateTypeTransactionRequest) : Promise<void> {
        await firstValueFrom(this.httpClient.patch(this.url, request));
    }

    async delete(id: string) : Promise<void> {
        await firstValueFrom(this.httpClient.delete(`${this.url}/${id}`));
    }
}