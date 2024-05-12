import { Component, EventEmitter, Output } from '@angular/core';
import { AuthService } from '../../../../services/auth/auth.service';
import { HttpErrorResponse } from '@angular/common/http';
import { FormControl, UntypedFormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  public signUpIsActive: boolean = false;

  @Output() toggleLoginModalEvent: EventEmitter<boolean> = new EventEmitter<boolean>();

  public loginForm = new UntypedFormGroup({
    email: new FormControl<string>('', [Validators.required]),
    password: new FormControl<string>('', [Validators.required]),
  });

  constructor(private authService: AuthService) { }

  public toggleLoginModal(): void {
    this.toggleLoginModalEvent.emit(false);
  }

  public login(): void {
    this.authService.login(this.loginForm.value).subscribe({
      next: (token: string) => {
        localStorage.setItem('token', token);
        window.location.href = `/home`;
      },
      error: (err: HttpErrorResponse) => {
        console.error(err);
        localStorage.removeItem('token');
      }
    });
  }
}
