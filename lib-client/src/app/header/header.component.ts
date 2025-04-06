import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [MatButtonModule, MatIconModule, CommonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  constructor(
    private router: Router,
    private authService: AuthService
  ) {}

  navigateToHome(): void {
    this.router.navigate(['/home']);
  }

  navigateToProfile(): void {
    this.router.navigate(['/profile']);
  }
  navigateToAuthors(): void {
    this.router.navigate(['/authors']);
  }

  onLogout(): void {
    localStorage.clear();
    this.router.navigate(['/login']);
  }

  isAuthenticated(): boolean {
    return this.authService.isAuthenticated();
  }
}
