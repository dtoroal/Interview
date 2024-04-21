import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

export const interceptorInterceptor: HttpInterceptorFn = (req, next) => {
  const authToken = localStorage.getItem('token');

  let authReq;

  if (authToken) {
    authReq = req.clone({
      setHeaders: {
        Authorization: `Bearer ${authToken}`
      }
    });
  } else {
    authReq = req.clone();
  }

  return next(authReq).pipe(
    catchError((err: any) => {
      console.log("interceptor", err);
      if (err.status === 401) {
        localStorage.removeItem('token');
      }
      return throwError(() => err);
    }),
  )
};
