import { Component, inject, signal } from "@angular/core";
import { IdentityHubService } from "../../services/identity-hub.service";
import { DashboardResponse } from "../../models/dashboard.model";

@Component({
    selector: 'dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.scss'],
    standalone: true
})

export class DashboardComponent {
    private identityService = inject(IdentityHubService);

    constructor() {
        this.initializationAsync();
    }

    dashboardData = signal<DashboardResponse | null>(null);

    async initializationAsync() {
        this.dashboardData.set(await this.identityService.initDashboard());
    }
}