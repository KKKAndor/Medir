import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {ErrorHandlerService} from "../../shared/services/error-handler.service";
import {HttpErrorResponse} from "@angular/common/http";
import { PolyclinicsRepositoryService } from 'src/app/shared/services/polyclinics-repository.service';
import { PolyclinicDetailsVm } from 'src/app/_interfaces/polyclinics/PolyclinicDetailsVm';
import {CityLookUpDto} from "../../_interfaces/cities/CityLookUpDto";
import {CitiesRepositoryService} from "../../shared/services/cities-repository.service";

@Component({
  selector: 'app-polyclinic-details',
  templateUrl: './polyclinic-details.component.html',
  styleUrls: ['./polyclinic-details.component.css']
})
export class PolyclinicDetailsComponent implements OnInit {

  item: PolyclinicDetailsVm | null | undefined;

  constructor(private repository: PolyclinicsRepositoryService,
              private cityRepository: CitiesRepositoryService,
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
          this.cityRepository.getCity(it.cityId)
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
  public createImgPath = (serverPath: string) => {
    return `https://localhost:7192/${serverPath}`;
  }

}
