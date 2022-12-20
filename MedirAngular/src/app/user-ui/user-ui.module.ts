import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserUiRoutingModule } from './user-ui-routing.module';
import { MedicsUserComponent } from './medics-user/medics-user.component';
import { PositionsUserComponent } from './positions-user/positions-user.component';
import {MatGridListModule} from "@angular/material/grid-list";
import { PolyclinicOnMapUserComponent } from './polyclinic-on-map-user/polyclinic-on-map-user.component';
import {AngularYandexMapsModule} from "angular8-yandex-maps";
import { MakeAppointmentComponent } from './make-appointment/make-appointment.component';
import {AuthenticationModule} from "../authentication/authentication.module";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import { FastRegisterComponent } from './fast-register/fast-register.component';
import { PersonalAreaComponent } from './personal-area/personal-area.component';


@NgModule({
  declarations: [
    MedicsUserComponent,
    PositionsUserComponent,
    PolyclinicOnMapUserComponent,
    MakeAppointmentComponent,
    FastRegisterComponent,
    PersonalAreaComponent
  ],
  exports: [
    PositionsUserComponent
  ],
  imports: [
    CommonModule,
    UserUiRoutingModule,
    MatGridListModule,
    AngularYandexMapsModule,
    AuthenticationModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class UserUiModule { }
