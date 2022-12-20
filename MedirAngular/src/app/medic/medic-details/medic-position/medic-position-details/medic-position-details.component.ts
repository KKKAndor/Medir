import { Component, OnInit } from '@angular/core';
import {MedicDetailsVm} from "../../../../_interfaces/medics/MedicDetailsVm";
import {MedicsRepositoryService} from "../../../../shared/services/medics-repository.service";
import {ActivatedRoute, Router} from "@angular/router";
import {ErrorHandlerService} from "../../../../shared/services/error-handler.service";
import {HttpErrorResponse} from "@angular/common/http";
import { MedicPositionsRepositoryService } from 'src/app/shared/services/medic-positions-repository.service';
import { MedicPositionDetailsVm } from 'src/app/_interfaces/medicposition/MedicPositionDetailsVm';

@Component({
  selector: 'app-medic-position-details',
  templateUrl: './medic-position-details.component.html',
  styleUrls: ['./medic-position-details.component.css']
})
export class MedicPositionDetailsComponent implements OnInit {

  medicId: string | undefined;

  positionId: string | undefined;

  item!: MedicPositionDetailsVm | null;

  constructor(private repository: MedicPositionsRepositoryService, private router: Router,
              private activeRoute: ActivatedRoute, private errorHandler: ErrorHandlerService) { }

  ngOnInit() {
    this.getMedicPositionDetails()
  }

  getMedicPositionDetails = () => {
    this.activeRoute.queryParams
      .subscribe(params => {
          this.medicId = params['medicId'];
          this.positionId = params['positionId'];
        }
      );
    this.repository.getMedicPosition(this.medicId, this.positionId)
      .subscribe({
        next: (it: MedicPositionDetailsVm) => this.item = it,
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
