import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { environment } from '../environments/environment.development';
import { provideHttpClient } from '@angular/common/http';

function getApiUrl(): string {
  return environment.api;
}

export const appConfig: ApplicationConfig = {
  providers: [
    { provide: 'API_URL', useFactory: getApiUrl, deps: [] },
    provideHttpClient(),
    provideRouter(routes),
  ]
};
