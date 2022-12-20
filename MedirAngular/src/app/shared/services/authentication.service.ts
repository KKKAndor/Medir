import { Injectable } from '@angular/core';
import { UserForRegistrationDto } from './../../_interfaces/user/userForRegistrationDto.model';
import { HttpClient } from '@angular/common/http';
import { EnvironmentUrlService } from './environment-url.service';
import { RegistrationResponseDto } from 'src/app/_interfaces/response/registrationResponseDto';
import { UserForAuthenticationDto } from 'src/app/_interfaces/user/userForAuthenticationDto';
import { AuthResponseDto } from 'src/app/_interfaces/response/AuthResponseDto';
import { Subject } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import * as Console from "console";
import { UserForFastRegistrationDto } from 'src/app/_interfaces/user/userForFastRegistrationDto';
import { FastRegistrationResponseDto } from 'src/app/_interfaces/response/fastRegistrationResponseDto';
import { ForgotPasswordDto } from 'src/app/_interfaces/user/forgotPasswordDto';
import { ResetPasswordDto } from 'src/app/_interfaces/user/ResetPasswordDto';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private authChangeSub = new Subject<boolean>()
  public authChanged = this.authChangeSub.asObservable();

  constructor(
    private http: HttpClient,
    private envUrl: EnvironmentUrlService,
    private jwtHelper: JwtHelperService) { }

  public registerUser = (route: string, body: UserForRegistrationDto) => {
    return this.http.post<RegistrationResponseDto> (this.createCompleteRoute(route, this.envUrl.urlAddress), body);
  }

  public fastRegisterUser = (route: string, body: UserForFastRegistrationDto) => {
    return this.http.post<FastRegistrationResponseDto> (this.createCompleteRoute(route, this.envUrl.urlAddress), body);
  }

  public sendAuthStateChangeNotification = (isAuthenticated: boolean) => {
    this.authChangeSub.next(isAuthenticated);
  }

  public isUserAdmin = (): boolean => {
    const token = localStorage.getItem("token");
    const decodedToken = this.jwtHelper.decodeToken(token!);
    const role = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
    return role === 'Administrator';
  }

  public getUserName = (): string =>{
    const token = localStorage.getItem("token");
    const decodedToken = this.jwtHelper.decodeToken(token!);
    const name = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']
    return name;
  }

  public isUserAuthenticated = (): boolean => {
    const token = localStorage.getItem("token");
    // @ts-ignore
    return token && !this.jwtHelper.isTokenExpired(token);
  }

  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }

  public loginUser = (route: string, body: UserForAuthenticationDto) => {
    return this.http.post<AuthResponseDto>(this.createCompleteRoute(route, this.envUrl.urlAddress), body);
  }

  public logout = () => {
    localStorage.removeItem("token");
    this.sendAuthStateChangeNotification(false);
  }

  public forgotPassword = (route: string, body: ForgotPasswordDto) => {
    return this.http.post(this.createCompleteRoute(route, this.envUrl.urlAddress), body);
  }

  public resetPassword = (route: string, body: ResetPasswordDto) => {
    return this.http.post(this.createCompleteRoute(route, this.envUrl.urlAddress), body);
  }

}
