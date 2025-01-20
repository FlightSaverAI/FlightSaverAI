import { Component, ElementRef, HostListener, inject, Input, input, output } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'shared-dropdown-list',
  standalone: true,
  imports: [CommonModule],
  template: `<div class="dropdown">
    <ul class="dropdown__list">
      @for(item of filterProps; track item){
      <li class="dropdown__element" (click)="selectedItem.emit(item)">
        <p class="dropdown__item-name">{{ item?.name ? item.name : item }}</p>
      </li>
      }
    </ul>
  </div> `,
  styleUrl: './dropdown-list.component.scss',
})
export class DropdownListComponent {
  @Input() filterProps!: any;

  clickOutside = output();
  selectedItem = output<any>();

  elementRef = inject(ElementRef);

  @HostListener('document:click', ['$event.target'])
  onClick(target: EventTarget) {
    const clickedInside = this.elementRef.nativeElement.contains(target);
    if (!clickedInside) {
      this.clickOutside.emit();
    }
  }
}
