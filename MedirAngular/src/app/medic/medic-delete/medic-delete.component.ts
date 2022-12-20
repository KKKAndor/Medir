import { Component, OnInit } from '@angular/core';
import {MedicDetailsVm} from "../../_interfaces/medics/MedicDetailsVm";
import {MedicsRepositoryService} from "../../shared/services/medics-repository.service";
import {ActivatedRoute, Router} from "@angular/router";
import {ErrorHandlerService} from "../../shared/services/error-handler.service";
import {HttpErrorResponse} from "@angular/common/http";

@Component({
  selector: 'app-medic-delete',
  templateUrl: './medic-delete.component.html',
  styleUrls: ['./medic-delete.component.css']
})
export class MedicDeleteComponent implements OnInit {

  item!: MedicDetailsVm | null;

  constructor(private repository: MedicsRepositoryService, private router: Router,
              private activeRoute: ActivatedRoute, private errorHandler: ErrorHandlerService) { }

  ngOnInit() {
    this.getMedicDetails()
  }

  getMedicDetails = () => {
    const id: string = this.activeRoute.snapshot.params['id'];
    this.repository.getMedic(id)
      .subscribe({
        next: (it: MedicDetailsVm) => this.item = it,
        error: (err: HttpErrorResponse) => {
          this.errorHandler.handleError(err);
        }
      })
  }

  deleteMedic = ()=>{
    this.repository.deleteMedic(this.item!.medicId)
      .subscribe({
        next: (_) => this.redirectToMedicList()});
  }

  redirectToMedicList = () => {
    this.router.navigate(['/medic/medic-list']);
  }
}
