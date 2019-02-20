import { StatusesRequest } from '../Enums/StatusesRequest';

export interface UserVacationRequest {
  id: number;
  userName: string;
  userId: string;
  startDate: Date;
  endDate: Date;
  vacationType: string;
  status: StatusesRequest;
  payment: number;
}  

