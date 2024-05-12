import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeModel } from '../../../../models/employees/employee.model';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { EmployeeService } from '../../../../services/employee/employee.service';
import { HttpErrorResponse } from '@angular/common/http';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';

@Component({
  selector: 'description',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDatepickerModule,
    MatNativeDateModule,
  ],
  templateUrl: './description.component.html',
  styleUrl: './description.component.scss'
})
export class DescriptionComponent implements OnInit {
  @Input() employee?: EmployeeModel;
  public employeeForm!: FormGroup;

  constructor(
    private employeeService: EmployeeService,
  ) { }

  public validateEmployee(): void {
    if (this.employeeForm.valid) {
      if (this.employee?.id) {
        this.updateEmployee();
      } else {
        this.createEmployee();
      }
    } else {
      alert("There some mistakes in the form");
    }
  }

  public deleteEmployee(): void {
    this.employeeService.deleteEmployee(this.employeeForm.get('email')?.value).subscribe(
      {
        next: (response: boolean) => {
          alert('Employee deleted');
          if (response) {
            window.location.href = '/home';
          }
        },
        error: (err: HttpErrorResponse) => {
          console.error(err);
        }
      }
    );
  }

  private updateEmployee(): void {
    this.employeeService.updateEmployee(this.employeeForm.getRawValue()).subscribe(
      {
        next: (response: boolean) => {
          alert('Employee updated');
          if (response) {
            window.location.reload();
          }
        },
        error: (err: HttpErrorResponse) => {
          console.error(err);
        }
      }
    );
  }

  private createEmployee(): void {
    this.employeeService.postEmployee(this.employeeForm.value).subscribe(
      {
        next: (response: boolean) => {
          alert('Employee created');
          if (response) {
            window.location.reload();
          }
        },
        error: (err: HttpErrorResponse) => {
          console.error(err);
        }
      }
    );
  }

  ngOnInit(): void {
    const emailPattern: RegExp = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;

    this.employeeForm = new FormGroup({
      birthdayDate: new FormControl<Date>(this.employee?.birthdayDate ?? new Date(), [Validators.required]),
      email: new FormControl<string>(this.employee?.email ?? '', [Validators.required, Validators.pattern(emailPattern)]),
      lastName: new FormControl<string>(this.employee?.lastName ?? '', [Validators.required]),
      name: new FormControl<string>(this.employee?.name ?? '', [Validators.required]),
      phoneNumber: new FormControl<string>(this.employee?.phoneNumber ?? '', [Validators.required]),
    });

    if (this.employee?.email) {
      this.employeeForm.get('email')?.disable();
    }
  }

}
