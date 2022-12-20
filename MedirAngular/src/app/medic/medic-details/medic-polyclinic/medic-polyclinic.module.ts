import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MedicPolyclinicRoutingModule } from './medic-polyclinic-routing.module';
import { MedicPolyclinicListComponent } from './medic-polyclinic-list/medic-polyclinic-list.component';
import { MedicPolyclinicDetailsComponent } from './medic-polyclinic-details/medic-polyclinic-details.component';
import { MedicPolyclinicDeleteComponent } from './medic-polyclinic-delete/medic-polyclinic-delete.component';
import { MedicPolyclinicCreateComponent } from './medic-polyclinic-create/medic-polyclinic-create.component';
import {ReactiveFormsModule} from "@angular/forms";
import { MedicPolyclinicUpdateComponent } from './medic-polyclinic-update/medic-polyclinic-update.component';


@NgModule({
  declarations: [
    MedicPolyclinicListComponent,
    MedicPolyclinicDetailsComponent,
    MedicPolyclinicDeleteComponent,
    MedicPolyclinicCreateComponent,
    MedicPolyclinicUpdateComponent
  ],
  exports: [
    MedicPolyclinicListComponent
  ],
  imports: [
    CommonModule,
    MedicPolyclinicRoutingModule,
    ReactiveFormsModule
  ]
})
export class MedicPolyclinicModule { }
