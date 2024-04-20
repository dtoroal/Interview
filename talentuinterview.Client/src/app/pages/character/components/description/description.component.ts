import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeModel } from '../../../../models/employees/employee.model';

@Component({
  selector: 'description',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './description.component.html',
  styleUrl: './description.component.scss'
})
export class DescriptionComponent {
  @Input() employee?: EmployeeModel;

}
