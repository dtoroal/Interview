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
    const requestData: RequestEmployeeModel = this.setRequestEmployeeData(employee);
    return this.httpClient.put<any>(`${this.baseUrlApiGetway}/update`, requestData);
  }

  public postEmployee(employee: EmployeeModel): Observable<boolean> {
    const requestData: RequestEmployeeModel = this.setRequestEmployeeData(employee);
    return this.httpClient.post<any>(`${this.baseUrlApiGetway}/post`, requestData);
  }

  public deleteEmployee(employeeEmail: string): Observable<boolean> {
    return this.httpClient.delete<any>(`${this.baseUrlApiGetway}/delete/${employeeEmail}`);
  }

  private setRequestEmployeeData(employee: EmployeeModel): RequestEmployeeModel {
    return {
      name: employee.name as string,
      lastName: employee.lastName as string,
      phoneNumber: employee.phoneNumber as string,
      email: employee.email as string,
      birthdayDate: employee.birthdayDate as Date,
    };
  }
}
