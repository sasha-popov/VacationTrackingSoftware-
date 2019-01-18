import { validate, Contains, IsInt, Length, IsEmail, IsFQDN, IsDate, Min, Max, IsString } from "class-validator";

 export interface UserRegistration {
  email: string;
  password: string;
  firstName: string;
  lastName: string;
   //location: string;
   phoneNumber: string;
   role: string;
}
