import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CitiesRepositoryService } from 'src/app/shared/services/cities-repository.service';
import { ErrorHandlerService } from 'src/app/shared/services/error-handler.service';
import { CityDetailsVm } from "../../_interfaces/cities/CityDetailsVm";

@Component({
  selector: 'app-city-details',
  templateUrl: './city-details.component.html',
  styleUrls: ['./city-details.component.css']
})
export class CityDetailsComponent implements OnInit {

  item!: CityDetailsVm | null;

  constructor(private repository: CitiesRepositoryService, private router: Router,
              private activeRoute: ActivatedRoute, private errorHandler: ErrorHandlerService) { }

  ngOnInit() {
    this.getCityDetails()
  }

  getCityDetails = () => {
    const id: string = this.activeRoute.snapshot.params['id'];
    this.repository.getCity(id)
      .subscribe({
        next: (it: CityDetailsVm) => this.item = it,
        error: (err: HttpErrorResponse) => {
          this.errorHandler.handleError(err);
        }
      })
  }
}
