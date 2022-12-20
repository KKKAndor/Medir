import { Component, OnInit } from '@angular/core';
import {CityLookUpDto} from "../../_interfaces/cities/CityLookUpDto";
import {CitiesRepositoryService} from "../../shared/services/cities-repository.service";
import {ErrorHandlerService} from "../../shared/services/error-handler.service";
import {Router} from "@angular/router";
import { PositionLookUpDto } from 'src/app/_interfaces/positions/PositionLookUpDto';
import { PositionsRepositoryService } from 'src/app/shared/services/positions-repository.service';

@Component({
  selector: 'app-position-list',
  templateUrl: './position-list.component.html',
  styleUrls: ['./position-list.component.css']
})
export class PositionListComponent implements OnInit {

  public list!: PositionLookUpDto[] | undefined;

  constructor(private repository: PositionsRepositoryService,
              private errorHandler: ErrorHandlerService,
              private router: Router) { }

  ngOnInit(): void {
    this.repository.getAllPositions(1,10,undefined, undefined).subscribe(value => this.list = value.positions);
  }

  public getPositionDetails = (id: string) => {
    const detailsUrl: string = `/position/position-details/${id}`;
    this.router.navigate([detailsUrl], { queryParams: { positionId: id } });
  }

  public redirectToPositionUpdatePage = (id: string) => {
    const updateUrl: string = `/position/position-update/${id}`;
    this.router.navigate([updateUrl]);
  }

  public redirectToPositionDeletePage = (id: string) => {
    const deleteUrl: string = `/position/position-delete/${id}`;
    this.router.navigate([deleteUrl]);
  }
}
