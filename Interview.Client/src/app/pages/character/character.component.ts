import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { NavbarModule } from '../../shared/components/navbar/navbar.module';
import { SidenavModule } from '../../shared/components/sidenav/sidenav.module';
import { ActivatedRoute, Params } from '@angular/router';
import { forkJoin, Subscription } from 'rxjs';
import { CharacterService } from '../../services/character/character.service';
import { HttpErrorResponse } from '@angular/common/http';
import { CharacterModel } from '../../models/characters/character.model';
import { DescriptionComponent } from './components/description/description.component';
import { EmployeeService } from '../../services/employee/employee.service';
import { EmployeeModel } from '../../models/employees/employee.model';

@Component({
  selector: 'character',
  standalone: true,
  imports: [
    CommonModule,
    NavbarModule,
    NavbarModule,
    SidenavModule,
    DescriptionComponent,
  ],
  templateUrl: './character.component.html',
  styleUrl: './character.component.scss'
})
export class CharacterComponent implements OnInit, OnDestroy {

  public employee?: EmployeeModel;
  public openSidenav: boolean = false;
  private routeSub?: Subscription;

  constructor(
    private route: ActivatedRoute,
    private characterService: CharacterService,
    private employeeService: EmployeeService,
  ) { }

  public toggleSidenavEvent(event: boolean): void {
    this.openSidenav = event;
  }

  private getEmployee(emailEmployee: string): void {

    this.employeeService.getEmployees(emailEmployee).subscribe(
      {
        next: (response: Array<EmployeeModel> | EmployeeModel) => {
          this.employee = response as EmployeeModel;
        },
        error: (err: HttpErrorResponse) => {
          console.error(err);
          window.location.href = '/home';
        }
      }
    );
  }

  private routingSubscription(): void {
    this.routeSub = this.route.params.subscribe((params: Params) => {
      if (params['emailEmployee']) {
        const emailEmployee = params['emailEmployee'];
        this.getEmployee(emailEmployee);
      } else {
        this.setNewCharacterImage(params);
      }
    });
  }

  private setNewCharacterImage(params: Params) {
    const characterId: string = String(Number.parseInt(params['characterId']) + 1);
    if (characterId) {
      this.characterService.getCharacter(characterId).subscribe(
        {
          next: (response: CharacterModel) => {
            this.employee = {};
            this.employee.image = response.image;
          },
          error: (err: HttpErrorResponse) => {
            console.log(err);
          }
        }
      );
    }
  }

  ngOnInit() {
    this.routingSubscription();
  }

  ngOnDestroy() {
    this.routeSub?.unsubscribe();
  }
}
