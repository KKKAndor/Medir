import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {ErrorHandlerService} from "../../../../shared/services/error-handler.service";
import { MedicPolyclinicsRepositoryService } from 'src/app/shared/services/medic-polyclinics-repository.service';
import { PolyclinicLookUpDto } from 'src/app/_interfaces/polyclinics/PolyclinicLookUpDto';
import { PolyclinicsRepositoryService } from 'src/app/shared/services/polyclinics-repository.service';
import { CreateMedicPolyclinicDto } from 'src/app/_interfaces/medicpolyclinics/CreateMedicPolyclinicDto';

@Component({
  selector: 'app-medic-polyclinic-create',
  templateUrl: './medic-polyclinic-create.component.html',
  styleUrls: ['./medic-polyclinic-create.component.css']
})
export class MedicPolyclinicCreateComponent implements OnInit {

  id: string | undefined;

  form!: FormGroup;

  Polyclinics: PolyclinicLookUpDto[] | undefined;

  selected: string | undefined;

  constructor(private repository: MedicPolyclinicsRepositoryService,
              private polyclinicsRepositoryService: PolyclinicsRepositoryService,
              private activeRoute: ActivatedRoute,
              private errorHandler: ErrorHandlerService,
              private router: Router) { }

  ngOnInit(): void {
    this.activeRoute.queryParams
      .subscribe(params => {
          this.id = params['medicId']
        }
      );

    this.polyclinicsRepositoryService.getAllPolyclinics()
      .subscribe(value => this.Polyclinics = value.polyclinics);

    this.form = new FormGroup({
      polyclinicId: new FormControl('', [Validators.required]),
      price: new FormControl('', [Validators.required])

    });
  }

  public validateControl = (controlName: string) => {
    return this.form.get(controlName)!.invalid && this.form.get(controlName)!.touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.form.get(controlName)!.hasError(errorName)
  }

  createMedicPolyclinic = (FormValue: any) => {
    if(this.selected !== undefined){
      const item: CreateMedicPolyclinicDto = {
        medicId: this.id!,
        polyclinicId: FormValue.polyclinicId,
        price: FormValue.price
      }

      this.repository.createMedicPolyclinic(item)
        .subscribe({
          next: (_) => this.redirectToMedic()});
    }
  }

  redirectToMedic = () => {
    const redirectUrl: string = `../medic/medic-details/${this.id}`;
    this.router.navigate([redirectUrl]);
  }

  changed(e:any){
    this.selected = e.target.value;
  }

}
