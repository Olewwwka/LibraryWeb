import {Component, OnInit} from '@angular/core';
import {BookModel} from '../Models/BookModel';
import {BooksService} from '../services/books.service';
import {CommonModule, NgForOf} from '@angular/common';
import {MatCardModule} from '@angular/material/card';
import {MatPaginator, PageEvent} from '@angular/material/paginator';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {AuthService} from '../services/auth.service';
import {MatSnackBar} from '@angular/material/snack-bar';
import {Router} from '@angular/router';
import {MatDialog, MatDialogModule} from '@angular/material/dialog';
import {BookDialogComponent} from './book-dialog/book-dialog.component';
import {Genre, getGenreName} from '../Models/Genre';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { FormsModule } from '@angular/forms';
import { AuthorsService } from '../services/authors.service';
import { AuthorModel } from '../Models/AuthorModel';
import { MatInputModule } from '@angular/material/input';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { BookImageComponent } from '../shared/components/book-image/book-image.component';
import { BookImageService } from '../services/book-image.service';
import { BookImageUploadComponent } from '../shared/components/book-image-upload/book-image-upload.component';
import { Observable } from 'rxjs';

interface BookResponse {
  books: BookModel[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
}

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    NgForOf,
    MatPaginator,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    FormsModule,
    MatSnackBarModule,
    BookImageComponent,
    BookImageUploadComponent
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {
  books: BookModel[] = [];
  authors: AuthorModel[] = [];
  genres = Object.values(Genre).filter(value => typeof value === 'number');
  selectedGenre: Genre | null = null;
  selectedAuthor: string | null = null;
  searchQuery: string = '';
  currentPage = 1;
  pageSize = 10;
  totalPages = 0;
  totalCount = 0;
  isAdmin: boolean = false;
  isUser: boolean = false;
  file: File | null = null;
  getGenreName = getGenreName;
  Genre = Genre;

  constructor(
    private booksService: BooksService,
    private authorsService: AuthorsService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar,
    private authService: AuthService,
    private router: Router,
    private bookImageService: BookImageService
  ) {
    this.isAdmin = this.authService.isAdmin();
    this.isUser = !this.isAdmin && this.authService.isAuthenticated();
  }

  ngOnInit(): void {
    this.isAdmin = this.authService.isAdmin();
    this.loadBooks();
    this.loadAuthors();
  }

  getAuthorName(authorId: string): string {
    const author = this.authors.find(a => a.id === authorId);
    return author ? `${author.name} ${author.surname}` : 'Неизвестный автор';
  }

  loadBooks(): void {
    let request: Observable<BookResponse>;
    
    if (this.selectedGenre && this.selectedAuthor) {
      request = this.booksService.getBooksByGenreAndAuthor(this.selectedGenre, this.selectedAuthor, this.currentPage, this.pageSize);
    } else if (this.selectedGenre) {
      request = this.booksService.getBooksByGenre(this.selectedGenre, this.currentPage, this.pageSize);
    } else if (this.selectedAuthor) {
      request = this.booksService.getBooksByAuthor(this.selectedAuthor, this.currentPage, this.pageSize);
    } else {
      request = this.booksService.getBooks(this.currentPage, this.pageSize);
    }

    request.subscribe({
      next: (response) => {
        this.books = response.books;
        this.totalCount = response.totalCount;
        this.totalPages = response.totalPages;
        
        // Фильтрация по поисковому запросу на клиенте
        if (this.searchQuery) {
          const query = this.searchQuery.toLowerCase();
          this.books = this.books.filter(book => 
            book.name.toLowerCase().includes(query)
          );
        }
      },
      error: (error) => {
        this.snackBar.open('Ошибка при загрузке книг', 'Закрыть', { duration: 3000 });
      }
    });
  }

  loadAuthors(): void {
    this.authorsService.getAuthors().subscribe({
      next: (authors) => {
        this.authors = authors;
      },
      error: (error) => {
        this.snackBar.open('Ошибка при загрузке авторов', 'Закрыть', { duration: 3000 });
      }
    });
  }

  onGenreChange(): void {
    this.currentPage = 1;
    this.loadBooks();
  }

  onAuthorChange(): void {
    this.currentPage = 1;
    this.loadBooks();
  }

  onPageChange(page: number, newPageSize: number): void {
    this.currentPage = page;
    this.pageSize = newPageSize;
    this.loadBooks();
  }

  borrowBook(book: BookModel): void {
    const userId = this.authService.getUser()?.id;
    if (!userId) {
      this.snackBar.open('Unathorized', 'Close', { duration: 3000 });
      return;
    }

    const borrowTime = new Date();
    const returnTime = new Date();
    returnTime.setDate(returnTime.getDate() + 14); 

    this.booksService.borrowBook(book.id, userId, borrowTime, returnTime).subscribe({
      next: () => {
        this.snackBar.open('You take book!', 'Close', { duration: 3000 });
        this.loadBooks();
      },
      error: (err) => {
        this.snackBar.open('Error', 'Close', { duration: 3000 });
      }
    });
  }

  openBookDialog(book?: BookModel): void {
    const dialogRef = this.dialog.open(BookDialogComponent, {
      width: '500px',
      data: book
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadBooks();
      }
    });
  }

  deleteBook(bookId: string): void {
    if (confirm('Sure?')) {
      this.booksService.deleteBook(bookId).subscribe({
        next: () => {
          this.snackBar.open('Sucsess!', 'Close', { duration: 3000 });
          this.loadBooks();
        },
        error: (error) => {
          this.snackBar.open('Error', 'Close', { duration: 3000 });
        }
      });
    }
  }

  onSearch(): void {
    this.currentPage = 1;
    this.loadBooks();
  }

  uploadImage(bookId: string, file: File): void {
    this.booksService.uploadBookImage(bookId, file).subscribe({
      next: () => {
        this.snackBar.open('Sucsess!', 'Close', { duration: 3000 });
        this.loadBooks();
      },
      error: (error) => {
        this.snackBar.open('Error!', 'Close', { duration: 3000 });
      }
    });
  }
}
