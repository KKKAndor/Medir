import { Component, OnInit } from '@angular/core';
import {ErrorHandlerService} from "../../shared/services/error-handler.service";
import {Router} from "@angular/router";
import { PolyclinicLookUpDto } from 'src/app/_interfaces/polyclinics/PolyclinicLookUpDto';
import { PolyclinicsRepositoryService } from 'src/app/shared/services/polyclinics-repository.service';
import {CityLookUpDto} from "../../_interfaces/cities/CityLookUpDto";
import {CitiesRepositoryService} from "../../shared/services/cities-repository.service";
import { CityDetailsVm } from 'src/app/_interfaces/cities/CityDetailsVm';
import {HttpErrorResponse} from "@angular/common/http";

@Component({
  selector: 'app-polyclinic-list',
  templateUrl: './polyclinic-list.component.html',
  styleUrls: ['./polyclinic-list.component.css']
})
export class PolyclinicListComponent implements OnInit {

  public list!: PolyclinicLookUpDto[] | undefined;

  constructor(private repository: PolyclinicsRepositoryService,
              private repCit: CitiesRepositoryService,
              private errorHandler: ErrorHandlerService,
              private router: Router) { }

  ngOnInit(): void {
    this.repository.getAllPolyclinics(undefined, 1,10,undefined, undefined)
      .subscribe(value => this.list = value.polyclinics);
  }

  public getCityName = (id: string): string =>{
    let name = '';
    this.repCit.getCity(id)
      .subscribe({
        next:(value:CityDetailsVm)=>{
          name =  value.cityName
        },
        error: (err: HttpErrorResponse) => {
          this.errorHandler.handleError(err);
        }});
    return name;
  }

  public getPolyclinicDetails = (id: string) => {
    const detailsUrl: string = `/polyclinic/polyclinic-details/${id}`;
    this.router.navigate([detailsUrl]);
  }

  public redirectToPolyclinicUpdatePage = (id: string) => {
    const updateUrl: string = `/polyclinic/polyclinic-update/${id}`;
    this.router.navigate([updateUrl]);
  }

  public redirectToPolyclinicDeletePage = (id: string) => {
    const deleteUrl: string = `/polyclinic/polyclinic-delete/${id}`;
    this.router.navigate([deleteUrl]);
  }

}
