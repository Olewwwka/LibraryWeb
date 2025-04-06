import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  credentials = {
    email: '',
    password: ''
  };

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  onLogin() {
    this.authService.login(this.credentials).subscribe({
      next: () => {
      },
      error: (err) => {
        console.error('Ошибка при входе:', err);
      }
    });
  }

  registerRedirect()
  {
    this.router.navigate(['/register']);
  }
}
