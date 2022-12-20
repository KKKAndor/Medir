export interface PolyclinicForUserLookUpDto {
  cityId: string;
  polyclinicId: string;
  polyclinicName: string | undefined;
  polyclinicAddress: string;
  polyclinicPhoneNumber: string;
  polyclinicPhoto: string;
  latitude: number;
  longitude: number;
}
