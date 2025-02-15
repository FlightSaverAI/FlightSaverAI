import { ChangeDetectionStrategy, Component, forwardRef, input, signal } from '@angular/core';
import { CommonModule, NgClass } from '@angular/common';
import { NgOptimizedImage } from '@angular/common';
import {
  AbstractControl,
  ControlValueAccessor,
  FormControl,
  FormGroup,
  NG_VALUE_ACCESSOR,
} from '@angular/forms';
import { ValidationSignComponent } from '../validation-sign/validation-sign.component';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'shared-input',
  standalone: true,
  imports: [CommonModule, NgOptimizedImage, ValidationSignComponent, NgClass, FormsModule],
  template: `
    <div [ngClass]="{ disabled: isDisabled() }">
      <label for="input" *ngIf="label()">{{ label() }}{{ hasRequiredValidator ? '*' : '' }}</label>
      <div
        id="input"
        class="input-container"
        [ngClass]="
          !formField.pristine
            ? {
                'input-container--error': hasValidator && formField.status === 'INVALID',
                'input-container--success': hasValidator && formField.status === 'VALID'
              }
            : {}
        "
      >
        @if(iconSrc()){
        <div class="icon-container">
          <img [ngSrc]="iconSrc()" alt="" width="20" height="20" />
        </div>
        }
        <input
          [ngModel]="value"
          [style.padding]="!iconSrc() ? '10px' : null"
          [type]="type()"
          [placeholder]="placeholder()"
          [readOnly]="isReadOnly()"
          (input)="setInput($event.target)"
          (blur)="onTouched()"
        />

        @if(formField){
        <shared-validation-sign [formField]="formField"></shared-validation-sign>
        }
      </div>

      <p
        class="error-message"
        [style.visibility]="
          hasValidator && !formField.pristine && formField.status === 'INVALID'
            ? 'visible'
            : 'hidden'
        "
      >
        Field {{ label() }} {{ valueChangeListener() ? 'is invalid' : 'is required' }}
      </p>
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
  isReadOnly = input<boolean>(false);
  isDisabled = input<boolean>(false);

  valueChangeListener = signal('');
  value: any;

  onChange!: <T>(value: T) => void;
  onTouched!: () => void;

  get formField(): FormControl {
    return this.parentForm().get(this.fieldName()) as FormControl;
  }

  get hasValidator(): boolean {
    return this.formField && this.formField.validator ? true : false;
  }

  get hasRequiredValidator() {
    const validator = this.formField.validator;

    if (!validator) {
      return false;
    }

    const validationResult = validator({} as AbstractControl);
    return validationResult && validationResult['required'] ? true : false;
  }

  writeValue(value: any): void {
    if (!value) {
      this.valueChangeListener.set('');
      return;
    }

    if (value === undefined) {
      this.value = null;
    } else {
      this.value = value;
    }
    this.valueChangeListener.set(value);
  }

  registerOnChange(fn: () => void): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  setInput(target: EventTarget | null): void {
    const inputValue = (target as HTMLInputElement).value;

    this.valueChangeListener.set(inputValue);

    this.onChange(inputValue);
  }
}
