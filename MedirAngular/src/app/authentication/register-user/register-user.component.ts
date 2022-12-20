import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { PasswordConfirmationValidatorService } from 'src/app/shared/custom-validators/password-confirmation-validator.service';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';
import { UserForRegistrationDto } from 'src/app/_interfaces/user/userForRegistrationDto.model';

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.css']
})
export class RegisterUserComponent implements OnInit {
  registerForm!: FormGroup;
  public errorMessage: string = '';
  public showError!: boolean;

  constructor(private authService: AuthenticationService,
              private passConfValidator: PasswordConfirmationValidatorService,
              private router: Router) { }

  ngOnInit(): void {
    this.registerForm = new FormGroup({
      firstName: new FormControl('', [Validators.required]),
      lastName: new FormControl('', [Validators.required]),
      patronymic: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      phoneNumber: new FormControl('', [Validators.required,
        Validators.pattern("^((\\+373-?)|0)?[0-9]{8}$")]),
      dateOfBirth: new FormControl('', [Validators.required]),
      address: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required]),
      confirm: new FormControl(''),
      gender: new FormControl('', [Validators.required]),
      country: new FormControl('', [Validators.required]),
      region: new FormControl('', [Validators.required]),
      street: new FormControl('', [Validators.required]),
      house: new FormControl('', [Validators.required]),
      polyceNumber: new FormControl('', [Validators.required,
        Validators.pattern("^\\d{16}$")]),
    });

    this.registerForm.get('confirm')!.setValidators([Validators.required,
      this.passConfValidator.validateConfirmPassword(this.registerForm.get('password')!)]);
  }

  public validateControl = (controlName: string) => {
    return this.registerForm.get(controlName)!.invalid && this.registerForm.get(controlName)!.touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.registerForm.get(controlName)!.hasError(errorName)
  }

  public registerUser = (registerFormValue: any) => {
    this.showError = false;
    const formValues = { ...registerFormValue };

    const user: UserForRegistrationDto = {
      firstName: formValues.firstName,
      lastName: formValues.lastName,
      patronymic: formValues.patronymic,
      email: formValues.email,
      phoneNumber: formValues.phoneNumber,
      dateOfBirth: formValues.dateOfBirth,
      address: formValues.address,
      password: formValues.password,
      passwordConfirm: formValues.confirm,
      gender: formValues.gender,
      country: formValues.country,
      region: formValues.region,
      street: formValues.street,
      house: formValues.house,
      polyceNumber: formValues.polyceNumber
    };

    this.authService.registerUser("api/accounts/registration", user)
      .subscribe({
        next: (_) => this.router.navigate(["/authentication/login"]),
        error: (err: HttpErrorResponse) => {
          this.errorMessage = err.message;
          this.showError = true;
        }
      })
  }
}
