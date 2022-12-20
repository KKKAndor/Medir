import {Component, Input, OnInit} from '@angular/core';
import {MedicPositionLookUpDto} from "../../../../_interfaces/medicposition/MedicPositionLookUpDto";
import {MedicPositionsRepositoryService} from "../../../../shared/services/medic-positions-repository.service";
import {ErrorHandlerService} from "../../../../shared/services/error-handler.service";
import {ActivatedRoute, Router} from "@angular/router";
import {HttpErrorResponse} from "@angular/common/http";

@Component({
  selector: 'app-medic-position-list',
  templateUrl: './medic-position-list.component.html',
  styleUrls: ['./medic-position-list.component.css']
})
export class MedicPositionListComponent implements OnInit {

  @Input() id: string | undefined;

  public list!: MedicPositionLookUpDto[] | undefined;

  constructor(private repository: MedicPositionsRepositoryService,
              private errorHandler: ErrorHandlerService,
              private activeRoute: ActivatedRoute,
              private router: Router) { }

  ngOnInit(): void {
    this.repository.getAllMedicPositions(this.id).subscribe({next:value => this.list = value.medicPositions});
  }

  public getMedicPositionDetails = (positionId: string) => {
    const detailsUrl: string = `./medic-position/medic-position-details/`;
    this.router.navigate([detailsUrl], { queryParams: { medicId: this.id, positionId: positionId}});
  }

  public redirectToMedicPositionDeletePage = (positionId: string) => {
    const deleteUrl: string = `./medic-position/medic-position-delete/`;
    this.router.navigate([deleteUrl], { queryParams: { medicId: this.id, positionId: positionId}});
  }

  public redirectToMedicPositionUpdatePage = (positionId: string) => {
    const deleteUrl: string = `./medic-position/medic-position-update/`;
    this.router.navigate([deleteUrl], { queryParams: { medicId: this.id, positionId: positionId}});
  }

  public createMedicPositionPage = () =>{
    const createUrl: string = `./medic-position/medic-position-create/`;
    this.router.navigate([createUrl], { queryParams: { medicId: this.id}});
  }

}
