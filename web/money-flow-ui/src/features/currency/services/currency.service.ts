import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { CreateCurrencyRequest, CreatedCurrencyResponse, CurrencyResponse, UpdateCurrencyRequest } from "../models/currency.model";
import { firstValueFrom } from "rxjs";

@Injectable({providedIn: 'root'})

export class CurrencyService {
    private readonly httpClient = inject(HttpClient);
    private readonly url = 'api/v1/currency';

    async create(request: CreateCurrencyRequest) : Promise<CreatedCurrencyResponse> {
        return await firstValueFrom(this.httpClient.post<CreatedCurrencyResponse>(this.url, request));
    }

    async getAll() : Promise<CurrencyResponse[]> {
        return await firstValueFrom(this.httpClient.get<CurrencyResponse[]>(this.url));
    }

    async getById(id: string) : Promise<CurrencyResponse> {
        return await firstValueFrom(this.httpClient.get<CurrencyResponse>(`${this.url}/${id}`));
    }

    async update(request: UpdateCurrencyRequest) : Promise<void> {
        await firstValueFrom(this.httpClient.patch(this.url, request));
    }

    async delete(id: string) : Promise<void> {
        await firstValueFrom(this.httpClient.delete(`${this.url}/${id}`));
    }
}