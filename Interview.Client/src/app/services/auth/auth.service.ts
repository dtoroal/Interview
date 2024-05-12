import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EmployeeModel } from '../../models/employees/employee.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrlApiGetway: string;
  private moduleName: string = 'auth';

  constructor(
    @Inject('APIGETWAY_URL') apiGetwayUrl: string,
    private httpClient: HttpClient,
  ) {
    this.baseUrlApiGetway = `${apiGetwayUrl}/${this.moduleName}`;
  }

  public login(employee: EmployeeModel): Observable<any> {
    const requestOptions: Object = {
      /* other options here */
      responseType: 'text'
    }
    return this.httpClient.post<any>(`${this.baseUrlApiGetway}/login`, employee, requestOptions);
  }
}
