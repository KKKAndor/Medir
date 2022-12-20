import { Component, OnInit } from '@angular/core';
import {ErrorHandlerService} from "../../shared/services/error-handler.service";
import {Router} from "@angular/router";
import { MedicsRepositoryService } from 'src/app/shared/services/medics-repository.service';
import { MedicLookUpDto } from 'src/app/_interfaces/medics/MedicLookUpDto';

@Component({
  selector: 'app-medic-list',
  templateUrl: './medic-list.component.html',
  styleUrls: ['./medic-list.component.css']
})
export class MedicListComponent implements OnInit {

  public list!: MedicLookUpDto[] | undefined;

  constructor(private repository: MedicsRepositoryService,
              private errorHandler: ErrorHandlerService,
              private router: Router) { }

  ngOnInit(): void {
    this.repository.getAllMedics(1,10,undefined, undefined).subscribe(value => this.list = value.medics);
  }

  public getMedicDetails = (id: string) => {
    const detailsUrl: string = `/medic/medic-details/${id}`;
    this.router.navigate([detailsUrl]);
  }

  public redirectToMedicUpdatePage = (id: string) => {
    const updateUrl: string = `/medic/medic-update/${id}`;
    this.router.navigate([updateUrl]);
  }

  public redirectToMedicDeletePage = (id: string) => {
    const deleteUrl: string = `/medic/medic-delete/${id}`;
    this.router.navigate([deleteUrl]);
  }
}
