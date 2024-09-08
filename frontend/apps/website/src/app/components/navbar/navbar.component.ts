import { Component } from '@angular/core';
import { NgClass } from '@angular/common';

@Component({
  selector: 'web-navbar',
  standalone: true,
  imports: [NgClass],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss',
})
export class NavbarComponent {
  isNavbarOpen!: boolean;

  public openNavbar() {
    this.isNavbarOpen = !this.isNavbarOpen;
  }
}
