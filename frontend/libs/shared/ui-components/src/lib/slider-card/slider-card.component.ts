import { Component, ElementRef, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'shared-slider-card',
  standalone: true,
  imports: [CommonModule],
  template: `<div class="slider-container">
    <div class="slider">
      <div #cards class="cards">
        <ng-content></ng-content>
      </div>
    </div>
    <div class="previous-btn-container">
      <button class="previous-btn" (click)="previousCard()">
        <img class="arrow-left" src="global/assets/slider-arrow.svg" alt="" />
      </button>
    </div>
    <div class="next-btn-container">
      <button class="next-btn" (click)="nextCard()">
        <img class="arrow-right" src="global/assets/slider-arrow.svg" alt="" />
      </button>
    </div>
  </div>`,
  styleUrl: './slider-card.component.scss',
})
export class SliderCardComponent {
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
