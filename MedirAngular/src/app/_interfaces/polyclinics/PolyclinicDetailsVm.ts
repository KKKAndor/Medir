import {CityLookUpDto} from "../cities/CityLookUpDto";

export interface PolyclinicDetailsVm {
  polyclinicId: string;
  cityId: string;
  polyclinicName: string;
  polyclinicAddress: string;
  polyclinicPhoneNumber: string;
  polyclinicPhoto: string;
  latitude: number;
  longitude: number;
  city: CityLookUpDto | undefined;
}
