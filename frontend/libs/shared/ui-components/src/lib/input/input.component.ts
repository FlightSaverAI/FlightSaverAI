import { ChangeDetectionStrategy, Component, forwardRef, input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgOptimizedImage } from '@angular/common';
import { ControlValueAccessor, FormGroup, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'shared-input',
  standalone: true,
  imports: [CommonModule, NgOptimizedImage],
  template: `
    <label for="input">{{ label() }}</label>
    <div id="input" class="input-container">
      @if(iconSrc()){
      <div class="icon-container">
        <img [ngSrc]="iconSrc()" alt="" [width]="20" [height]="20" />
      </div>
      }
      <input (input)="setInput($event.target)" [type]="type()" [placeholder]="placeholder()" />
    </div>
  `,
  styleUrl: './input.component.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => InputComponent),
      multi: true,
    },
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class InputComponent implements ControlValueAccessor {
  label = input<string>('');
  iconSrc = input<string>('');
  placeholder = input<string>('');
  type = input<string>('text');
  parentForm = input.required<FormGroup<any>>();
  fieldName = input.required<string>();

  onChange!: <T>(value: T) => void;
  onTouched!: () => void;

  get formField() {
    return this.parentForm().get(this.fieldName());
  }

  writeValue(obj: any): void {
    console.log(obj);
  }

  registerOnChange(fn: () => void): void {
    this.onChange = fn;
  }
  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  setInput(target: EventTarget | null): void {
    const inputValue = (target as HTMLInputElement).value;

    this.onChange(inputValue);
  }
}
