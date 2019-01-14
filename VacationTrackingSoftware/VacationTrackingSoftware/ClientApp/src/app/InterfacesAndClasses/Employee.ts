import { validate, Contains, IsInt, Length, IsEmail, IsFQDN, IsDate, Min, Max, IsString } from "class-validator";

export class Employee {
  @Length(1, 20)
  @IsString()
  name: string;
  @Length(1, 20)
  @IsString()
  surname: string;
  phoneNumber: string;
  @IsEmail()
  email: string;
}  
