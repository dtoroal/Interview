import { Component, OnInit } from '@angular/core';
import { CharacterModel } from '../../models/characters/character.model';

@Component({
  selector: 'home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {
  public openSidenav: boolean = false;
  public openLoginModal: boolean = false;
  public charactersList?: Array<CharacterModel>;
  public isAuthenticated?: boolean = undefined;

  public toggleSidenavEvent(event: boolean): void {
    this.openSidenav = event;
  }

  public toggleLoginModalEvent(event: boolean): void {
    this.openLoginModal = event;
  }

  ngOnInit(): void {
    this.isAuthenticated = !!localStorage.getItem('token');
  }
}
