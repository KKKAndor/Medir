import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {ErrorHandlerService} from "../../shared/services/error-handler.service";
import {ActivatedRoute, Router} from "@angular/router";
import { MedicsRepositoryService } from 'src/app/shared/services/medics-repository.service';
import { CreateMedicDto } from 'src/app/_interfaces/medics/CreateMedicDto';
import {CityDetailsVm} from "../../_interfaces/cities/CityDetailsVm";
import {UpdateCityDto} from "../../_interfaces/cities/UpdateCityDto";
import {HttpErrorResponse} from "@angular/common/http";
import { MedicDetailsVm } from 'src/app/_interfaces/medics/MedicDetailsVm';
import { UpdateMedicDto } from 'src/app/_interfaces/medics/UpdateMedicDto';

@Component({
  selector: 'app-medic-create',
  templateUrl: './medic-create.component.html',
  styleUrls: ['./medic-create.component.css']
})
export class MedicCreateComponent implements OnInit {

  response!: {dbPath: ''};

  form!: FormGroup;

  constructor(private repository: MedicsRepositoryService, private errorHandler: ErrorHandlerService,
              private router: Router) { }

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

  createMedic = (FormValue: any) => {

    const item: CreateMedicDto = {
      medicFullName: FormValue.medicFullName,
      shortDescription: FormValue.shortDescription,
      fullDescription: FormValue.fullDescription,
      medicPhoneNumber: FormValue.medicPhoneNumber,
      medicPhoto: this.response.dbPath
    }

    this.repository.createMedic(item)
      .subscribe({
        next: (_) => this.redirectToMedicList()});

  }

  redirectToMedicList = () => {
    this.router.navigate(['/medic/medic-list']);
  }

  uploadFinished = (event: any) => {
    this.response = event;
  }
}
