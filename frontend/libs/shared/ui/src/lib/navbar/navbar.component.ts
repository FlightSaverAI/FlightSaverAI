import { ChangeDetectionStrategy, Component, inject, input, OnInit, signal } from '@angular/core';
import { NgClass } from '@angular/common';
import { ButtonComponent } from '@shared/ui-components';
import { RouterModule } from '@angular/router';
import { NgOptimizedImage } from '@angular/common';
import { Router } from '@angular/router';
import { DropdownDirective } from '@shared/ui-components';
import { DropdownConfig, NavConfig } from '@shared/models';

@Component({
  selector: 'shared-navbar',
  standalone: true,
  imports: [NgClass, ButtonComponent, RouterModule, NgOptimizedImage, DropdownDirective],
  template: `<nav class="nav">
    <div class="nav__logo">
      <img src="global/assets/flight-saver-logo.svg" alt="Flight Saver Logo" />
      <span class="nav__logo-title">FlightSaver</span>
    </div>
    <div class="nav__burger" (click)="openNavbar()">
      <div class="nav__burger-line" [ngClass]="{ 'nav__burger-line--open': isNavbarOpen() }"></div>
      <div class="nav__burger-line" [ngClass]="{ 'nav__burger-line--open': isNavbarOpen() }"></div>
      <div class="nav__burger-line" [ngClass]="{ 'nav__burger-line--open': isNavbarOpen() }"></div>
    </div>
    <ul class="nav__list" [ngClass]="{ 'nav__list--open': isNavbarOpen() }">
      @for(item of navConfig(); let index = $index; track index ){
      <!-- prettier-ignore -->
      @if(item.type === 'list'){
      <li
        class="nav__list-item"
        [class.active]="activeIndex() === index"
        [routerLink]="item.routerLink"
        (click)="activeIndex.set(index)"
      >
        {{ item.name }}
      </li>
      }
      <!-- prettier-ignore -->
      @if(item.type === 'button'){
      <shared-button
        class="nav__list-btn"
        [content]="item.name"
        [routerLink]="item.routerLink"
        (emitEvent)="activeIndex.set(index)"
      ></shared-button>
      }
      <!-- prettier-ignore -->
      @if(item.image && item.type === 'photo'){
      <div
        class="nav__list-photo"
        sharedDropdown
        [dropdownConfig]="dropdownConfig() || []"
        (selectedOption)="handleSelectedOption($event)"
      >
        <img
          [ngSrc]="userPhotoSrc() || defaultUserPhotoSrc()"
          [alt]="item.image.alt"
          [width]="item.image.width"
          [height]="item.image.height"
        />
      </div>
      }}
    </ul>
  </nav> `,
  styleUrl: './navbar.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class NavbarComponent implements OnInit {
  public navConfig = input.required<NavConfig[]>();
  public dropdownConfig = input<DropdownConfig[] | undefined>();
  public userPhotoSrc = input<string>();

  protected activeIndex = signal(0);
  protected isNavbarOpen = signal(false);
  protected defaultUserPhotoSrc = signal('global/assets/default-user-photo.png');

  private _router = inject(Router);

  public ngOnInit(): void {
    this.activeIndex.set(this._findActiveNavIndex());
  }

  private _findActiveNavIndex() {
    return this.navConfig().findIndex(({ name }) => this._router.url.includes(name.toLowerCase()));
  }

  protected openNavbar() {
    this.isNavbarOpen.update((state) => !state);
  }

  protected handleSelectedOption(selectedOption: string) {
    this.dropdownConfig()?.map(({ field, action }) => field === selectedOption && action());
  }
}
