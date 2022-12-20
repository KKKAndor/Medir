import {Component, Input, OnInit} from '@angular/core';
import {ErrorHandlerService} from "../../../../shared/services/error-handler.service";
import {ActivatedRoute, Router} from "@angular/router";
import {MedicAvailabilityRepositoryService} from "../../../../shared/services/medic-availability-repository.service";
import {MedicAvailabilityLookUpDto} from "../../../../_interfaces/medicavailability/MedicAvailabilityLookUpDto";

@Component({
  selector: 'app-medic-availability-list',
  templateUrl: './medic-availability-list.component.html',
  styleUrls: ['./medic-availability-list.component.css']
})
export class MedicAvailabilityListComponent implements OnInit {

  @Input() id: string | undefined;

  public list!: MedicAvailabilityLookUpDto[] | undefined;

  constructor(private repository: MedicAvailabilityRepositoryService,
              private errorHandler: ErrorHandlerService,
              private activeRoute: ActivatedRoute,
              private router: Router) { }

  ngOnInit(): void {
    this.repository.getMedicAvailabilitiesList(this.id).subscribe(value => this.list = value.lookUpList);
  }

  public redirectToMedicAvailabilityDeletePage = (polyclinicId: string, positionId: string, date: Date) => {
    const deleteUrl: string = `./medic-availability/medic-availability-delete/`;
    this.router.navigate([deleteUrl],
      { queryParams: { medicId: this.id, polyclinicId: polyclinicId, positionId: positionId, date: date}});
  }

  public createMedicAvailabilityPage = () =>{
    const createUrl: string = `./medic-availability/medic-availability-create/`;
    this.router.navigate([createUrl], { queryParams: { medicId: this.id}});
  }

}
