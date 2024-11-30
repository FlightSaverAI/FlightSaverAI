import { ChangeDetectionStrategy, Component, input, output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgOptimizedImage } from '@angular/common';

type Category = 'primary' | 'secondary';

interface imgConf {
  imgSrc: string;
  imgClass?: string;
  btnClass?: string;
}

@Component({
  selector: 'shared-button',
  standalone: true,
  imports: [CommonModule, NgOptimizedImage],
  template: `<button
    class="button"
    [ngClass]="'button__' + category()"
    [class.button__secondary--active]="isActive()"
    (click)="emitEvent.emit()"
  >
    @if(imgConf().imgSrc){
    <img
      [ngClass]="imgConf().imgClass"
      [ngSrc]="imgConf().imgSrc"
      alt=""
      width="15"
      height="15"
    />
    }

    {{ content() }}
  </button>`,
  styleUrl: './button.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ButtonComponent {
  content = input.required();
  imgConf = input<imgConf>({ imgSrc: '', imgClass: '' });
  category = input<Category>('primary');
  isActive = input(false);
  emitEvent = output<void>();
}
