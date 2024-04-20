import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EmployeeModel } from '../../models/employees/employee.model';

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

}
