import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';
import { CharacterService } from '../../../services/character/character.service';
import { HttpErrorResponse } from '@angular/common/http';
import { ResponseModel } from '../../../models/common/response.model';
import { CharacterModel } from '../../../models/characters/character.model';

@Component({
  selector: 'sidenav',
  templateUrl: './sidenav.component.html',
  styleUrl: './sidenav.component.scss'
})
export class SidenavComponent implements OnInit, OnDestroy {
  @Input() openSidenav: boolean = false;
  @Output() toggleSidenavEvent: EventEmitter<boolean> = new EventEmitter<boolean>();
  public charactersList?: Array<CharacterModel>;
  public formSearch: FormControl<string | null> = new FormControl<string | null>('');
  private formSearchSubscription?: Subscription;

  constructor(
    private characterService: CharacterService,
  ) { }

  public redirectToCharacter(characterId: number): void {
    this.toggleSidenav(false);
    window.location.href = `/character/${characterId}`;
  }

  public toggleSidenav(isOpen: boolean): void {
    this.toggleSidenavEvent.emit(isOpen);
  }

  private getCharactersByName(characterName: string): void {
    this.characterService.getCharactersByName(characterName).subscribe({
      next: (response: ResponseModel<CharacterModel>) => {
        this.charactersList = response.results;
      },
      error: (err: HttpErrorResponse) => {
        console.error(err);
        this.charactersList = [];
      }
    });
  }

  private setFormSearchSubscription(): void {
    this.formSearchSubscription =
      this.formSearch.valueChanges.subscribe((value: string | null) => {
        if (value && value?.length > 2) {
          this.getCharactersByName(value);
        } else {
          this.charactersList = [];
        }
      });
  }

  ngOnInit(): void {
    this.setFormSearchSubscription();
  }

  ngOnDestroy(): void {
    this.formSearchSubscription?.unsubscribe();
  }
}
