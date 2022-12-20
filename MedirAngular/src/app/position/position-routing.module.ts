import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PositionCreateComponent } from './position-create/position-create.component';
import { PositionDeleteComponent } from './position-delete/position-delete.component';
import { PositionDetailsComponent } from './position-details/position-details.component';
import { PositionListComponent } from './position-list/position-list.component';
import { PositionUpdateComponent } from './position-update/position-update.component';

const routes: Routes = [
  { path:'position-list', component: PositionListComponent },
  { path: 'position-details/:id', component: PositionDetailsComponent},
  { path: 'position-create', component: PositionCreateComponent},
  { path: 'position-update/:id', component: PositionUpdateComponent },
  { path: 'position-delete/:id', component: PositionDeleteComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PositionRoutingModule { }
