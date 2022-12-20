import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../shared/services/authentication.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {

  public isUserAuthenticated!: boolean;

  public isUserAdmin!: boolean;

  isCollapsed: boolean = false;

  constructor(private authService: AuthenticationService, private router: Router) {
    this.authService.authChanged
      .subscribe(res => {
        this.isUserAuthenticated = res;
        this.isUserAdmin = this.authService.isUserAdmin();
      });
  }

  ngOnInit(): void {
    this.authService.authChanged
      .subscribe(res => {
        this.isUserAuthenticated = res;
        this.isUserAdmin = this.authService.isUserAdmin();
      })
  }

  public logout = () => {
    this.authService.logout();
    this.router.navigate(["/"]);
  }
}
