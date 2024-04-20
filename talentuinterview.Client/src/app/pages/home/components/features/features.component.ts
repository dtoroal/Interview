import { Component, Input, OnInit } from '@angular/core';
import { CharacterModel } from '../../../../models/characters/character.model';
import { ResponseModel } from '../../../../models/common/response.model';
import { HttpErrorResponse } from '@angular/common/http';
import { CharacterService } from '../../../../services/character/character.service';

@Component({
  selector: 'features',
  templateUrl: './features.component.html',
  styleUrl: './features.component.scss'
})
export class FeaturesComponent implements OnInit {
  @Input() charactersList?: Array<CharacterModel>;
  @Input() totalItems?: number;
  public currentPage: number = 1;
  public itemsPerPage: number = 10;

  constructor(
    private characterService: CharacterService,
  ) { }

  public onPageChange(page: number): void {
    this.currentPage = page;
    this.getCharacters(this.currentPage);
  }

  public redirectToCharacter(characterId: number): void {
    window.location.href = `/character/${characterId}`;
  }

  private getCharacters(pageNumber?: number): void {
    this.characterService.getCharacters(pageNumber).subscribe({
      next: (response: ResponseModel<CharacterModel>) => {
        const res = (response as ResponseModel<CharacterModel>);
        this.charactersList = res.results;
        this.totalItems = res.info.count;
      },
      error: (err: HttpErrorResponse) => {
        console.error(err);
      }
    });
  }

  ngOnInit(): void {
    this.getCharacters(1);
  }
}
