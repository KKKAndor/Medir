import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {MedicPolyclinicsRepositoryService} from "../../../../shared/services/medic-polyclinics-repository.service";
import {ActivatedRoute, Router} from "@angular/router";
import {ErrorHandlerService} from "../../../../shared/services/error-handler.service";
import { UpdateMedicPolyclinicDto } from 'src/app/_interfaces/medicpolyclinics/UpdateMedicPolyclinicDto';
import {HttpErrorResponse} from "@angular/common/http";
import {CreateMedicPolyclinicDto} from "../../../../_interfaces/medicpolyclinics/CreateMedicPolyclinicDto";
import {MedicPolyclinicDetailsVm} from "../../../../_interfaces/medicpolyclinics/MedicPolyclinicDetailsVm";

@Component({
  selector: 'app-medic-polyclinic-update',
  templateUrl: './medic-polyclinic-update.component.html',
  styleUrls: ['./medic-polyclinic-update.component.css']
})
export class MedicPolyclinicUpdateComponent implements OnInit {

  item!: MedicPolyclinicDetailsVm | null;

  medicId: string | undefined;

  form!: FormGroup;

  polyclinicId: string | undefined;

  constructor(private repository: MedicPolyclinicsRepositoryService,
              private activeRoute: ActivatedRoute,
              private errorHandler: ErrorHandlerService,
              private router: Router) { }

  ngOnInit(): void {
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

    this.form = new FormGroup({
      price: new FormControl('', [Validators.required])
    });
  }

  public validateControl = (controlName: string) => {
    return this.form.get(controlName)!.invalid && this.form.get(controlName)!.touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.form.get(controlName)!.hasError(errorName)
  }

  updateMedicPolyclinic = (FormValue: any) => {
    const item: UpdateMedicPolyclinicDto = {
      medicId: this.medicId!,
      polyclinicId: this.polyclinicId!,
      price: FormValue.price
    }

    this.repository.updateMedicPolyclinic(item)
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
