import { Component, OnInit } from '@angular/core';
import {CitiesRepositoryService} from "../../shared/services/cities-repository.service";
import {ActivatedRoute, Router} from "@angular/router";
import {ErrorHandlerService} from "../../shared/services/error-handler.service";
import {CityDetailsVm} from "../../_interfaces/cities/CityDetailsVm";
import {HttpErrorResponse} from "@angular/common/http";

@Component({
  selector: 'app-city-delete',
  templateUrl: './city-delete.component.html',
  styleUrls: ['./city-delete.component.css']
})
export class CityDeleteComponent implements OnInit {
  city!: CityDetailsVm | null;

  constructor(private repository: CitiesRepositoryService, private router: Router,
              private activeRoute: ActivatedRoute, private errorHandler: ErrorHandlerService) { }

  ngOnInit(): void {
    this.getCityDetails()
  }

  getCityDetails = () => {
    const cityId: string = this.activeRoute.snapshot.params['id'];
    this.repository.getCity(cityId)
      .subscribe({
        next: (cit: CityDetailsVm) => this.city = cit,
        error: (err: HttpErrorResponse) => {
          this.errorHandler.handleError(err);
        }
      })
  }

  deleteCity = ()=>{
    this.repository.deleteCity(this.city!.cityId)
      .subscribe({
        next: (_) => this.redirectToCityList()});
  }

  redirectToCityList = () => {
    this.router.navigate(['/city/сity-list']);
  }
}
