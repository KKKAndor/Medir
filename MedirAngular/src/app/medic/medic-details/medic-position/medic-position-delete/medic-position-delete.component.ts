import { Component, OnInit } from '@angular/core';
import {MedicPositionDetailsVm} from "../../../../_interfaces/medicposition/MedicPositionDetailsVm";
import {MedicPositionsRepositoryService} from "../../../../shared/services/medic-positions-repository.service";
import {ActivatedRoute, Router} from "@angular/router";
import {ErrorHandlerService} from "../../../../shared/services/error-handler.service";
import {HttpErrorResponse} from "@angular/common/http";

@Component({
  selector: 'app-medic-position-delete',
  templateUrl: './medic-position-delete.component.html',
  styleUrls: ['./medic-position-delete.component.css']
})
export class MedicPositionDeleteComponent implements OnInit {
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

  deleteMedicPosition= ()=>{
    this.repository.deleteMedicPosition(this.medicId,this.positionId)
      .subscribe({
        next: (_) => this.redirectToMedic()});
  }

  redirectToMedic = () => {
    const redirectUrl: string = `../medic/medic-details/${this.medicId}`;
    this.router.navigate([redirectUrl]);
  }

}
