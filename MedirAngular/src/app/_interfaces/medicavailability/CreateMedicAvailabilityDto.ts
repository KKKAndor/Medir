export interface CreateMedicAvailabilityDto {
  medicId: string;
  positionId: string;
  polyclinicId: string;
  date: Date;
  timeStart: Date;
  timeEnd: Date;
  receptionMinutesTime: number;
}
