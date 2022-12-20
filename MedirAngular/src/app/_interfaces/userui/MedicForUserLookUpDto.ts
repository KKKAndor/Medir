import { MedicPolyclinicForUserLookUpDto } from "./MedicPolyclinicForUserLookUpDto";

export interface MedicForUserLookUpDto {
  medicId: string;
  positionId: string;
  medicFullName: string;
  shortDescription: string;
  fullDescription: string;
  medicPhoneNumber: string;
  medicPhoto: string;
  yearsOnPosition: number;
  positionNames: string[];
  medicPolyclinicForUserLookUpList: MedicPolyclinicForUserLookUpDto[];
}
