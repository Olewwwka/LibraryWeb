import { Component, EventEmitter, Input, Output } from '@angular/core';
import { BookImageService } from '../../../services/book-image.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-book-image-upload',
  standalone: true,
  imports: [MatIconModule, MatButtonModule],
  template: `
    <div class="upload-container">
      <input 
        type="file" 
        accept="image/jpeg,image/png"
        (change)="onFileSelected($event)"
        style="display: none"
        #fileInput
      />
      <button 
        mat-button 
        color="primary"
        (click)="fileInput.click()"
      >
        <mat-icon>photo_camera</mat-icon>
        Выбрать изображение
      </button>
      <button 
        mat-button 
        color="warn"
        (click)="deleteImage()"
        *ngIf="imagePath && imagePath !== 'images/default_image.jpg'"
      >
        Удалить
      </button>
    </div>
  `,
  styles: [`
    .upload-container {
      display: flex;
      gap: 10px;
      margin: 10px 0;
    }
  `]
})
export class BookImageUploadComponent {
  @Input() bookId: string = '';
  @Input() imagePath: string = 'images/default_image.jpg';
  @Output() imageUploaded = new EventEmitter<string>();
  @Output() imageDeleted = new EventEmitter<void>();

  constructor(private bookImageService: BookImageService, private snackBar: MatSnackBar) { }

  onFileSelected(event: Event): void {
    const file = (event.target as HTMLInputElement).files?.[0];
    if (!file) return;

    if (file.size > 5 * 1024 * 1024) {
      alert('Размер файла не должен превышать 5MB');
      return;
    }

    if (!['image/jpeg', 'image/png'].includes(file.type)) {
      alert('Допустимы только файлы JPG и PNG');
      return;
    }

    this.bookImageService.uploadImage(this.bookId, file).subscribe({
      next: (response: any) => {
        this.imagePath = response.imagePath;
        this.imageUploaded.emit(this.imagePath);
      },
      error: (error: any) => {
        this.snackBar.open('Ошибка при загрузке изображения', 'Закрыть', { duration: 3000 });
      }
    });
  }

  deleteImage(): void {
    this.bookImageService.deleteImage(this.bookId).subscribe({
      next: () => {
        this.imagePath = 'images/default_image.jpg';
        this.imageDeleted.emit();
      },
      error: (error: any) => {
        this.snackBar.open('Ошибка при удалении изображения', 'Закрыть', { duration: 3000 });
      }
    });
  }
} 