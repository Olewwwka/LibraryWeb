import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { BookModel } from '../../Models/BookModel';
import { Genre, getGenreName } from '../../Models/Genre';
import { AuthorsService } from '../../services/authors.service';
import { AuthorModel } from '../../Models/AuthorModel';
import { MatSelectChange } from '@angular/material/select';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BooksService } from '../../services/books.service';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-book-dialog',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatIconModule
  ],
  templateUrl: './book-dialog.component.html',
  styleUrls: ['./book-dialog.component.scss']
})
export class BookDialogComponent implements OnInit {
  genres = Object.values(Genre).filter(value => typeof value === 'number');
  Genre = Genre;
  authors: AuthorModel[] = [];
  bookForm: FormGroup;
  selectedImage: string;
  selectedFile: File | null = null;

  constructor(
    public dialogRef: MatDialogRef<BookDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: BookModel,
    private authorsService: AuthorsService,
    private snackBar: MatSnackBar,
    private booksService: BooksService,
    private fb: FormBuilder
  ) {
    this.bookForm = this.fb.group({
      name: [data?.name || '', Validators.required],
      isbn: [data?.isbn || '', [Validators.required, Validators.pattern(/^\d{13}$/)]],
      genre: [data?.genre || Genre.Fiction, Validators.required],
      authorId: [data?.authorId || '', Validators.required],
      description: [data?.description || '']
    });

    if (data?.imagePath) {
      this.selectedImage = `${environment.apiUrl}/uploads/${data.imagePath}`;
    } else {
      this.selectedImage = `${environment.apiUrl}/uploads/default_image.jpg`;
    }
  }

  ngOnInit(): void {
    this.authorsService.getAuthors().subscribe({
      next: (authors) => {
        this.authors = authors;
      },
      error: (err) => {
        this.snackBar.open('Ошибка при загрузке авторов', 'Закрыть', { duration: 3000 });
      }
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files[0]) {
      this.selectedFile = input.files[0];
      
      // Создаем превью изображения
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.selectedImage = e.target.result;
      };
      reader.readAsDataURL(this.selectedFile);
    }
  }

  onSubmit(): void {
    if (this.bookForm.valid) {
      const bookData = this.bookForm.value;
      
      if (this.data?.id) {
        this.booksService.updateBook(this.data.id, bookData).subscribe({
          next: () => {
            this.snackBar.open('Sucsess!', 'Close', { duration: 3000 });
            this.dialogRef.close(true);
          },
          error: (error) => {
            this.snackBar.open('Error!', 'Close', { duration: 3000 });
          }
        });
      } else {
        this.booksService.addBook(bookData).subscribe({
          next: () => {
            this.snackBar.open('Sucsess!', 'Close', { duration: 3000 });
            this.dialogRef.close(true);
          },
          error: (error) => {
            this.snackBar.open('Error!', 'Close', { duration: 3000 });
          }
        });
      }
    }
  }
} 