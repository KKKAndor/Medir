import { Component, OnInit } from '@angular/core';
import {MedicPositionDetailsVm} from "../../../../_interfaces/medicposition/MedicPositionDetailsVm";
import {MedicPositionsRepositoryService} from "../../../../shared/services/medic-positions-repository.service";
import {ActivatedRoute, Router} from "@angular/router";
import {ErrorHandlerService} from "../../../../shared/services/error-handler.service";
import {HttpErrorResponse} from "@angular/common/http";
import { MedicPolyclinicDetailsVm } from 'src/app/_interfaces/medicpolyclinics/MedicPolyclinicDetailsVm';
import { MedicPolyclinicsRepositoryService } from 'src/app/shared/services/medic-polyclinics-repository.service';

@Component({
  selector: 'app-medic-polyclinic-details',
  templateUrl: './medic-polyclinic-details.component.html',
  styleUrls: ['./medic-polyclinic-details.component.css']
})
export class MedicPolyclinicDetailsComponent implements OnInit {

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

  redirectToMedic = () => {
    const redirectUrl: string = `../medic/medic-details/${this.medicId}`;
    this.router.navigate([redirectUrl]);
  }
}
