import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MedicCreateComponent } from './medic-create/medic-create.component';
import { MedicDeleteComponent } from './medic-delete/medic-delete.component';
import { MedicDetailsComponent } from './medic-details/medic-details.component';
import { MedicListComponent } from './medic-list/medic-list.component';
import {MedicUpdateComponent} from './medic-update/medic-update.component';
import {AdminGuard} from "../shared/guards/admin.guard";

const routes: Routes = [
  { path: 'medic-list', component: MedicListComponent },
  { path: 'medic-details/:id', component: MedicDetailsComponent},
  { path: 'medic-position', loadChildren: () =>
      import('./medic-details/medic-position/medic-position.module').then(m => m.MedicPositionModule), canActivate: [AdminGuard] },
  { path: 'medic-polyclinic', loadChildren: () =>
      import('./medic-details/medic-polyclinic/medic-polyclinic.module').then(m => m.MedicPolyclinicModule), canActivate: [AdminGuard] },
  { path: 'medic-availability', loadChildren: () =>
      import('./medic-details/medic-availability/medic-availability.module').then(m => m.MedicAvailabilityModule), canActivate: [AdminGuard] },
  { path: 'medic-create', component: MedicCreateComponent},
  { path: 'medic-update/:id', component: MedicUpdateComponent },
  { path: 'medic-delete/:id', component: MedicDeleteComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MedicRoutingModule { }
