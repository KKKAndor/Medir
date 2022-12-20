import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CitiesRepositoryService } from 'src/app/shared/services/cities-repository.service';
import { ErrorHandlerService } from 'src/app/shared/services/error-handler.service';
import { CreateCityDto } from 'src/app/_interfaces/cities/CreateCityDto';
import {HttpErrorResponse} from "@angular/common/http";

@Component({
  selector: 'app-city-create',
  templateUrl: './city-create.component.html',
  styleUrls: ['./city-create.component.css']
})
export class CityCreateComponent implements OnInit {

  form!: FormGroup;

  constructor(private repository: CitiesRepositoryService, private errorHandler: ErrorHandlerService,
              private router: Router) { }

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

  createCity = (FormValue: any) => {

    const city: CreateCityDto = {
      cityName: FormValue.cityName,
      longitude: FormValue.longitude,
      latitude: FormValue.latitude
    }

    this.repository.createCity(city)
      .subscribe({
        next: (_) => this.redirectToCityList()});

  }

  redirectToCityList = () => {
    this.router.navigate(['/city/сity-list']);
  }
}
