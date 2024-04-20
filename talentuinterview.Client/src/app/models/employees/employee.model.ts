import { RoleModel } from "./role.model";

export interface EmployeeModel {
  id: string;
  birthdayDate: Date;
  email: string;
  hireDate: Date;
  lastName: string;
  name: string;
  phoneNumber: string;
  roleId: string;
  role?: RoleModel;
  image?: string;
}
