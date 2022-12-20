export interface MedicAvailabilityLookUpDto {
  timeStart: Date;
  timeEnd: Date;
  date: Date;
  polyclinicName: string | undefined;
  positionName: string | undefined;
  polyclinicId: string;
  positionId: string;
}
