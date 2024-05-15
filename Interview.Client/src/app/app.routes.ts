import { Routes } from '@angular/router';
import { CharacterComponent } from './pages/character/character.component';
import { HomeComponent } from './pages/home/home.component';

export const routes: Routes = [
  {
    path: 'employee/:emailEmployee',
    component: CharacterComponent,
  },
  {
    path: 'newemployee/:characterId',
    component: CharacterComponent,
  },
  {
    path: 'employee',
    component: HomeComponent,
  },
  {
    path: 'home',
    component: HomeComponent,
  },
  { path: '**', redirectTo: 'home', pathMatch: 'full' }
];
