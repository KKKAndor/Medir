import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {CitiesRepositoryService} from "../../shared/services/cities-repository.service";
import {ErrorHandlerService} from "../../shared/services/error-handler.service";
import {ActivatedRoute, Router} from "@angular/router";
import {CreateCityDto} from "../../_interfaces/cities/CreateCityDto";
import { UpdateCityDto } from 'src/app/_interfaces/cities/UpdateCityDto';
import {CityDetailsVm} from "../../_interfaces/cities/CityDetailsVm";
import {HttpErrorResponse} from "@angular/common/http";

@Component({
  selector: 'app-city-update',
  templateUrl: './city-update.component.html',
  styleUrls: ['./city-update.component.css']
})
export class CityUpdateComponent implements OnInit {
  form!: FormGroup;

  constructor(private repository: CitiesRepositoryService, private errorHandler: ErrorHandlerService,
              private router: Router, private activeRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.form = new FormGroup({
      cityName: new FormControl('', [Validators.required, Validators.maxLength(50)]),
      latitude: new FormControl('', [Validators.required]),
      longitude: new FormControl('', [Validators.required])
    });
  }


  public validateControl = (controlName: string) => {
    return this.form.get(controlName)!.invalid && this.form.get(controlName)!.touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.form.get(controlName)!.hasError(errorName)
  }

  updateCity = (FormValue: any) => {

    const cityId: string = this.activeRoute.snapshot.params['id'];
    this.repository.getCity(cityId)
      .subscribe({
        next: (cit: CityDetailsVm) => {
          const city: UpdateCityDto = {
            cityId: cit.cityId,
            cityName: FormValue.cityName,
            longitude: FormValue.longitude,
            latitude: FormValue.latitude
          }
          this.repository.updateCity(city)
            .subscribe({
              next: (_) => this.redirectToCityList()});
        },
        error: (err: HttpErrorResponse) => {
          this.errorHandler.handleError(err);
        }
      })
  }

  redirectToCityList = () => {
    this.router.navigate(['/city/сity-list']);
  }

}
