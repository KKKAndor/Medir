import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {ErrorHandlerService} from "../../../../shared/services/error-handler.service";
import {MedicAvailabilityRepositoryService} from "../../../../shared/services/medic-availability-repository.service";
import { DeleteMedicAvailabilityCommand } from 'src/app/_interfaces/medicavailability/DeleteMedicAvailabilityCommand';

@Component({
  selector: 'app-medic-availability-delete',
  templateUrl: './medic-availability-delete.component.html',
  styleUrls: ['./medic-availability-delete.component.css']
})
export class MedicAvailabilityDeleteComponent implements OnInit {

  medicId: string | undefined;

  polyclinicId: string | undefined;

  positionId: string | undefined;

  date: Date;

  constructor(private repository: MedicAvailabilityRepositoryService, private router: Router,
              private activeRoute: ActivatedRoute, private errorHandler: ErrorHandlerService) {
    this.activeRoute.queryParams
      .subscribe(params => {
          this.medicId = params['medicId'];
          this.polyclinicId = params['polyclinicId'];
          this.positionId = params['positionId'];
          this.date = params['date'];
        }
      );
  }

  ngOnInit() {
  }

  deleteMedicAvailability= ()=>{
    const command: DeleteMedicAvailabilityCommand = {
      medicId: this.medicId!,
      polyclinicId: this.polyclinicId!,
      positionId: this.positionId!,
      date: this.date
    }

    this.repository.deleteMedicAvailability(command)
      .subscribe({
        next: (_) => this.redirectToMedic()});
  }

  redirectToMedic = () => {
    const redirectUrl: string = `../medic/medic-details/${this.medicId}`;
    this.router.navigate([redirectUrl]);
  }

}
