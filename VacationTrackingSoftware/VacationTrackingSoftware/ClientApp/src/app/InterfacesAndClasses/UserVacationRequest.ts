export interface UserVacationRequest {
  id: number;
  user: number;
  startDate: Date;
  endDate: Date;
  vacationType: string;
  status: string;
}  

