import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MedicsUserComponent } from './medics-user/medics-user.component';
import {PositionsUserComponent} from "./positions-user/positions-user.component";
import {PolyclinicOnMapUserComponent} from "./polyclinic-on-map-user/polyclinic-on-map-user.component";
import {MakeAppointmentComponent} from "./make-appointment/make-appointment.component";
import {FastRegisterComponent} from "./fast-register/fast-register.component";
import {PersonalAreaComponent} from "./personal-area/personal-area.component";

const routes: Routes = [
  { path: 'positions-user', component: PositionsUserComponent },
  { path: 'medics-user', component: MedicsUserComponent },
  { path: 'polyclinic-on-map', component: PolyclinicOnMapUserComponent},
  { path: 'make-appointment', component: MakeAppointmentComponent},
  { path: 'fast-register', component: FastRegisterComponent},
  { path: 'personal-area', component: PersonalAreaComponent},
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class UserUiRoutingModule { }
