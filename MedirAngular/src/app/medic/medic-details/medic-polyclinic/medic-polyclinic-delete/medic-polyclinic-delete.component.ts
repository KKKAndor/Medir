import { Component, OnInit } from '@angular/core';
import {MedicPolyclinicDetailsVm} from "../../../../_interfaces/medicpolyclinics/MedicPolyclinicDetailsVm";
import {MedicPolyclinicsRepositoryService} from "../../../../shared/services/medic-polyclinics-repository.service";
import {ActivatedRoute, Router} from "@angular/router";
import {ErrorHandlerService} from "../../../../shared/services/error-handler.service";
import {HttpErrorResponse} from "@angular/common/http";

@Component({
  selector: 'app-medic-polyclinic-delete',
  templateUrl: './medic-polyclinic-delete.component.html',
  styleUrls: ['./medic-polyclinic-delete.component.css']
})
export class MedicPolyclinicDeleteComponent implements OnInit {

  medicId: string | undefined;

  polyclinicId: string | undefined;

  item!: MedicPolyclinicDetailsVm | null;

  constructor(private repository: MedicPolyclinicsRepositoryService, private router: Router,
              private activeRoute: ActivatedRoute, private errorHandler: ErrorHandlerService) { }

  ngOnInit() {
    this.getMedicPolyclinicDetails()
  }

  getMedicPolyclinicDetails = () => {
    this.activeRoute.queryParams
      .subscribe(params => {
          this.medicId = params['medicId'];
          this.polyclinicId = params['polyclinicId'];
        }
      );
    this.repository.getMedicPolyclinic(this.medicId, this.polyclinicId)
      .subscribe({
        next: (it: MedicPolyclinicDetailsVm) => this.item = it,
        error: (err: HttpErrorResponse) => {
          this.errorHandler.handleError(err);
        }
      })
  }

  deleteMedicPolyclinic= ()=>{
    this.repository.deleteMedicPolyclinic(this.medicId,this.polyclinicId)
      .subscribe({
        next: (_) => this.redirectToMedic()});
  }

  redirectToMedic = () => {
    const redirectUrl: string = `../medic/medic-details/${this.medicId}`;
    this.router.navigate([redirectUrl]);
  }

}
