import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {MedicPositionListComponent} from "./medic-position-list/medic-position-list.component";
import {MedicPositionCreateComponent} from "./medic-position-create/medic-position-create.component";
import {MedicPositionDetailsComponent} from "./medic-position-details/medic-position-details.component";
import { MedicPositionDeleteComponent } from './medic-position-delete/medic-position-delete.component';
import {MedicPositionUpdateComponent} from "./medic-position-update/medic-position-update.component";

const routes: Routes = [
  { path: 'medic-position-list', component: MedicPositionListComponent },
  { path: 'medic-position-create', component: MedicPositionCreateComponent },
  { path: 'medic-position-details', component: MedicPositionDetailsComponent },
  { path: 'medic-position-delete', component: MedicPositionDeleteComponent },
  { path: 'medic-position-update', component: MedicPositionUpdateComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MedicPositionRoutingModule { }
