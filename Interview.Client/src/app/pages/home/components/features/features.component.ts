import { Component, Input, OnInit } from '@angular/core';
import { CharacterModel } from '../../../../models/characters/character.model';
import { HttpErrorResponse } from '@angular/common/http';
import { EmployeeService } from '../../../../services/employee/employee.service';
import { EmployeeModel } from '../../../../models/employees/employee.model';

@Component({
  selector: 'features',
  templateUrl: './features.component.html',
  styleUrl: './features.component.scss'
})
export class FeaturesComponent implements OnInit {
  @Input() charactersList?: Array<CharacterModel>;
  @Input() employeesList?: Array<EmployeeModel>;
  @Input() totalItems?: number;
  public currentPage: number = 1;
  public itemsPerPage: number = 10;

  constructor(
    private employeeService: EmployeeService,
  ) { }

  public onPageChange(page: number): void {
    this.currentPage = page;
    this.getEmployees(this.currentPage);
  }

  public redirectToEmployee(emailEmployee?: string, characterId?: number): void {
    const parameter = emailEmployee ? emailEmployee : characterId;
    window.location.href = `/${emailEmployee ? 'employee' : 'newemployee'}/${parameter}`;
  }

  private getEmployees(pageNumber?: number): void {
    this.employeeService.getEmployees().subscribe({
      next: (
        response: Array<EmployeeModel> | EmployeeModel) => {
        const employees = response as Array<EmployeeModel>;
        this.employeesList = employees;
        this.totalItems = employees.length;
      },
      error: (err: HttpErrorResponse) => {
        console.error(err);
      }
    });
  }

  ngOnInit(): void {
    this.getEmployees(1);
  }
}
