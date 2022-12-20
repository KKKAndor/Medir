import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {ErrorHandlerService} from "../../shared/services/error-handler.service";
import {HttpErrorResponse} from "@angular/common/http";
import { PositionsRepositoryService } from 'src/app/shared/services/positions-repository.service';
import { PositionDetailsVm } from 'src/app/_interfaces/positions/PositionDetailsVm';

@Component({
  selector: 'app-position-delete',
  templateUrl: './position-delete.component.html',
  styleUrls: ['./position-delete.component.css']
})
export class PositionDeleteComponent implements OnInit {
  item!: PositionDetailsVm | null;

  constructor(private repository: PositionsRepositoryService, private router: Router,
              private activeRoute: ActivatedRoute, private errorHandler: ErrorHandlerService) { }

  ngOnInit(): void {
    this.getPositionDetails()
  }

  getPositionDetails = () => {
    const id: string = this.activeRoute.snapshot.params['id'];
    this.repository.getPosition(id)
      .subscribe({
        next: (it: PositionDetailsVm) => this.item = it,
        error: (err: HttpErrorResponse) => {
          this.errorHandler.handleError(err);
        }
      })
  }

  deletePosition = ()=>{
    this.repository.deletePosition(this.item!.positionId)
      .subscribe({
        next: (_) => this.redirectToPositionList()});
  }

  redirectToPositionList = () => {
    this.router.navigate(['/position/position-list']);
  }
}
