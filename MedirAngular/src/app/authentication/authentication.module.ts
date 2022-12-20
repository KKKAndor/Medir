import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterUserComponent } from './register-user/register-user.component';
import { RouterModule } from '@angular/router';
import {ReactiveFormsModule} from "@angular/forms";
import {BsDatepickerModule} from "ngx-bootstrap/datepicker";
import { LoginComponent } from './login/login.component';
import {SharedModule} from "../shared/shared.module";
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';

@NgModule({
  declarations: [RegisterUserComponent, LoginComponent, ForgotPasswordComponent, ResetPasswordComponent],
  exports: [
    BsDatepickerModule
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule.forChild([
      {path: 'register', component: RegisterUserComponent},
      {path: 'login', component: LoginComponent},
      { path: 'forgotpassword', component: ForgotPasswordComponent },
      { path: 'resetpassword', component: ResetPasswordComponent }
    ]),
    SharedModule
  ]
})
export class AuthenticationModule { }
