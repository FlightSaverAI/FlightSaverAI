import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgOptimizedImage } from '@angular/common';

@Component({
  selector: 'shared-input',
  standalone: true,
  imports: [CommonModule, NgOptimizedImage],
  template: `
    <label for="input">{{ label() }}</label>
    <div id="input" class="input-container">
      <!-- @if(iconSrc()){ -->
      <div class="icon-container">
        <img [ngSrc]="iconSrc()" alt="" [width]="20" [height]="20" />
      </div>
      <!-- } -->
      <input [type]="type()" [placeholder]="placeholder()" />
    </div>
  `,
  styleUrl: './input.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class InputComponent {
  label = input<string>('');
  iconSrc = input<string>('');
  placeholder = input<string>('');
  type = input<string>('text');
}
