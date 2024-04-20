import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { environment } from '../environments/environment.development';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { interceptorInterceptor } from './interceptor.interceptor';

function getApiUrl(): string {
  return environment.api;
}

function getApiGetwayUrl(): string {
  return environment.api_getway;
}

export const appConfig: ApplicationConfig = {
  providers: [
    { provide: 'API_URL', useFactory: getApiUrl, deps: [] },
    { provide: 'APIGETWAY_URL', useFactory: getApiGetwayUrl, deps: [] },
    provideHttpClient(),
    provideRouter(routes),
    provideHttpClient(withInterceptors([interceptorInterceptor])),
  ]
};
