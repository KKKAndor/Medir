import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {MedicPolyclinicForUserLookUpDto} from "../../_interfaces/userui/MedicPolyclinicForUserLookUpDto";

@Component({
  selector: 'app-polyclinic-on-map-user',
  templateUrl: './polyclinic-on-map-user.component.html',
  styleUrls: ['./polyclinic-on-map-user.component.css']
})
export class PolyclinicOnMapUserComponent implements OnInit {

  polyclinic!: MedicPolyclinicForUserLookUpDto;

  cityId: string = "";

  positionId: string = "";

  constructor(private activeRoute: ActivatedRoute,
              private router: Router) { }

  ngOnInit(): void {
    this.activeRoute.queryParams
      .subscribe(params => {
          this.polyclinic = new class implements MedicPolyclinicForUserLookUpDto {
            polyclinicId = params['polyclinicId']
            latitude = params['latitude'];
            longitude = params['longitude'];
            polyclinicAddress = params['polyclinicAddress'];
            polyclinicName = params['polyclinicName'];
            polyclinicPhoneNumber = params['polyclinicPhoneNumber'];
            polyclinicPhoto = params['polyclinicPhoto'];
            price = 0;
            closestAppoint = new Date;
          }
          this.cityId = params['cityId'];
          this.positionId = params['positionId']
        }
      );
  }

  backToMedics(){
    const medicsUrl: string = `/user-ui/medics-user/`;
    this.router.navigate([medicsUrl], { queryParams: { cityId: this.cityId, positionId: this.positionId}});
  }

}
