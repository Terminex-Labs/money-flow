import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { catchError, firstValueFrom, map, of } from "rxjs";

@Injectable({providedIn: 'root'})

export class AdminService {
    private readonly httpClient = inject(HttpClient);

    async check() : Promise<boolean> {
        console.log('[AdminService.check] вызван');
        return await firstValueFrom(this.httpClient.get('api/v1/check/admin')
        .pipe
        (
            map(() => {
                console.log('[AdminService.check] 200 OK');
                return true;
            }),
            catchError((error) => 
            {
                console.log('[AdminService.check] error', error.status);
                if (error.status == 403)
                    return of(false);

                throw error;
            })
        ));
    }
}