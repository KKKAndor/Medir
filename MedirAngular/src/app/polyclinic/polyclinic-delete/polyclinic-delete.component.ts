import { Component, OnInit } from '@angular/core';
import {PolyclinicDetailsVm} from "../../_interfaces/polyclinics/PolyclinicDetailsVm";
import {PolyclinicsRepositoryService} from "../../shared/services/polyclinics-repository.service";
import {CitiesRepositoryService} from "../../shared/services/cities-repository.service";
import {ActivatedRoute, Router} from "@angular/router";
import {ErrorHandlerService} from "../../shared/services/error-handler.service";
import {CityLookUpDto} from "../../_interfaces/cities/CityLookUpDto";
import {HttpErrorResponse} from "@angular/common/http";

@Component({
  selector: 'app-polyclinic-delete',
  templateUrl: './polyclinic-delete.component.html',
  styleUrls: ['./polyclinic-delete.component.css']
})
export class PolyclinicDeleteComponent implements OnInit {
  item: PolyclinicDetailsVm | null | undefined;

  constructor(private repository: PolyclinicsRepositoryService,
              private citRepository: CitiesRepositoryService,
              private router: Router,
              private activeRoute: ActivatedRoute,
              private errorHandler: ErrorHandlerService) { }

  ngOnInit() {
    this.getPolyclinicDetails()
  }

  getPolyclinicDetails = () => {
    const id: string = this.activeRoute.snapshot.params['id'];
    this.repository.getPolyclinic(id)
      .subscribe({
        next: (it: PolyclinicDetailsVm) => {
          this.item = it
          this.citRepository.getCity(it.cityId)
            .subscribe({
              next: (cit: CityLookUpDto) =>{
                this.item!.city = cit;
              },
              error: (err: HttpErrorResponse) => {
                this.errorHandler.handleError(err);
              }
            })
        },
        error: (err: HttpErrorResponse) => {
          this.errorHandler.handleError(err);
        }
      })
  }
  deletePolyclinic = ()=>{
    this.repository.deletePolyclinic(this.item!.polyclinicId)
      .subscribe({
        next: (_) => this.redirectToPolyclinicList()});
  }

  redirectToPolyclinicList = () => {
    this.router.navigate(['/polyclinic/polyclinic-list']);
  }
}
