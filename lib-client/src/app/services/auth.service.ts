import { Injectable } from '@angular/core';
import {Router} from '@angular/router';
import {HttpClient} from '@angular/common/http';
import {UserModel} from '../Models/UserModel';
import { tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl:string = `${environment.apiUrl}/auth`;
  private apiUrl1:string = `${environment.apiUrl}/users/refresh-token`;
  private userKey: string = 'user';
  private roleKey: string = 'role';

  constructor(private router: Router, private http: HttpClient) { }

  login(credentials: {email: string, password: string}): Observable<UserModel> {
    return this.http.post<UserModel>(this.apiUrl + '/login', credentials, {withCredentials: true}).pipe(
      tap(user => {
        localStorage.setItem('user', JSON.stringify(user));
        localStorage.setItem('role', user.role);
      }),
      tap(() => this.router.navigate(['/home']))
    );
  }
  
  refreshToken() {
    return this.http.post(this.apiUrl1, {}, {withCredentials: true});
  }

  register(userData: {name: string, email: string, password: string}): Observable<UserModel> {
    return this.http.post<UserModel>(this.apiUrl, userData, {withCredentials: true}).pipe();
  }

  logout() {
    localStorage.removeItem(this.userKey);
    localStorage.removeItem(this.roleKey);
    this.router.navigate(['/login']);
  }

  getUser() {
    return JSON.parse(localStorage.getItem(this.userKey) || '{}');
  }

  isAuthenticated() {
    return localStorage.getItem(this.userKey)? true: false;
  }

  isAdmin() {
    return localStorage.getItem(this.roleKey) === 'Admin';
  }
}
