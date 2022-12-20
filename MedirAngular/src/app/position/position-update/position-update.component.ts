import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {CitiesRepositoryService} from "../../shared/services/cities-repository.service";
import {ErrorHandlerService} from "../../shared/services/error-handler.service";
import {ActivatedRoute, Router} from "@angular/router";
import {CityDetailsVm} from "../../_interfaces/cities/CityDetailsVm";
import {UpdateCityDto} from "../../_interfaces/cities/UpdateCityDto";
import {HttpErrorResponse} from "@angular/common/http";
import { PositionsRepositoryService } from 'src/app/shared/services/positions-repository.service';
import { PositionDetailsVm } from 'src/app/_interfaces/positions/PositionDetailsVm';
import { UpdatePositionDto } from 'src/app/_interfaces/positions/UpdatePositionDto';

@Component({
  selector: 'app-position-update',
  templateUrl: './position-update.component.html',
  styleUrls: ['./position-update.component.css']
})
export class PositionUpdateComponent implements OnInit {
  form!: FormGroup;

  constructor(private repository: PositionsRepositoryService, private errorHandler: ErrorHandlerService,
              private router: Router, private activeRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.form = new FormGroup({
      positionName: new FormControl('', [Validators.required, Validators.maxLength(100)])
    });
  }


  public validateControl = (controlName: string) => {
    return this.form.get(controlName)!.invalid && this.form.get(controlName)!.touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.form.get(controlName)!.hasError(errorName)
  }

  updatePosition = (FormValue: any) => {

    const id: string = this.activeRoute.snapshot.params['id'];
    this.repository.getPosition(id)
      .subscribe({
        next: (it: PositionDetailsVm) => {
          const item: UpdatePositionDto = {
            positionId: it.positionId,
            positionName: FormValue.positionName
          }
          this.repository.updatePosition(item)
            .subscribe({
              next: (_) => this.redirectToPositionList()});
        },
        error: (err: HttpErrorResponse) => {
          this.errorHandler.handleError(err);
        }
      })
  }

  redirectToPositionList = () => {
    this.router.navigate(['/position/position-list']);
  }

}
