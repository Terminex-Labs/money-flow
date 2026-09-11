import { HttpInterceptorFn } from "@angular/common/http";
import { environment } from "../../environments/environment";

export const bffUrlInterceptor: HttpInterceptorFn = (request, next) => {
    if (request.url.startsWith('http://') || request.url.startsWith('https://'))
        return next(request);
    
    const baseUrl = environment.bffBase.replace(/\/$/, '');
    const requestPath = request.url.replace(/^\//, '');
    const apiRequest = request.clone({
        url: `${baseUrl}/${requestPath}`
    });
    
    return next(apiRequest);
}