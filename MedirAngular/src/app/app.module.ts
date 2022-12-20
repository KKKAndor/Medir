import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router'
import { HttpClientModule, HttpHeaders, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CollapseModule } from 'ngx-bootstrap/collapse';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { MenuComponent } from './menu/menu.component';
import { NotFoundComponent } from './error-pages/not-found/not-found.component';
import { ErrorHandlerService } from './shared/services/error-handler.service';
import { JwtModule } from '@auth0/angular-jwt';
import { ForbiddenComponent } from './forbidden/forbidden.component';
import { AdminGuard } from './shared/guards/admin.guard';
import { CitiesRepositoryService} from "./shared/services/cities-repository.service";
import { CityModule } from './city/city.module';
import { InternalServerComponent } from './error-pages/internal-server/internal-server.component';
import {AuthGuard} from "./shared/guards/auth.guard";
import {PositionsRepositoryService} from "./shared/services/positions-repository.service";
import { PositionModule } from './position/position.module';
import { PolyclinicModule } from './polyclinic/polyclinic.module';
import { MedicModule } from './medic/medic.module';
import { AngularYandexMapsModule } from 'angular8-yandex-maps';
import { FileLoadModule } from './shared/file-load/file-load.module';
import { UserUiModule } from './user-ui/user-ui.module';

export function tokenGetter() {
  return localStorage.getItem("token");
}

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    MenuComponent,
    NotFoundComponent,
    ForbiddenComponent,
    InternalServerComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    CollapseModule.forRoot(),
    RouterModule.forRoot([
      {path: 'home', component: HomeComponent},
      {
        path: 'city',
        loadChildren: () => import('./city/city.module').then(m => m.CityModule),
        canActivate: [AdminGuard]
      },
      {
        path: 'position',
        loadChildren: () => import('./position/position.module').then(m => m.PositionModule),
        canActivate: [AdminGuard]
      },
      {
        path: 'polyclinic',
        loadChildren: () => import('./polyclinic/polyclinic.module').then(m => m.PolyclinicModule),
        canActivate: [AdminGuard]
      },
      {
        path: 'medic',
        loadChildren: () => import('./medic/medic.module').then(m => m.MedicModule),
        canActivate: [AdminGuard]
      },
      {path: 'forbidden', component: ForbiddenComponent},
      {
        path: 'authentication',
        loadChildren: () => import('./authentication/authentication.module').then(m => m.AuthenticationModule)
      },
      {
        path: 'user-ui',
        loadChildren: () => import('./user-ui/user-ui.module').then(m => m.UserUiModule)
      },
      {path: '404', component: NotFoundComponent},
      {path: '500', component: InternalServerComponent},
      {path: '', redirectTo: '/home', pathMatch: 'full'},
      {path: '**', redirectTo: '/404', pathMatch: 'full'}
    ]),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["https://localhost:7192"],
        disallowedRoutes: []
      }
    }),
    CityModule,
    PositionModule,
    PolyclinicModule,
    MedicModule,
    AngularYandexMapsModule,
    FileLoadModule,
    UserUiModule
  ],
  providers: [
    AuthGuard,
    AdminGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorHandlerService,
      multi: true
    },
    CitiesRepositoryService,
    PositionsRepositoryService
  ],
  exports: [
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
