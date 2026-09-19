import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { CurrencyResponse } from "../models/account.model";
import { firstValueFrom } from "rxjs";

@Injectable({providedIn: 'root'})

export class CurrencyService {
    private readonly httpClient = inject(HttpClient);

    async getAllCurrency() : Promise<CurrencyResponse[]> {
        return await firstValueFrom(this.httpClient.get<CurrencyResponse[]>("api/v1/currency"));
    }
}