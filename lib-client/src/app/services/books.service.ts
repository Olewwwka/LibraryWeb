import { Injectable } from '@angular/core';
import {Router} from '@angular/router';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';
import {PaginatedBooks} from '../Models/PaginatedBooks';
import {HttpParams} from '@angular/common/http';
import { map, tap, catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { Genre } from '../Models/Genre';
import { BookModel } from '../Models/BookModel';
import { environment } from '../../environments/environment';

interface BookResponse {
  books: BookModel[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
}

@Injectable({
  providedIn: 'root'
})
export class BooksService {
  private apiUrl:string = `${environment.apiUrl}/books`;
  private usersApiUrl:string = `${environment.apiUrl}/users`;

  constructor(
    private http: HttpClient
  ) { }

  private getHeaders(): HttpHeaders {
    const token = localStorage.getItem('token');
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });
  }

  private getFileUploadHeaders(): HttpHeaders {
    const token = localStorage.getItem('token');
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
  }

  getBooks(pageNumber: number = 1, pageSize: number = 10): Observable<{ books: BookModel[], totalCount: number, pageNumber: number, pageSize: number, totalPages: number }> {
    return this.http.get<{ books: BookModel[], totalCount: number, pageNumber: number, pageSize: number, totalPages: number }>(
      `${this.apiUrl}?pageNumber=${pageNumber}&pageSize=${pageSize}`,
      { headers: this.getHeaders(), withCredentials: true }
    );
  }

  getBook(id: string): Observable<BookModel> {
    return this.http.get<BookModel>(`${this.apiUrl}/${id}`, { headers: this.getHeaders(), withCredentials: true });
  }

  getBookByISBN(isbn: string): Observable<BookModel> {
    return this.http.get<BookModel>(`${this.apiUrl}/isbn/${isbn}`, { headers: this.getHeaders(), withCredentials: true });
  }

  createBook(book: Partial<BookModel>): Observable<BookModel> {
    return this.http.post<BookModel>(this.apiUrl, book, { headers: this.getHeaders(), withCredentials: true });
  }

  updateBook(id: string, book: Partial<BookModel>): Observable<BookModel> {
    return this.http.patch<BookModel>(`${this.apiUrl}/${id}`, book, { headers: this.getHeaders(), withCredentials: true });
  }

  deleteBook(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`, { headers: this.getHeaders(), withCredentials: true });
  }

  borrowBook(bookId: string, userId: string, borrowTime: Date, returnTime: Date): Observable<BookModel> {
    const request = {
      bookId: bookId,
      userId: userId,
      borrowTime: borrowTime,
      returnTime: returnTime
    };
    return this.http.post<BookModel>(`${this.usersApiUrl}/book/borrow`, request, { headers: this.getHeaders(), withCredentials: true });
  }

  returnBook(bookId: string): Observable<BookModel> {
    return this.http.post<BookModel>(`${this.usersApiUrl}/book/return/${bookId}`, null, { headers: this.getHeaders(), withCredentials: true });
  }

  getUserBorrowedBooks(userId: string): Observable<BookModel[]> {
    return this.http.get<BookModel[]>(`${this.usersApiUrl}/books?id=${userId}`, { headers: this.getHeaders(), withCredentials: true });
  }

  addBook(book: BookModel): Observable<BookModel> {
    return this.http.post<BookModel>(`${this.apiUrl}`, book, { headers: this.getHeaders(), withCredentials: true });
  }

  uploadBookImage(bookId: string, file: File): Observable<{ imagePath: string }> {
    const formData = new FormData();
    formData.append('imageFile', file);
    return this.http.post<{ imagePath: string }>(`${this.apiUrl}/${bookId}/image`, formData, { 
      headers: this.getFileUploadHeaders(),
      withCredentials: true 
    });
  }

  getBooksByGenre(genre: Genre, pageNumber: number = 1, pageSize: number = 10): Observable<{ books: BookModel[], totalCount: number, pageNumber: number, pageSize: number, totalPages: number }> {
    return this.http.get<{ books: BookModel[], totalCount: number, pageNumber: number, pageSize: number, totalPages: number }>(
      `${this.apiUrl}/genre/${genre}?pageNumber=${pageNumber}&pageSize=${pageSize}`, { headers: this.getHeaders(), withCredentials: true }
    );
  }

  getBooksByAuthor(authorId: string, pageNumber: number, pageSize: number): Observable<BookResponse> {
    return this.http.get<BookResponse>(`http://localhost:5202/api/authors/${authorId}/books?pageNumber=${pageNumber}&pageSize=${pageSize}`, { headers: this.getHeaders(), withCredentials: true })
      .pipe(
        catchError(error => {
          return throwError(() => error);
        })
      );
  }

  getBooksByGenreAndAuthor(genre: Genre, authorId: string, pageNumber: number = 1, pageSize: number = 10): Observable<{ books: BookModel[], totalCount: number, pageNumber: number, pageSize: number, totalPages: number }> {
    return this.http.get<{ books: BookModel[], totalCount: number, pageNumber: number, pageSize: number, totalPages: number }>(
      `${this.apiUrl}/genre/${genre}/author/${authorId}?pageNumber=${pageNumber}&pageSize=${pageSize}`, { headers: this.getHeaders(), withCredentials: true }
    );
  }
}
