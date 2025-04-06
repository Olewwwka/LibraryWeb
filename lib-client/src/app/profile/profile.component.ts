import { Component, OnInit } from '@angular/core';
import { BooksService } from '../services/books.service';
import { BookModel } from '../Models/BookModel';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { AuthService } from '../services/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { timer } from 'rxjs';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent implements OnInit {
  borrowedBooks: BookModel[] = [];

  constructor(
    private booksService: BooksService,
    private authService: AuthService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    // Добавляем небольшую задержку перед загрузкой книг
    timer(100).subscribe(() => {
      this.loadBorrowedBooks();
    });
  }

  loadBorrowedBooks(): void {
    const user = this.authService.getUser();
    if (user && user.id) {
      this.booksService.getUserBorrowedBooks(user.id).subscribe({
        next: (books) => {
          this.borrowedBooks = books;
        },
        error: (err) => {
          this.snackBar.open('Ошибка загрузки книг', 'Закрыть', { duration: 3000 });
        }
      });
    } else {
      this.snackBar.open('Пользователь не авторизован', 'Закрыть', { duration: 3000 });
    }
  }

  returnBook(book: BookModel): void {
    this.booksService.returnBook(book.id).subscribe({
      next: () => {
        this.snackBar.open('Книга успешно возвращена', 'Закрыть', { duration: 3000 });
        this.loadBorrowedBooks();
      },
      error: (err) => {
        this.snackBar.open('Ошибка при возврате книги', 'Закрыть', { duration: 3000 });
      }
    });
  }
} 