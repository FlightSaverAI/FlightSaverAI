import { Component } from '@angular/core';
import { NgClass } from '@angular/common';
import { ButtonComponent } from '@shared/ui-components';

@Component({
  selector: 'web-navbar',
  standalone: true,
  imports: [NgClass, ButtonComponent],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss',
})
export class NavbarComponent {
  isNavbarOpen!: boolean;

  public openNavbar() {
    this.isNavbarOpen = !this.isNavbarOpen;
  }
}
