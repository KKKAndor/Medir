import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MedicRoutingModule } from './medic-routing.module';
import { MedicListComponent } from './medic-list/medic-list.component';
import { MedicDetailsComponent } from './medic-details/medic-details.component';
import { MedicUpdateComponent } from './medic-update/medic-update.component';
import { MedicDeleteComponent } from './medic-delete/medic-delete.component';
import { MedicCreateComponent } from './medic-create/medic-create.component';
import { ReactiveFormsModule } from "@angular/forms";
import { MedicPositionModule } from './medic-details/medic-position/medic-position.module';
import { MedicPolyclinicModule } from './medic-details/medic-polyclinic/medic-polyclinic.module';
import {FileLoadModule} from "../shared/file-load/file-load.module";
import { MedicAvailabilityModule } from './medic-details/medic-availability/medic-availability.module';


@NgModule({
  declarations: [
    MedicListComponent,
    MedicDetailsComponent,
    MedicUpdateComponent,
    MedicDeleteComponent,
    MedicCreateComponent
  ],
    imports: [
        CommonModule,
        MedicRoutingModule,
        ReactiveFormsModule,
        MedicPositionModule,
        MedicPolyclinicModule,
        FileLoadModule,
        MedicAvailabilityModule
    ]
})
export class MedicModule { }
