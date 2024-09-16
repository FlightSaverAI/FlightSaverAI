import { ChangeDetectionStrategy, Component, input, output } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'shared-button',
  standalone: true,
  imports: [CommonModule],
  template: `<button class="button" [type]="type()" (click)="emitEvent.emit()">
    {{ content() }}
  </button>`,
  styleUrl: './button.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ButtonComponent {
  content = input.required();
  type = input('');
  emitEvent = output<void>();
}
