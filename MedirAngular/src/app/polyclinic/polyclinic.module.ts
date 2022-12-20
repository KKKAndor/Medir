import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PolyclinicRoutingModule } from './polyclinic-routing.module';
import { PolyclinicListComponent } from './polyclinic-list/polyclinic-list.component';
import { PolyclinicCreateComponent } from './polyclinic-create/polyclinic-create.component';
import { ReactiveFormsModule } from "@angular/forms";
import { PolyclinicDetailsComponent } from './polyclinic-details/polyclinic-details.component';
import { PolyclinicDeleteComponent } from './polyclinic-delete/polyclinic-delete.component';
import { PolyclinicUpdateComponent } from './polyclinic-update/polyclinic-update.component';
import {AngularYandexMapsModule} from "angular8-yandex-maps";
import {FileLoadModule} from "../shared/file-load/file-load.module";



@NgModule({
  declarations: [
    PolyclinicListComponent,
    PolyclinicCreateComponent,
    PolyclinicDetailsComponent,
    PolyclinicDeleteComponent,
    PolyclinicUpdateComponent
  ],
  imports: [
    CommonModule,
    PolyclinicRoutingModule,
    ReactiveFormsModule,
    AngularYandexMapsModule,
    FileLoadModule
  ]
})
export class PolyclinicModule { }
