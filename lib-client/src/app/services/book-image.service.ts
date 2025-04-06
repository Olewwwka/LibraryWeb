import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';

interface UploadImageResponse {
  imagePath: string;
}

@Injectable({
  providedIn: 'root'
})
export class BookImageService {
  private readonly API_URL = `${environment.apiUrl}/books`;

  constructor(private http: HttpClient) {}

  uploadImage(bookId: string, imageFile: File): Observable<UploadImageResponse> {
    const formData = new FormData();
    formData.append('imageFile', imageFile);
    return this.http.post<UploadImageResponse>(`${this.API_URL}/${bookId}/image`, formData, { withCredentials: true });
  }

  deleteImage(bookId: string): Observable<void> {
    return this.http.delete<void>(`${this.API_URL}/${bookId}/image`, { withCredentials: true });
  }
} 