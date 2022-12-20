import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {ErrorHandlerService} from "../../shared/services/error-handler.service";
import {HttpErrorResponse} from "@angular/common/http";
import { MedicsRepositoryService } from 'src/app/shared/services/medics-repository.service';
import { MedicDetailsVm } from 'src/app/_interfaces/medics/MedicDetailsVm';

@Component({
  selector: 'app-medic-details',
  templateUrl: './medic-details.component.html',
  styleUrls: ['./medic-details.component.css']
})
export class MedicDetailsComponent implements OnInit {

  item!: MedicDetailsVm | null;

  constructor(private repository: MedicsRepositoryService, private router: Router,
              private activeRoute: ActivatedRoute, private errorHandler: ErrorHandlerService) { }

  ngOnInit() {
    this.getMedicDetails()
  }

  getMedicDetails = () => {
    const id: string = this.activeRoute.snapshot.params['id'];
    this.repository.getMedic(id)
      .subscribe({
        next: (it: MedicDetailsVm) => this.item = it,
        error: (err: HttpErrorResponse) => {
          this.errorHandler.handleError(err);
        }
      })
  }
  public createImgPath = (serverPath: string) => {
    return `https://localhost:7192/${serverPath}`;
  }

}
