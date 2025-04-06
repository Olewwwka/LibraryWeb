import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { AuthorModel } from '../../Models/AuthorModel';
import { ValidationConstants, ErrorMessages } from '../../constants/validation.constants';

@Component({
  selector: 'app-author-dialog',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule
  ],
  templateUrl: './author-dialog.component.html',
  styleUrls: ['./author-dialog.component.scss']
})
export class AuthorDialogComponent {
  authorForm: FormGroup;
  errorMessages = ErrorMessages.author;

  constructor(
    public dialogRef: MatDialogRef<AuthorDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: AuthorModel,
    private fb: FormBuilder
  ) {
    this.authorForm = this.fb.group({
      name: [
        data?.name || '',
        [
          Validators.required,
          Validators.minLength(ValidationConstants.author.name.minLength),
          Validators.maxLength(ValidationConstants.author.name.maxLength),
          Validators.pattern(ValidationConstants.author.name.pattern)
        ]
      ],
      surname: [
        data?.surname || '',
        [
          Validators.required,
          Validators.minLength(ValidationConstants.author.surname.minLength),
          Validators.maxLength(ValidationConstants.author.surname.maxLength),
          Validators.pattern(ValidationConstants.author.surname.pattern)
        ]
      ],
      country: [
        data?.country || '',
        [
          Validators.required,
          Validators.minLength(ValidationConstants.author.country.minLength),
          Validators.maxLength(ValidationConstants.author.country.maxLength)
        ]
      ],
      birthday: [
        data?.birthday || new Date(),
        [
          Validators.required,
          this.validateBirthday
        ]
      ]
    });
  }

  validateBirthday(control: any) {
    const date = new Date(control.value);
    const today = new Date();
    const minDate = new Date(ValidationConstants.author.birthday.minYear, 0, 1);

    if (date > today) {
      return { past: true };
    }
    if (date < minDate) {
      return { minYear: true };
    }
    return null;
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onSubmit(): void {
    if (this.authorForm.valid) {
      this.dialogRef.close(this.authorForm.value);
    }
  }
} 