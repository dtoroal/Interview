import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EmployeeModel } from '../../models/employees/employee.model';
import { RequestEmployeeModel } from '../../models/employees/request-employee.model';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  private baseUrlApiGetway: string;
  private moduleName: string = 'employee';

  constructor(
    @Inject('APIGETWAY_URL') apiGetwayUrl: string,
    private httpClient: HttpClient,
  ) {
    this.baseUrlApiGetway = `${apiGetwayUrl}/${this.moduleName}`;
  }

  public getEmployees(employeeId?: string): Observable<Array<EmployeeModel> | EmployeeModel> {
    return this.httpClient.get<any>(`${this.baseUrlApiGetway}${employeeId ? '/' + employeeId : ''}`);
  }

  public updateEmployee(employee: EmployeeModel): Observable<boolean> {

    const request: RequestEmployeeModel = {
      name: employee.name,
      lastName: employee.lastName,
      phoneNumber: employee.phoneNumber,
      email: employee.email,
      birthdayDate: employee.birthdayDate,
    };

    return this.httpClient.put<any>(`${this.baseUrlApiGetway}/update`, request);
  }

}
