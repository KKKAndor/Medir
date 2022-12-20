import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {MedicAvailabilityCreateComponent} from "./medic-availability-create/medic-availability-create.component";
import { MedicAvailabilityDeleteComponent } from './medic-availability-delete/medic-availability-delete.component';
import {MedicAvailabilityListComponent} from "./medic-availability-list/medic-availability-list.component";

const routes: Routes = [
  { path: 'medic-availability-list', component: MedicAvailabilityListComponent },
  { path: 'medic-availability-create', component: MedicAvailabilityCreateComponent },
  { path: 'medic-availability-delete', component: MedicAvailabilityDeleteComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MedicAvailabilityRoutingModule { }
