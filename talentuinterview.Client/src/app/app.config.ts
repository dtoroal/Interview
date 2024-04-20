import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { environment } from '../environments/environment.development';
import { provideHttpClient } from '@angular/common/http';

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
  ]
};
