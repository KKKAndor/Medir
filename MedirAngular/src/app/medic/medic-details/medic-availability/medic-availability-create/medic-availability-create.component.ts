import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {PolyclinicsRepositoryService} from "../../../../shared/services/polyclinics-repository.service";
import {ActivatedRoute, Router} from "@angular/router";
import {ErrorHandlerService} from "../../../../shared/services/error-handler.service";
import {MedicAvailabilityRepositoryService} from "../../../../shared/services/medic-availability-repository.service";
import {PolyclinicLookUpDto} from "../../../../_interfaces/polyclinics/PolyclinicLookUpDto";
import { PositionsRepositoryService } from 'src/app/shared/services/positions-repository.service';
import { PositionLookUpDto } from 'src/app/_interfaces/positions/PositionLookUpDto';
import {CreateMedicAvailabilityDto} from "../../../../_interfaces/medicavailability/CreateMedicAvailabilityDto";
import { CreateMedicPolyclinicDto } from 'src/app/_interfaces/medicpolyclinics/CreateMedicPolyclinicDto';
import {MedicPolyclinicsRepositoryService} from "../../../../shared/services/medic-polyclinics-repository.service";
import {MedicPositionsRepositoryService} from "../../../../shared/services/medic-positions-repository.service";
import { MedicPolyclinicLookUpDto } from 'src/app/_interfaces/medicpolyclinics/MedicPolyclinicLookUpDto';
import { MedicPositionLookUpDto } from 'src/app/_interfaces/medicposition/MedicPositionLookUpDto';

@Component({
  selector: 'app-medic-availability-create',
  templateUrl: './medic-availability-create.component.html',
  styleUrls: ['./medic-availability-create.component.css']
})
export class MedicAvailabilityCreateComponent implements OnInit {

  id: string | undefined;

  form!: FormGroup;

  Polyclinics: MedicPolyclinicLookUpDto[] | undefined

  Positions: MedicPositionLookUpDto[] | undefined;

  selectedPolyclinic: string | undefined;

  selectedPosition: string | undefined;

  constructor(private repository: MedicAvailabilityRepositoryService,
              private polyclinicsRepositoryService: MedicPolyclinicsRepositoryService,
              private positionsRepositoryService: MedicPositionsRepositoryService,
              private activeRoute: ActivatedRoute,
              private errorHandler: ErrorHandlerService,
              private router: Router) { }

  ngOnInit(): void {
    this.activeRoute.queryParams
      .subscribe(params => {
          this.id = params['medicId']
        }
      );

    this.polyclinicsRepositoryService.getAllMedicPolyclinics(this.id)
      .subscribe(value => this.Polyclinics = value.medicPolyclinics);

    this.positionsRepositoryService.getAllMedicPositions(this.id)
      .subscribe(value => this.Positions = value.medicPositions);

    this.form = new FormGroup({
      polyclinicId: new FormControl('', [Validators.required]),
      positionId: new FormControl('', [Validators.required]),
      date: new FormControl('', [Validators.required]),
      timeStart: new FormControl('', [Validators.required]),
      timeEnd: new FormControl('', [Validators.required]),
      receptionMinutesTime: new FormControl('', [Validators.required])
    });
  }

  public validateControl = (controlName: string) => {
    return this.form.get(controlName)!.invalid && this.form.get(controlName)!.touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.form.get(controlName)!.hasError(errorName)
  }

  redirectToMedic = () => {
    const redirectUrl: string = `../medic/medic-details/${this.id}`;
    this.router.navigate([redirectUrl]);
  }

  changedPosition(e:any){
    this.selectedPosition = e.target.value;
  }

  changedPolyclinic(e:any){
    this.selectedPolyclinic = e.target.value;
  }

  createMedicAvailability(FormValue: any){
    if(this.selectedPolyclinic !== undefined && this.selectedPosition !== undefined){
      const item: CreateMedicAvailabilityDto = {
        medicId: this.id!,
        positionId: this.selectedPosition,
        polyclinicId: this.selectedPolyclinic,
        date: FormValue.date,
        timeStart: FormValue.timeStart,
        timeEnd: FormValue.timeEnd,
        receptionMinutesTime: FormValue.receptionMinutesTime
      }
      this.repository.createMedicAvailability(item)
        .subscribe({
          next: (_) => this.redirectToMedic()});
    }

  }
}
