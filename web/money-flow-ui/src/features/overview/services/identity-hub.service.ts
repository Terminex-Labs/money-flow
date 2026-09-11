import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { firstValueFrom } from "rxjs";
import { DashboardResponse } from "../models/dashboard.model";

@Injectable({providedIn: 'root'})

export class IdentityHubService {
    private httpClient = inject(HttpClient);

    async initDashboard() : Promise<DashboardResponse> {
        return await firstValueFrom(this.httpClient.get<DashboardResponse>("user/dashboard"));
    }
}