export interface MedicPolyclinicForUserLookUpDto {
  polyclinicId: string;
  polyclinicName: string;
  polyclinicAddress: string;
  polyclinicPhoneNumber: string;
  polyclinicPhoto: string;
  latitude: number;
  longitude: number;
  price: number;
  closestAppoint: Date;
}
