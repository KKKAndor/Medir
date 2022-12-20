import {Component, Input, OnInit} from '@angular/core';
import {ErrorHandlerService} from "../../../../shared/services/error-handler.service";
import {ActivatedRoute, Router} from "@angular/router";
import { MedicPolyclinicLookUpDto } from 'src/app/_interfaces/medicpolyclinics/MedicPolyclinicLookUpDto';
import { MedicPolyclinicsRepositoryService } from 'src/app/shared/services/medic-polyclinics-repository.service';

@Component({
  selector: 'app-medic-polyclinic-list',
  templateUrl: './medic-polyclinic-list.component.html',
  styleUrls: ['./medic-polyclinic-list.component.css']
})
export class MedicPolyclinicListComponent implements OnInit {

  @Input() id: string | undefined;

  public list!: MedicPolyclinicLookUpDto[] | undefined;

  constructor(private repository: MedicPolyclinicsRepositoryService,
              private errorHandler: ErrorHandlerService,
              private activeRoute: ActivatedRoute,
              private router: Router) { }

  ngOnInit(): void {
    this.repository.getAllMedicPolyclinics(this.id, 1,10,undefined, undefined)
      .subscribe({next:value => this.list = value.medicPolyclinics});
  }

  public getMedicPolyclinicDetails = (polyclinicId: string) => {
    const detailsUrl: string = `./medic-polyclinic/medic-polyclinic-details/`;
    this.router.navigate([detailsUrl], { queryParams: { medicId: this.id, polyclinicId: polyclinicId}});
  }

  public redirectToMedicPolyclinicDeletePage = (polyclinicId: string) => {
    const deleteUrl: string = `./medic-polyclinic/medic-polyclinic-delete/`;
    this.router.navigate([deleteUrl], { queryParams: { medicId: this.id, polyclinicId: polyclinicId}});
  }

  public redirectToMedicPolyclinicUpdatePage = (polyclinicId: string) => {
    const deleteUrl: string = `./medic-polyclinic/medic-polyclinic-update/`;
    this.router.navigate([deleteUrl], { queryParams: { medicId: this.id, polyclinicId: polyclinicId}});
  }

  public createMedicPolyclinicPage = () =>{
    const createUrl: string = `./medic-polyclinic/medic-polyclinic-create/`;
    this.router.navigate([createUrl], { queryParams: { medicId: this.id}});
  }

}
