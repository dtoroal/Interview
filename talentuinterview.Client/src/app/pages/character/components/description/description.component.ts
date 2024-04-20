import { Component, Input } from '@angular/core';
import { CharacterModel } from '../../../../models/characters/character.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'description',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './description.component.html',
  styleUrl: './description.component.scss'
})
export class DescriptionComponent {
  @Input() character?: CharacterModel;

}
