import { Component, ElementRef, ViewChild } from '@angular/core';

@Component({
  selector: 'app-why-us-section',
  standalone: true,
  imports: [],
  templateUrl: './why-us-section.component.html',
  styleUrl: './why-us-section.component.scss',
})
export class WhyUsSectionComponent {
  @ViewChild('cards') cards!: ElementRef<HTMLElement>;

  cardIndex = 0;

  public nextCard() {
    this.cardIndex = (this.cardIndex + 1) % 4;

    this.cards.nativeElement.style.transform = `translateX(-${this.cardIndex * 100}%)`;
  }

  public previousCard() {
    this.cardIndex > 0 ? this.cardIndex-- : this.cardIndex;

    this.cards.nativeElement.style.transform = `translateX(-${this.cardIndex * 100}%)`;
  }
}
