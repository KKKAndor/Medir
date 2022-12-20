import {Component, Input, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {PositionLookUpDto} from "../../../../_interfaces/positions/PositionLookUpDto";
import {MedicPositionsRepositoryService} from "../../../../shared/services/medic-positions-repository.service";
import {PositionsRepositoryService} from "../../../../shared/services/positions-repository.service";
import {ActivatedRoute, Router} from "@angular/router";
import {ErrorHandlerService} from "../../../../shared/services/error-handler.service";
import {CreateMedicPositionDto} from "../../../../_interfaces/medicposition/CreateMedicPositionDto";

@Component({
  selector: 'app-medic-position-create',
  templateUrl: './medic-position-create.component.html',
  styleUrls: ['./medic-position-create.component.css']
})
export class MedicPositionCreateComponent implements OnInit {

  id: string | undefined;

  form!: FormGroup;

  Positions: PositionLookUpDto[] | undefined;

  selected: string | undefined;

  constructor(private repository: MedicPositionsRepositoryService,
              private positionsRepositoryService: PositionsRepositoryService,
              private activeRoute: ActivatedRoute,
              private errorHandler: ErrorHandlerService,
              private router: Router) { }

  ngOnInit(): void {
    this.activeRoute.queryParams
      .subscribe(params => {
        this.id = params['medicId']
        }
      );

    this.positionsRepositoryService.getAllPositions()
      .subscribe(value => this.Positions = value.positions);

    this.form = new FormGroup({
      positionId: new FormControl('', [Validators.required]),
      dateOnPosition: new FormControl('', [Validators.required])

    });
  }

  public validateControl = (controlName: string) => {
    return this.form.get(controlName)!.invalid && this.form.get(controlName)!.touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.form.get(controlName)!.hasError(errorName)
  }

  createMedicPosition = (FormValue: any) => {
    if(this.selected !== undefined){
      const item: CreateMedicPositionDto = {
        medicId: this.id!,
        positionId: FormValue.positionId,
        dateOnPosition: FormValue.dateOnPosition
      }

      this.repository.createMedicPosition(item)
        .subscribe({
          next: (_) => this.redirectToMedic()});
    }
  }

  redirectToMedic = () => {
    const redirectUrl: string = `../medic/medic-details/${this.id}`;
    this.router.navigate([redirectUrl]);
  }

  changed(e:any){
    this.selected = e.target.value;
  }

}
