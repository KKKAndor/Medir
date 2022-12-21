import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {MedicPositionsRepositoryService} from "../../../../shared/services/medic-positions-repository.service";
import {ActivatedRoute, Router} from "@angular/router";
import {ErrorHandlerService} from "../../../../shared/services/error-handler.service";
import {HttpErrorResponse} from "@angular/common/http";
import {CreateMedicPositionDto} from "../../../../_interfaces/medicposition/CreateMedicPositionDto";
import {UpdateMedicPositionDto} from "../../../../_interfaces/medicposition/UpdateMedicPositionDto";
import {MedicPositionDetailsVm} from "../../../../_interfaces/medicposition/MedicPositionDetailsVm";

@Component({
  selector: 'app-medic-position-update',
  templateUrl: './medic-position-update.component.html',
  styleUrls: ['./medic-position-update.component.css']
})
export class MedicPositionUpdateComponent implements OnInit {

  item!: MedicPositionDetailsVm | null;

  medicId: string | undefined;

  dateOn: string| undefined;

  form!: FormGroup;

  positionId: string | undefined;

  constructor(private repository: MedicPositionsRepositoryService,
              private activeRoute: ActivatedRoute,
              private errorHandler: ErrorHandlerService,
              private router: Router) { }

  ngOnInit(): void {
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
    this.dateOn = this.item?.dateOnPosition.toString();

    this.form = new FormGroup({
      dateOnPosition: new FormControl('', [Validators.required])

    });
  }

  public validateControl = (controlName: string) => {
    return this.form.get(controlName)!.invalid && this.form.get(controlName)!.touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.form.get(controlName)!.hasError(errorName)
  }

  updateMedicPosition = (FormValue: any) => {
    const item: UpdateMedicPositionDto = {
      medicId: this.medicId!,
      positionId: this.positionId!,
      dateOnPosition: FormValue.dateOnPosition
    }
    this.repository.updateMedicPosition(item)
      .subscribe({
        next: (_) => this.redirectToMedic(),
        error: (err: HttpErrorResponse) => {
          this.errorHandler.handleError(err);
        }});
  }

  redirectToMedic = () => {
    const redirectUrl: string = `../medic/medic-details/${this.medicId}`;
    this.router.navigate([redirectUrl]);
  }
}
