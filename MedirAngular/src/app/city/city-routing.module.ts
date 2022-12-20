import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CityListComponent } from './city-list/city-list.component';
import { CityDetailsComponent} from "./city-details/city-details.component";
import { CityCreateComponent } from "./city-create/city-create.component";
import { CityUpdateComponent } from './city-update/city-update.component';
import { CityDeleteComponent } from './city-delete/city-delete.component';

const routes: Routes = [
  { path:'сity-list', component: CityListComponent },
  { path: 'city-details/:id', component: CityDetailsComponent},
  { path: 'city-create', component: CityCreateComponent},
  { path: 'city-update/:id', component: CityUpdateComponent },
  { path: 'city-delete/:id', component: CityDeleteComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CityRoutingModule { }
