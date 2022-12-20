import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CityRoutingModule } from './city-routing.module';
import { CityListComponent } from './city-list/city-list.component';
import { CityDetailsComponent } from './city-details/city-details.component';
import { CityCreateComponent } from './city-create/city-create.component';
import { ReactiveFormsModule } from '@angular/forms';
import { CityUpdateComponent } from './city-update/city-update.component';
import { CityDeleteComponent } from './city-delete/city-delete.component';


@NgModule({
  declarations: [
    CityListComponent,
    CityDetailsComponent,
    CityCreateComponent,
    CityUpdateComponent,
    CityDeleteComponent
  ],
  imports: [
    CommonModule,
    CityRoutingModule,
    ReactiveFormsModule
  ]
})
export class CityModule { }
