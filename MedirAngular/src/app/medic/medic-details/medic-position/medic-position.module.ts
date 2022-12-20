import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MedicPositionRoutingModule } from './medic-position-routing.module';
import { MedicPositionListComponent } from './medic-position-list/medic-position-list.component';
import { MedicPositionCreateComponent } from './medic-position-create/medic-position-create.component';
import { MedicPositionDeleteComponent } from './medic-position-delete/medic-position-delete.component';
import { MedicPositionDetailsComponent } from './medic-position-details/medic-position-details.component';
import {ReactiveFormsModule} from "@angular/forms";
import {BsDatepickerModule} from "ngx-bootstrap/datepicker";
import { MedicPositionUpdateComponent } from './medic-position-update/medic-position-update.component';


@NgModule({
  declarations: [
    MedicPositionListComponent,
    MedicPositionCreateComponent,
    MedicPositionDeleteComponent,
    MedicPositionDetailsComponent,
    MedicPositionUpdateComponent
  ],
  exports: [
    MedicPositionListComponent
  ],
  imports: [
    CommonModule,
    MedicPositionRoutingModule,
    ReactiveFormsModule,
    BsDatepickerModule
  ]
})
export class MedicPositionModule { }
