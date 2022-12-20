import { PolyclinicLookUpDto } from 'src/app/_interfaces/polyclinics/PolyclinicLookUpDto';

export interface PolyclinicsListVm {
  polyclinics: PolyclinicLookUpDto[] | undefined;
}
