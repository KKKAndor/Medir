import {Component, Input, OnInit} from '@angular/core';
import {UserUiService} from "../../shared/services/user-ui.service";
import {ErrorHandlerService} from "../../shared/services/error-handler.service";
import {HttpErrorResponse} from "@angular/common/http";
import {MedicsForUserListVm} from "../../_interfaces/userui/MedicsForUserListVm";
import { MedicForUserLookUpDto } from 'src/app/_interfaces/userui/MedicForUserLookUpDto';
import {ActivatedRoute, Router} from "@angular/router";
import {PolyclinicForUserLookUpDto} from "../../_interfaces/userui/PolyclinicForUserLookUpDto";
import {MedicPolyclinicLookUpDto} from "../../_interfaces/medicpolyclinics/MedicPolyclinicLookUpDto";
import {MedicPolyclinicForUserLookUpDto} from "../../_interfaces/userui/MedicPolyclinicForUserLookUpDto";

@Component({
  selector: 'app-medics-user',
  templateUrl: './medics-user.component.html',
  styleUrls: ['./medics-user.component.css']
})
export class MedicsUserComponent implements OnInit {

  cityId: string | undefined  = "undefined";

  positionId: string | undefined;

  medicsList: MedicForUserLookUpDto[] | undefined;

  constructor(private userService: UserUiService,
              private errorHandler: ErrorHandlerService,
              private activeRoute: ActivatedRoute,
              private router: Router) { }

  ngOnInit(): void {
    this.activeRoute.queryParams
      .subscribe(params => {
          this.cityId = params['cityId'];
          this.positionId = params['positionId'];
        }
      );
    if(this.cityId == "undefined"){
      this.userService.getAllMedicsForUserList(this.positionId,
        undefined, 1,10,
        undefined, undefined)
        .subscribe({
          next: (it:MedicsForUserListVm) => this.medicsList = it.medicsForUser,
          error: (err: HttpErrorResponse) => {
            this.errorHandler.handleError(err);
          }
        })
    }
    else{
      this.userService.getAllMedicsForUserList(
        this.positionId, this.cityId,
        1,10,
        undefined, undefined)
        .subscribe({
          next: (it:MedicsForUserListVm) => this.medicsList = it.medicsForUser,
          error: (err: HttpErrorResponse) => {
            this.errorHandler.handleError(err);
          }
        })
    }
  }

  public createImgPath = (serverPath: string) => {
    return `https://localhost:7192/${serverPath}`;
  }

  polyclinicOnMap=(polyclinic: MedicPolyclinicForUserLookUpDto)=>{
    const medicsUrl: string = `/user-ui/polyclinic-on-map/`;
    this.router.navigate([medicsUrl], { queryParams: {
        polyclinicName: polyclinic.polyclinicName,
        polyclinicAddress: polyclinic.polyclinicAddress,
        polyclinicPhoneNumber: polyclinic.polyclinicPhoneNumber,
        latitude: polyclinic.latitude,
        longitude: polyclinic.longitude,
        polyclinicPhoto: polyclinic.polyclinicPhoto,
        cityId: this.cityId,
        positionId: this.positionId,
        polyclinicId: polyclinic.polyclinicId
    }});
  }

  makeAppointment=(medicId: string, polyclinicId: string)=>{
    const medicsUrl: string = `/user-ui/make-appointment/`;
    this.router.navigate([medicsUrl], { queryParams: {
        medicId: medicId,
        positionId: this.positionId,
        polyclinicId: polyclinicId
      }});
  }

}
