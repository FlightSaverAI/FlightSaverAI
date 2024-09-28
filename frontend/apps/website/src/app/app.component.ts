import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MainComponent } from './components/main/main.component';
import { OfferSectionComponent } from './components/offer-section/offer-section.component';
import { WhyUsSectionComponent } from './components/why-us-section/why-us-section.component';
import { StreamlinedSoaringSectionComponent } from './components/streamlined-soaring-section/streamlined-soaring-section.component';
import { NewsletterComponent } from './components/newsletter/newsletter.component';
import { SocialsComponent } from './components/socials/socials.component';
import { FooterComponent } from './components/footer/footer.component';
import { NavbarComponent, NavConfig } from '@shared/ui-components';

@Component({
  selector: 'web-root',
  standalone: true,
  imports: [
    RouterOutlet,
    MainComponent,
    OfferSectionComponent,
    NavbarComponent,
    WhyUsSectionComponent,
    StreamlinedSoaringSectionComponent,
    NewsletterComponent,
    SocialsComponent,
    FooterComponent,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  navConfig: NavConfig[] = [
    {
      type: 'list',
      name: 'Home',
      routerLink: '',
    },
    {
      type: 'list',
      name: 'About',
      routerLink: '',
    },
    {
      type: 'list',
      name: 'Newsletter',
      routerLink: '',
    },
    {
      type: 'button',
      name: 'SIGN IN',
      routerLink: 'http://localhost:4201/login',
    },
  ];
}
