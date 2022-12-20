import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PositionRoutingModule } from './position-routing.module';
import { PositionListComponent } from './position-list/position-list.component';
import { PositionDetailsComponent } from './position-details/position-details.component';
import { PositionCreateComponent } from './position-create/position-create.component';
import {ReactiveFormsModule} from "@angular/forms";
import { PositionUpdateComponent } from './position-update/position-update.component';
import { PositionDeleteComponent } from './position-delete/position-delete.component';


@NgModule({
  declarations: [
    PositionListComponent,
    PositionDetailsComponent,
    PositionCreateComponent,
    PositionUpdateComponent,
    PositionDeleteComponent
  ],
  imports: [
    CommonModule,
    PositionRoutingModule,
    ReactiveFormsModule
  ]
})
export class PositionModule { }
