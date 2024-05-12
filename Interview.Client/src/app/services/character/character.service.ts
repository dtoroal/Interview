import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResponseModel } from '../../models/common/response.model';
import { CharacterModel } from '../../models/characters/character.model';

@Injectable({
  providedIn: 'root'
})
export class CharacterService {

  private baseUrlApi: string;
  private moduleName: string = 'character';

  constructor(
    @Inject('API_URL') apiUrl: string,
    private httpClient: HttpClient,
  ) {
    this.baseUrlApi = `${apiUrl}/${this.moduleName}`;
  }

  public getCharacters(pageNumber?: number): Observable<ResponseModel<CharacterModel>> {
    if (pageNumber) {
      return this.httpClient.get<ResponseModel<CharacterModel>>(`${this.baseUrlApi}/?page=${pageNumber}`);
    }

    return this.httpClient.get<ResponseModel<CharacterModel>>(`${this.baseUrlApi}`);
  }

  public getCharactersByName(characterName: string): Observable<ResponseModel<CharacterModel>> {
    return this.httpClient.get<ResponseModel<CharacterModel>>(`${this.baseUrlApi}?name=${characterName}`);
  }

  public getCharacter(characterId: string): Observable<CharacterModel> {
    return this.httpClient.get<CharacterModel>(`${this.baseUrlApi}/${characterId}`);
  }

}
