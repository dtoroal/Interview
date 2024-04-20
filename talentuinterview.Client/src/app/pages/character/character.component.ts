import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { NavbarModule } from '../../shared/components/navbar/navbar.module';
import { SidenavModule } from '../../shared/components/sidenav/sidenav.module';
import { ActivatedRoute, Params } from '@angular/router';
import { Subscription } from 'rxjs';
import { CharacterService } from '../../services/character/character.service';
import { HttpErrorResponse } from '@angular/common/http';
import { CharacterModel } from '../../models/characters/character.model';
import { DescriptionComponent } from './components/description/description.component';

@Component({
  selector: 'character',
  standalone: true,
  imports: [CommonModule, NavbarModule, NavbarModule, SidenavModule, DescriptionComponent,],
  templateUrl: './character.component.html',
  styleUrl: './character.component.scss'
})
export class CharacterComponent implements OnInit, OnDestroy {

  public character?: CharacterModel;
  public openSidenav: boolean = false;
  private routeSub?: Subscription;

  constructor(
    private route: ActivatedRoute,
    private characterService: CharacterService,
  ) { }

  public toggleSidenavEvent(event: boolean): void {
    this.openSidenav = event;
  }

  private getCharacter(characterId: string): void {
    this.characterService.getCharacter(characterId).subscribe({
      next: (response: CharacterModel) => {
        this.character = response;
      },
      error: (err: HttpErrorResponse) => {
        console.error(err);
      }
    });
  }

  private routingSubscription(): void {
    this.routeSub = this.route.params.subscribe((params: Params) => {
      if (params['id']) {
        const id = params['id'];
        this.getCharacter(id);
      }
    });
  }

  ngOnInit() {
    this.routingSubscription();
  }

  ngOnDestroy() {
    this.routeSub?.unsubscribe();
  }
}
