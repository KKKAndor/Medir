import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {MedicsRepositoryService} from "../../shared/services/medics-repository.service";
import {ErrorHandlerService} from "../../shared/services/error-handler.service";
import {ActivatedRoute, Router} from "@angular/router";
import {MedicDetailsVm} from "../../_interfaces/medics/MedicDetailsVm";
import {UpdateMedicDto} from "../../_interfaces/medics/UpdateMedicDto";
import {HttpErrorResponse} from "@angular/common/http";

@Component({
  selector: 'app-medic-update',
  templateUrl: './medic-update.component.html',
  styleUrls: ['./medic-update.component.css']
})
export class MedicUpdateComponent implements OnInit {

  response!: {dbPath: ''};

  form!: FormGroup;

  constructor(private repository: MedicsRepositoryService, private errorHandler: ErrorHandlerService,
              private router: Router, private activeRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.form = new FormGroup({
      medicFullName: new FormControl('', [Validators.required, Validators.maxLength(100)]),
      shortDescription: new FormControl('', [Validators.required, Validators.maxLength(100)]),
      fullDescription: new FormControl('', [Validators.required, Validators.maxLength(250)]),
      medicPhoneNumber: new FormControl('', [Validators.required, Validators.pattern("^((\\+373-?)|0)?[0-9]{8}$")])
    });
  }

  public validateControl = (controlName: string) => {
    return this.form.get(controlName)!.invalid && this.form.get(controlName)!.touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.form.get(controlName)!.hasError(errorName)
  }

  updateMedic = (FormValue: any) => {

    const id: string = this.activeRoute.snapshot.params['id'];
    this.repository.getMedic(id)
      .subscribe({
        next: (it: MedicDetailsVm) => {
          const item: UpdateMedicDto = {
            medicId: it.medicId,
            medicFullName: FormValue.medicFullName,
            shortDescription: FormValue.shortDescription,
            fullDescription: FormValue.fullDescription,
            medicPhoneNumber: FormValue.medicPhoneNumber,
            medicPhoto: this.response.dbPath
          }
          this.repository.updateMedic(item)
            .subscribe({
              next: (_) => this.redirectToMedicList()});
        },
        error: (err: HttpErrorResponse) => {
          this.errorHandler.handleError(err);
        }
      })
  }

  redirectToMedicList = () => {
    this.router.navigate(['/medic/medic-list']);
  }

  uploadFinished = (event: any) => {
    this.response = event;
  }
}
