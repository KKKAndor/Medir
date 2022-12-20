import { PolyclinicForUserLookUpDto } from "./PolyclinicForUserLookUpDto";

export interface PolyclinicsForUserListVm {
  polyclinicsByCity: PolyclinicForUserLookUpDto[] | undefined;
}
