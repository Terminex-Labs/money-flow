import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { bffUrlInterceptor } from '../core/interceptors/bff-url.interceptor';
import { withCredentialsInterceptor } from '../core/interceptors/with-credentials.interceptor';
import { authErrorInterceptor } from '../core/interceptors/auth-error.Interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideHttpClient(withInterceptors([
      bffUrlInterceptor, 
      withCredentialsInterceptor, 
      authErrorInterceptor
    ])),
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes)
  ]
};
