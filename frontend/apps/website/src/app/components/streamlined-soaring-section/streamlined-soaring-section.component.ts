import { Component, ElementRef, ViewChild } from '@angular/core';

@Component({
  selector: 'web-streamlined-soaring-section',
  standalone: true,
  imports: [],
  templateUrl: './streamlined-soaring-section.component.html',
  styleUrl: './streamlined-soaring-section.component.scss',
})
export class StreamlinedSoaringSectionComponent {
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
