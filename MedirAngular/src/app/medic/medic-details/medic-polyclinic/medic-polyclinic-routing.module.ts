import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MedicPolyclinicCreateComponent } from './medic-polyclinic-create/medic-polyclinic-create.component';
import { MedicPolyclinicDeleteComponent } from './medic-polyclinic-delete/medic-polyclinic-delete.component';
import {MedicPolyclinicDetailsComponent} from './medic-polyclinic-details/medic-polyclinic-details.component';
import { MedicPolyclinicListComponent } from './medic-polyclinic-list/medic-polyclinic-list.component';
import { MedicPolyclinicUpdateComponent } from './medic-polyclinic-update/medic-polyclinic-update.component';

const routes: Routes = [
  { path: 'medic-polyclinic-list', component: MedicPolyclinicListComponent },
  { path: 'medic-polyclinic-create', component: MedicPolyclinicCreateComponent },
  { path: 'medic-polyclinic-details', component: MedicPolyclinicDetailsComponent },
  { path: 'medic-polyclinic-delete', component: MedicPolyclinicDeleteComponent },
  { path: 'medic-polyclinic-update', component: MedicPolyclinicUpdateComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MedicPolyclinicRoutingModule { }
