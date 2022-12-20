import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {ErrorHandlerService} from "../../shared/services/error-handler.service";
import {Router} from "@angular/router";
import { PositionsRepositoryService } from 'src/app/shared/services/positions-repository.service';
import {CreatePositionDto} from "../../_interfaces/positions/CreatePositionDto";

@Component({
  selector: 'app-position-create',
  templateUrl: './position-create.component.html',
  styleUrls: ['./position-create.component.css']
})
export class PositionCreateComponent implements OnInit {

  form!: FormGroup;

  constructor(private repository: PositionsRepositoryService, private errorHandler: ErrorHandlerService,
              private router: Router) { }

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

  createPosition = (FormValue: any) => {

    const item: CreatePositionDto = {
      positionName: FormValue.positionName
    }

    this.repository.createPosition(item)
      .subscribe({
        next: (_) => this.redirectToPositionList()});

  }

  redirectToPositionList = () => {
    this.router.navigate(['/position/position-list']);
  }
}
