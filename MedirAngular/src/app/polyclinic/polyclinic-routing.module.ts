import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PolyclinicCreateComponent } from './polyclinic-create/polyclinic-create.component';
import { PolyclinicDeleteComponent } from './polyclinic-delete/polyclinic-delete.component';
import { PolyclinicDetailsComponent } from './polyclinic-details/polyclinic-details.component';
import { PolyclinicListComponent } from './polyclinic-list/polyclinic-list.component';
import { PolyclinicUpdateComponent } from './polyclinic-update/polyclinic-update.component';

const routes: Routes = [
  { path: 'polyclinic-list', component: PolyclinicListComponent },
  { path: 'polyclinic-details/:id', component: PolyclinicDetailsComponent},
  { path: 'polyclinic-create', component: PolyclinicCreateComponent},
  { path: 'polyclinic-update/:id', component: PolyclinicUpdateComponent },
  { path: 'polyclinic-delete/:id', component: PolyclinicDeleteComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PolyclinicRoutingModule { }
