import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { AuthorModel } from '../Models/AuthorModel';
import { UpdateAuthorModel } from '../Models/UpdateAuthorModel';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthorsService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getAuthors(): Observable<AuthorModel[]> {
    return this.http.get<AuthorModel[]>(`${this.apiUrl}/authors`, {withCredentials: true});
  }

  getAuthorById(id: string): Observable<AuthorModel> {
    return this.http.get<AuthorModel>(`${this.apiUrl}/authors/${id}`, {withCredentials: true});
  }

  addAuthor(author: AuthorModel): Observable<AuthorModel> {
    return this.http.post<AuthorModel>(`${this.apiUrl}/authors`, author, {withCredentials: true});
  }

  updateAuthor(id: string, author: UpdateAuthorModel): Observable<AuthorModel> {
    const requestData = {
      Name: author.name,
      Surname: author.surname,
      Country: author.country,
      Birthday: author.birthday
    };
    return this.http.patch<AuthorModel>(`${this.apiUrl}/authors/up/${id}`, requestData, { withCredentials: true });
  }

  deleteAuthor(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/authors/${id}`, {withCredentials: true});
  }
} 