import { Component, OnInit } from '@angular/core';
import {UserUiService} from "../../shared/services/user-ui.service";
import {CityLookUpDto} from "../../_interfaces/cities/CityLookUpDto";
import {PositionForUserLookUpDto} from "../../_interfaces/userui/PositionForUserLookUpDto";
import {PositionsForUserListVm} from "../../_interfaces/userui/PositionsForUserListVm";
import {ErrorHandlerService} from "../../shared/services/error-handler.service";
import {HttpErrorResponse} from "@angular/common/http";
import {Router} from "@angular/router";

@Component({
  selector: 'app-positions-user',
  templateUrl: './positions-user.component.html',
  styleUrls: ['./positions-user.component.css']
})
export class PositionsUserComponent implements OnInit {

  public citiesList!: CityLookUpDto[] | undefined;

  public positionsList!: PositionForUserLookUpDto[] | undefined;

  selectedCityId: string | undefined = "undefined";

  constructor(private userService: UserUiService,
              private errorHandler: ErrorHandlerService,
              private router: Router) { }

  ngOnInit(): void {

    this.userService.getAllCitiesForChoice().subscribe(value => this.citiesList = value.cities);

    this.getCities(undefined)
  }

  cityChanged(e: any){
    if(e.target.value == "undefined"){
      this.selectedCityId = "undefined";
      this.getCities(undefined);
    }
    else {
      this.selectedCityId = e.target.value;
      this.getCities(this.selectedCityId)
    }
  }

  getCities(id: string | undefined){
    this.userService.getPositionsForUserList(id)
      .subscribe({
        next: (it: PositionsForUserListVm) => this.positionsList = it.positionsForUserList,
        error: (err: HttpErrorResponse) => {
          this.errorHandler.handleError(err);
        }
      })
  }

  positionSelected(id: string){
    const medicsUrl: string = `/user-ui/medics-user/`;
    this.router.navigate([medicsUrl], { queryParams: { cityId: this.selectedCityId, positionId: id}});
  }
}
