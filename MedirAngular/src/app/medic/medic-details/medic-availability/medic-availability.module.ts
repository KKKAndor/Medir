import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MedicAvailabilityRoutingModule } from './medic-availability-routing.module';
import { MedicAvailabilityListComponent } from './medic-availability-list/medic-availability-list.component';
import { MedicAvailabilityCreateComponent } from './medic-availability-create/medic-availability-create.component';
import {ReactiveFormsModule} from "@angular/forms";
import {TimepickerModule} from "ngx-bootstrap/timepicker";
import {BsDatepickerModule} from "ngx-bootstrap/datepicker";
import { MedicAvailabilityDeleteComponent } from './medic-availability-delete/medic-availability-delete.component';


@NgModule({
    declarations: [
        MedicAvailabilityListComponent,
        MedicAvailabilityCreateComponent,
        MedicAvailabilityDeleteComponent
    ],
    exports: [
        MedicAvailabilityListComponent
    ],
  imports: [
    CommonModule,
    MedicAvailabilityRoutingModule,
    ReactiveFormsModule,
    TimepickerModule,
    BsDatepickerModule
  ]
})
export class MedicAvailabilityModule { }
