import { ChangeDetectionStrategy, Component, Input, output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { DropdownConfig } from '@shared/models';

@Component({
  selector: 'shared-dropdown',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `<div class="dropdown">
    <ul class="dropdown__list">
      @for(item of dropdownConfig; track item){
      <li class="dropdown__item" (click)="selectedOption.emit(item.field)">
        <p class="dropdown__text">{{ item.field }}</p>
      </li>
      }
    </ul>
  </div> `,
  styleUrl: './dropdown.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DropdownComponent {
  @Input() public dropdownConfig!: DropdownConfig[];
  public selectedOption = output<string>();
}
