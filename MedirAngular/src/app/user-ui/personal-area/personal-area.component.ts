import { Component, OnInit } from '@angular/core';
import {UserUiService} from "../../shared/services/user-ui.service";
import {AuthenticationService} from "../../shared/services/authentication.service";
import { AppointmentLookUpDto } from 'src/app/_interfaces/userui/AppointmentLookUpDto';

@Component({
  selector: 'app-personal-area',
  templateUrl: './personal-area.component.html',
  styleUrls: ['./personal-area.component.css']
})
export class PersonalAreaComponent implements OnInit {

  appointments: AppointmentLookUpDto[];

  constructor(private repository: UserUiService,
              private authService: AuthenticationService) { }

  ngOnInit(): void {
    this.repository.getAppointments(this.authService.getUserName())
      .subscribe({next: value => {
        this.appointments = value.appointments
        }})
  }

}
