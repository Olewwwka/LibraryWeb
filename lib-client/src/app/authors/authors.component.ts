import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { AuthorsService } from '../services/authors.service';
import { AuthorModel } from '../Models/AuthorModel';
import { AuthService } from '../services/auth.service';
import { AuthorDialogComponent } from './author-dialog/author-dialog.component';

@Component({
  selector: 'app-authors',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    MatSnackBarModule
  ],
  templateUrl: './authors.component.html',
  styleUrls: ['./authors.component.scss']
})
export class AuthorsComponent implements OnInit {
  authors: AuthorModel[] = [];
  isAdmin: boolean = false;

  constructor(
    private authorsService: AuthorsService,
    private authService: AuthService,
    private snackBar: MatSnackBar,
    private dialog: MatDialog
  ) {
    this.isAdmin = this.authService.isAdmin();
  }

  ngOnInit(): void {
    this.loadAuthors();
  }

  loadAuthors(): void {
    this.authorsService.getAuthors().subscribe({
      next: (authors) => {
        this.authors = authors;
      },
      error: (err) => {
        this.snackBar.open('Ошибка при загрузке авторов', 'Закрыть', { duration: 3000 });
      }
    });
  }

  openAuthorDialog(author?: AuthorModel): void {
    const dialogRef = this.dialog.open(AuthorDialogComponent, {
      width: '500px',
      data: author || {
        name: '',
        surname: '',
        birthday: new Date(),
        country: ''
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (author) {
          this.authorsService.updateAuthor(author.id, result).subscribe({
            next: () => {
              this.snackBar.open('Автор успешно обновлен', 'Закрыть', { duration: 3000 });
              this.loadAuthors();
            },
            error: (err) => {
              this.snackBar.open('Ошибка при обновлении автора', 'Закрыть', { duration: 3000 });
            }
          });
        } else {
          this.authorsService.addAuthor(result).subscribe({
            next: () => {
              this.snackBar.open('Автор успешно добавлен', 'Закрыть', { duration: 3000 });
              this.loadAuthors();
            },
            error: (err) => {
              this.snackBar.open('Ошибка при добавлении автора', 'Закрыть', { duration: 3000 });
            }
          });
        }
      }
    });
  }

  deleteAuthor(author: AuthorModel): void {
    if (confirm('Вы уверены, что хотите удалить этого автора?')) {
      this.authorsService.deleteAuthor(author.id).subscribe({
        next: () => {
          this.snackBar.open('Автор успешно удален', 'Закрыть', { duration: 3000 });
          this.loadAuthors();
        },
        error: (err) => {
          this.snackBar.open('Ошибка при удалении автора', 'Закрыть', { duration: 3000 });
        }
      });
    }
  }
} 