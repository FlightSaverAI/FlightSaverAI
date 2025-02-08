import { ChangeDetectionStrategy, Component, input, output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgOptimizedImage } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'shared-input-search',
  standalone: true,
  imports: [CommonModule, NgOptimizedImage, FormsModule],
  template: `
    <div class="u-flex-1">
      <label for="input">{{ label() }}</label>
      <div id="input" class="input-container">
        @if(iconSrc()){
        <div class="icon-container">
          <img [ngSrc]="iconSrc()" alt="" width="20" height="20" />
        </div>
        }
        <input
          type="text"
          [style.padding]="!iconSrc() ? '10px' : null"
          [placeholder]="placeholder()"
          (input)="setInput($event.target)"
        />
      </div>
    </div>
  `,
  styleUrl: './input-search.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class InputSearchComponent {
  label = input<string>('');
  iconSrc = input<string>('');
  placeholder = input<string>('');

  valueChangeListener = output<string>();

  protected setInput(target: EventTarget | null) {
    const inputValue = (target as HTMLInputElement).value;

    this.valueChangeListener.emit(inputValue);
  }
}
