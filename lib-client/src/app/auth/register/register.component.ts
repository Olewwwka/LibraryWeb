import { Component } from '@angular/core';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatCard, MatCardContent, MatCardHeader, MatCardTitle} from "@angular/material/card";
import {MatFormField, MatInput, MatLabel} from "@angular/material/input";
import {MatButton} from '@angular/material/button';
import {AuthService} from '../../services/auth.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    FormsModule,
    MatButton,
    MatCard,
    MatCardContent,
    MatCardHeader,
    MatCardTitle,
    MatFormField,
    MatInput,
    MatLabel,
    ReactiveFormsModule,
    MatFormField
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
    credentials ={
        name: '',
        email: '',
        password: ''
    }
    constructor(
      private authService: AuthService,
      private router: Router
    ) {
    }

    onRegister() {
      this.authService.register(this.credentials);
    }

    loginRedirect(){
      this.router.navigate(['login']);
    }
}
