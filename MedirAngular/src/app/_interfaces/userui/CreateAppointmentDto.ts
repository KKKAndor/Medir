export interface CreateAppointmentDto {
  medicAppointmentAvailabilityId: string;
  userEmail: string;
  prescription: string | undefined;
  date: Date;
  time: Date;
}
