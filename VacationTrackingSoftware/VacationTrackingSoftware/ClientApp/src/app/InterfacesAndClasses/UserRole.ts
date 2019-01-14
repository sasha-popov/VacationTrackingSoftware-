export class UserRole {
  id?: number;
  user?: User;
  role?: Role;
}

export class User {
  id: number;
  name: string;
  surname: string;
  phoneNumber: string;
  email: string;
  password: string;
  dateRecruitment: string;
  userRoles?: UserRole[];
}

export class Role {
  id: number;
  name: string;
  userRoles: UserRole[];
}
