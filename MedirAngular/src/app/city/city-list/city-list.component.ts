import { Component, OnInit } from '@angular/core';
import {CitiesRepositoryService} from "../../shared/services/cities-repository.service";
import {CityLookUpDto} from "../../_interfaces/cities/CityLookUpDto";
import { Router } from '@angular/router';
import { ErrorHandlerService } from 'src/app/shared/services/error-handler.service';

@Component({
  selector: 'app-city-list',
  templateUrl: './city-list.component.html',
  styleUrls: ['./city-list.component.css']
})
export class CityListComponent implements OnInit {

  public list!: CityLookUpDto[] | undefined;

  constructor(private repository: CitiesRepositoryService,
              private errorHandler: ErrorHandlerService,
              private router: Router) { }

  ngOnInit(): void {
    this.repository.getAllCities().subscribe(value => this.list = value.cities);
  }

  public getCityDetails = (id: string) => {
    const detailsUrl: string = `/city/city-details/${id}`;
    this.router.navigate([detailsUrl]);
  }

  public redirectToCityUpdatePage = (id: string) => {
    const updateUrl: string = `/city/city-update/${id}`;
    this.router.navigate([updateUrl]);
  }

  public redirectToCityDeletePage = (id: string) => {
    const deleteUrl: string = `/city/city-delete/${id}`;
    this.router.navigate([deleteUrl]);
  }
}
