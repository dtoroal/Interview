import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent implements OnInit {
  public brandTitle = 'TALENTU Interview';
  @Input() hasBackground?: boolean;
  @Output() toggleSidenavEvent: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output() toggleLoginModalEvent: EventEmitter<boolean> = new EventEmitter<boolean>();
  public isAuthenticated?: boolean = undefined;

  public toggleSidenav(): void {
    this.toggleSidenavEvent.emit(true);
  }

  public toggleLoginModal(): void {
    this.toggleLoginModalEvent.emit(true);
  }

  ngOnInit(): void {
    this.isAuthenticated = !!localStorage.getItem('token');
  }
}
