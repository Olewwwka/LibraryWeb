import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError, switchMap, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  private isRefreshing = false;

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status === 401 && !this.isRefreshing) {
          this.isRefreshing = true;
          
          return this.authService.refreshToken().pipe(
            switchMap(() => {
              this.isRefreshing = false;
              return next.handle(request);
            }),
            catchError((refreshError) => {
              this.isRefreshing = false;
              this.authService.logout();
              this.router.navigate(['/login']);
              return throwError(() => refreshError);
            })
          );
        }
        
        return throwError(() => error);
      })
    );
  }
} 