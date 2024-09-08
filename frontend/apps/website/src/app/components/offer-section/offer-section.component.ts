import { Component, ElementRef, ViewChild } from '@angular/core';

@Component({
  selector: 'web-offer-section',
  standalone: true,
  imports: [],
  templateUrl: './offer-section.component.html',
  styleUrl: './offer-section.component.scss',
})
export class OfferSectionComponent {
  @ViewChild('cards') cards!: ElementRef<HTMLElement>;

  cardIndex = 0;

  public nextCard() {
    this.cardIndex = (this.cardIndex + 1) % 4;

    this.cards.nativeElement.style.transform = `translateX(-${this.cardIndex * 100}%)`;
  }

  public previousCard() {
    this.cardIndex = this.cardIndex > 0 ? --this.cardIndex : this.cardIndex;

    this.cards.nativeElement.style.transform = `translateX(-${this.cardIndex * 100}%)`;
  }
}
