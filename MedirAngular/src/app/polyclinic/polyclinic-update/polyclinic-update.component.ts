import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {CitiesRepositoryService} from "../../shared/services/cities-repository.service";
import {ErrorHandlerService} from "../../shared/services/error-handler.service";
import {ActivatedRoute, Router} from "@angular/router";
import {CityDetailsVm} from "../../_interfaces/cities/CityDetailsVm";
import {UpdateCityDto} from "../../_interfaces/cities/UpdateCityDto";
import {HttpErrorResponse} from "@angular/common/http";
import {CityLookUpDto} from "../../_interfaces/cities/CityLookUpDto";
import {PolyclinicsRepositoryService} from "../../shared/services/polyclinics-repository.service";
import { PolyclinicLookUpDto } from 'src/app/_interfaces/polyclinics/PolyclinicLookUpDto';
import { PolyclinicDetailsVm } from 'src/app/_interfaces/polyclinics/PolyclinicDetailsVm';
import { UpdatePolyclinicDto } from 'src/app/_interfaces/polyclinics/UpdatePolyclinicDto';

@Component({
  selector: 'app-polyclinic-update',
  templateUrl: './polyclinic-update.component.html',
  styleUrls: ['./polyclinic-update.component.css']
})
export class PolyclinicUpdateComponent implements OnInit {

  item: PolyclinicDetailsVm | null | undefined;

  response!: {dbPath: ''};

  form!: FormGroup;

  Cities: CityLookUpDto[] | undefined;

  selected: string | undefined;

  constructor(private repository: PolyclinicsRepositoryService,
              private citiesRepositoryService: CitiesRepositoryService,
              private errorHandler: ErrorHandlerService,
              private router: Router,
              private activeRoute: ActivatedRoute) { }

  ngOnInit(): void {

    const id: string = this.activeRoute.snapshot.params['id'];
    this.repository.getPolyclinic(id)
      .subscribe({
        next: (it: PolyclinicDetailsVm) => {
          this.item = it
        },
        error: (err: HttpErrorResponse) => {
          this.errorHandler.handleError(err);
        }
      })

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
  public createImgPath = (serverPath: string) => {
    return `https://localhost:7192/${serverPath}`;
  }


  public validateControl = (controlName: string) => {
    return this.form.get(controlName)!.invalid && this.form.get(controlName)!.touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.form.get(controlName)!.hasError(errorName)
  }

  updatePolyclinic = (FormValue: any) => {

    const id: string = this.activeRoute.snapshot.params['id'];
    this.repository.getPolyclinic(id)
      .subscribe({
        next: (it: PolyclinicDetailsVm) => {
          const updatePolyclinicDto: UpdatePolyclinicDto = {
            polyclinicId: it.polyclinicId,
            polyclinicAddress: FormValue.polyclinicAddress,
            polyclinicName: FormValue.polyclinicName,
            polyclinicPhoneNumber: FormValue.polyclinicPhoneNumber,
            polyclinicPhoto: this.response.dbPath,
            cityId: this.selected!,
            latitude: FormValue.latitude,
            longitude: FormValue.longitude
          }
          this.repository.updatePolyclinic(updatePolyclinicDto)
            .subscribe({
              next: (_) => this.redirectToPolyclinicList()});
        },
        error: (err: HttpErrorResponse) => {
          this.errorHandler.handleError(err);
        }
      })
  }

  redirectToPolyclinicList = () => {
    this.router.navigate(['/polyclinic/polyclinic-list']);
  }

  changed(e:any){
    this.selected = e.target.value;
  }

  uploadFinished = (event: any) => {
    this.response = event;
  }

}
