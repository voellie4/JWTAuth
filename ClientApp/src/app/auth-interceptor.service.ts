import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { from, Observable, switchMap } from 'rxjs';
import { AuthServiceService } from './auth-service.service';

@Injectable({
  providedIn: 'root'
})
export class AuthInterceptorService implements HttpInterceptor {

  constructor(private _authService: AuthServiceService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (req.url.startsWith("https://localhost:7054/token")) {
      return next.handle(req);
    }

    return from(this._authService.getToken()).pipe(switchMap(token => {
      req = req.clone({
        setHeaders: { Authorization: `Bearer ${token}` }
      });
      return next.handle(req);
    }));
    
  }
}
