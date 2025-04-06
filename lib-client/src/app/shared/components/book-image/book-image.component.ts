import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-book-image',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="image-container">
      <img
        [src]="getImageUrl()"
        [alt]="altText"
        [class]="className"
      />
      <div *ngIf="error" class="error-message">
        Не удалось загрузить изображение
      </div>
    </div>
  `,
  styles: [`
    :host {
      display: block;
    }
    .image-container {
      position: relative;
      width: 100%;
      height: 100%;
      display: flex;
      justify-content: center;
      align-items: center;
      background-color: #f5f5f5;
      border-radius: 4px;
      padding: 10px;
    }
    img {
      max-width: 100%;
      max-height: 100%;
      object-fit: contain;
    }
    .error-message {
      color: red;
      text-align: center;
    }
  `]
})
export class BookImageComponent implements OnInit {
  @Input() imagePath: string | null = null;
  @Input() altText: string = '';
  @Input() className: string = '';

  error: boolean = false;
  private readonly API_URL = environment.apiUrl;

  ngOnInit(): void {
    // Additional initialization logic if needed
  }

  getImageUrl(): string {
    if (!this.imagePath) {
      return `${this.API_URL}/uploads/default_image.jpg`;
    }
    return `${this.API_URL}/uploads/${this.imagePath}`;
  }

  onImageError(event: Event): void {
    this.error = true;
  }

  onImageLoad(event: Event): void {
    this.error = false;
  }
} 