import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {
  public brandTitle = 'TALENTU Interview';
  @Input() hasBackground?: boolean;
  @Output() toggleSidenavEvent: EventEmitter<boolean> = new EventEmitter<boolean>();

  public toggleSidenav(): void {
    this.toggleSidenavEvent.emit(true);
  }

}
