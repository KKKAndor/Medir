export interface AppointmentLookUpDto {
  appointmentId: string;
  prescription: string;
  medicFullName: string;
  positionName: string;
  polyclinicName: string;
  polyclinicAddress: string;
  cityName: string;
  price: number;
  time: Date;
  date: Date;
}
