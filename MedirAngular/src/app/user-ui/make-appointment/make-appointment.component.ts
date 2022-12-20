import { Component, OnInit } from '@angular/core';
import {AuthenticationService} from "../../shared/services/authentication.service";
import {ActivatedRoute, Router} from "@angular/router";
import {ErrorHandlerService} from "../../shared/services/error-handler.service";
import {UserUiService} from "../../shared/services/user-ui.service";
import { MedicAvailabilityForUserListVm } from 'src/app/_interfaces/userui/MedicAvailabilityForUserListVm';
import {Observable} from "rxjs";
import {CreateAppointmentDto} from "../../_interfaces/userui/CreateAppointmentDto";
import {MedicAvailabilityForUserLookUpDto} from "../../_interfaces/userui/MedicAvailabilityForUserLookUpDto";
import {FormControl, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-make-appointment',
  templateUrl: './make-appointment.component.html',
  styleUrls: ['./make-appointment.component.css']
})
export class MakeAppointmentComponent implements OnInit {

  minDate = new Date();

  form!: FormGroup;

  fastRegistered = false;

  public email: string | undefined = undefined;

  readyToAppoint = false;

  availabilities$: Observable<MedicAvailabilityForUserListVm>;

  medic: MedicAvailabilityForUserLookUpDto | undefined;

  name: string | undefined;

  positionId: string | undefined;

  medicId: string | undefined;

  polyclinicId: string | undefined;

  constructor(public authService: AuthenticationService,
              private router: Router,
              private activeRoute: ActivatedRoute,
              private repository: UserUiService,
              private errorHandler: ErrorHandlerService) {
  }

  ngOnInit(): void {
    this.activeRoute.queryParams
      .subscribe(params => {
          this.medicId = params['medicId'];
          this.positionId = params['positionId'];
          this.polyclinicId = params['polyclinicId'];
        }
      );
    this.form = new FormGroup({
      prescription: new FormControl()
    });
  }

  makeButtonReal= (medic: MedicAvailabilityForUserLookUpDto)=>{
    this.medic = medic;
    this.readyToAppoint = true;
  }

  dateChange=(date: Date) =>{
    this.availabilities$ = this.repository.getMedicAvailabilitiesForUserList(this.medicId!, this.positionId!, this.polyclinicId!, date);
    this.medic = undefined;
    this.readyToAppoint = false;
  }

  makeAppointment = ()=>{
    if(this.fastRegistered){
      const ap : CreateAppointmentDto = {
        medicAppointmentAvailabilityId: this.medic!.medicAppointmentAvailabilityId,
        userEmail: this.email!,
        prescription: this.form.value.prescription,
        date: this.medic!.date,
        time: this.medic!.timeStart
      }
      this.repository.createAppointment(ap)
        .subscribe({next: value => this.router.navigate(["/home"])});
    }
    else {
      const ap : CreateAppointmentDto = {
        medicAppointmentAvailabilityId: this.medic!.medicAppointmentAvailabilityId,
        userEmail: this.authService.getUserName(),
        prescription: this.form.value.prescription,
        date: this.medic!.date,
        time: this.medic!.timeStart
      }
      this.repository.createAppointment(ap)
        .subscribe({next: value => this.router.navigate(["/home"])});
    }

  }

  isFastRegistered=( mail: string)=>{
    if(mail !== undefined){
      this.email = mail;
      this.fastRegistered = true
    }

  }
}
