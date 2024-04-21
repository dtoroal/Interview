import { Component, Input, OnInit } from '@angular/core';
import { CharacterModel } from '../../../../models/characters/character.model';
import { ResponseModel } from '../../../../models/common/response.model';
import { HttpErrorResponse } from '@angular/common/http';
import { CharacterService } from '../../../../services/character/character.service';
import { EmployeeService } from '../../../../services/employee/employee.service';
import { EmployeeModel } from '../../../../models/employees/employee.model';
import { forkJoin } from 'rxjs';

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
    private characterService: CharacterService,
    private employeeService: EmployeeService,
  ) { }

  public onPageChange(page: number): void {
    this.currentPage = page;
    this.getEmployees(this.currentPage);
  }

  public redirectToEmployee(employeeId: string, characterId?: number): void {
    window.location.href = `/employee/${employeeId}/${characterId}`;
  }

  private getEmployees(pageNumber?: number): void {
    forkJoin(
      {
        employees: this.employeeService.getEmployees(),
        characters: this.characterService.getCharacters(),
      }
    ).subscribe({
      next: (
        response: {
          employees: Array<EmployeeModel> | EmployeeModel,
          characters: ResponseModel<CharacterModel>
        }) => {
        this.charactersList = response.characters.results;
        this.employeesList = response.employees as Array<EmployeeModel>;
        this.totalItems = (response.employees as Array<EmployeeModel>).length;
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
