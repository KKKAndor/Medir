import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {ErrorHandlerService} from "../../shared/services/error-handler.service";
import {Router} from "@angular/router";
import { PolyclinicsRepositoryService } from 'src/app/shared/services/polyclinics-repository.service';
import { CreatePolyclinicDto } from 'src/app/_interfaces/polyclinics/CreatePolyclinicDto';
import {CitiesRepositoryService} from "../../shared/services/cities-repository.service";
import {CityLookUpDto} from "../../_interfaces/cities/CityLookUpDto";

@Component({
  selector: 'app-polyclinic-create',
  templateUrl: './polyclinic-create.component.html',
  styleUrls: ['./polyclinic-create.component.css']
})
export class PolyclinicCreateComponent implements OnInit {

  response!: {dbPath: ''};

  form!: FormGroup;

  Cities: CityLookUpDto[] | undefined;

  selected: string | undefined;

  address: string | undefined;

  constructor(private repository: PolyclinicsRepositoryService,
              private citiesRepositoryService: CitiesRepositoryService,
              private errorHandler: ErrorHandlerService,
              private router: Router) { }

  ngOnInit(): void {
    this.citiesRepositoryService.getAllCities()
      .subscribe(value => this.Cities = value.cities);

    this.form = new FormGroup({
      polyclinicName: new FormControl('', [Validators.required, Validators.maxLength(50)]),
      polyclinicAddress: new FormControl('', [Validators.required]),
      polyclinicPhoneNumber: new FormControl('', [Validators.required, Validators.pattern("^((\\+373-?)|0)?[0-9]{8}$")]),
      cityId: new FormControl('', [Validators.required]),
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

  createPolyclinic = (FormValue: any) => {

    if(this.selected !== undefined){
      const item: CreatePolyclinicDto = {
        polyclinicName: FormValue.polyclinicName,
        polyclinicAddress: FormValue.polyclinicAddress,
        polyclinicPhoneNumber: FormValue.polyclinicPhoneNumber,
        polyclinicPhoto: this.response.dbPath,
        cityId: this.selected,
        longitude: FormValue.longitude,
        latitude: FormValue.latitude
      }

      this.repository.createPolyclinic(item)
        .subscribe({
          next: (_) => this.redirectToPolyclinicList()});
    }
  }

  redirectToPolyclinicList = () => {
    this.router.navigate(['/polyclinic/polyclinic-list']);
  }

  changed(e:any){
    this.selected = e.target.value;
  }

  uploadFinished = (event:any) => {
    this.response = event;
  }
}
