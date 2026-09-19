import { HttpErrorResponse, HttpInterceptorFn } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { catchError, throwError } from "rxjs";

export const authErrorInterceptor: HttpInterceptorFn = (request, next) => {
    if (request.headers.has('X-Skip-Auth-Interceptor')) {
        return next(request);
    }

    return next(request).pipe(
        catchError((error: HttpErrorResponse) => {
            if (error.status === 401) {
                const currentUrl = window.location.href;
                const returnUrl = encodeURIComponent(currentUrl);
                window.location.href = `${ environment.returnAuthUrlBase }?returnUrl = ${ returnUrl }`;
            }

            return throwError(() => error);
        })
    );
}