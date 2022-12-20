import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {AuthenticationService} from "../../shared/services/authentication.service";
import {Router} from "@angular/router";
import {UserForFastRegistrationDto} from "../../_interfaces/user/userForFastRegistrationDto";
import {HttpErrorResponse} from "@angular/common/http";

@Component({
  selector: 'app-fast-register',
  templateUrl: './fast-register.component.html',
  styleUrls: ['./fast-register.component.css']
})
export class FastRegisterComponent implements OnInit {
  fastRegisterForm!: FormGroup;
  public errorMessage: string = '';
  public showError!: boolean;

  @Output() onRegisterClick: EventEmitter<string> = new EventEmitter();

  constructor(private authService: AuthenticationService,
              private router: Router) { }

  ngOnInit(): void {
    this.fastRegisterForm = new FormGroup({
      firstName: new FormControl('', [Validators.required]),
      lastName: new FormControl('', [Validators.required]),
      patronymic: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      phoneNumber: new FormControl('', [Validators.required,
        Validators.pattern("^((\\+373-?)|0)?[0-9]{8}$")]),
      dateOfBirth: new FormControl('', [Validators.required]),
      address: new FormControl('', [Validators.required]),
      gender: new FormControl('', [Validators.required]),
    });
  }

  public validateControl = (controlName: string) => {
    return this.fastRegisterForm.get(controlName)!.invalid && this.fastRegisterForm.get(controlName)!.touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.fastRegisterForm.get(controlName)!.hasError(errorName)
  }

  public fastRegisterUser = (fastRegisterFormValue: any) => {
    this.showError = false;
    const formValues = { ...fastRegisterFormValue };

    const user: UserForFastRegistrationDto = {
      firstName: formValues.firstName,
      lastName: formValues.lastName,
      patronymic: formValues.patronymic,
      email: formValues.email,
      phoneNumber: formValues.phoneNumber,
      dateOfBirth: formValues.dateOfBirth,
      address: formValues.address,
      gender: formValues.gender
    };

    this.authService.fastRegisterUser("api/accounts/FastRegistration", user)
      .subscribe({
        next: (_) => {
          this.onRegisterClick.emit(user.email);
        },
        error: (err: HttpErrorResponse) => {
          this.onRegisterClick.emit(undefined)
          this.errorMessage = err.message;
          this.showError = true;
        }
      })
  }

}
