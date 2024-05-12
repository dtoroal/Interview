import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent implements OnInit {
  public brandTitle = 'INTERVIEW Test';
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

  public closeSession(): void {
    localStorage.removeItem('token');
    window.location.href = '/home';
  }

  ngOnInit(): void {
    this.isAuthenticated = !!localStorage.getItem('token');
  }
}
