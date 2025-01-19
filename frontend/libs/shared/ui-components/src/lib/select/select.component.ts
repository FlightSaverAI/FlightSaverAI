import { Component, forwardRef, input, output, signal } from '@angular/core';
import { CommonModule, NgClass } from '@angular/common';
import { NgOptimizedImage } from '@angular/common';
import { FormControl, FormGroup, NG_VALUE_ACCESSOR } from '@angular/forms';
import { ValidationSignComponent } from '../validation-sign/validation-sign.component';
import { FormsModule } from '@angular/forms';
import { DropdownListDirective } from '../dropdown-list/dropdown-list.directive';

@Component({
  selector: 'shared-select',
  standalone: true,
  imports: [
    CommonModule,
    NgOptimizedImage,
    ValidationSignComponent,
    NgClass,
    FormsModule,
    DropdownListDirective,
  ],
  template: ` @if(label() ){
    <label for="input">{{ label() }}</label>
    }
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
      sharedDropdownList
      [dropdownList]="options()"
      (selectedItem)="onSelect($event)"
      [(isOpen)]="isOpen"
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
        (blur)="onTouched()"
        [readOnly]="true"
      />

      <div class="u-align-center u-padding-1">
        <img
          class="dropdown-arrow"
          ngSrc="global/assets/arrow-select.svg"
          alt=""
          width="14"
          height="14"
          [ngClass]="{ rotated: isOpen }"
        />
      </div>

      @if(formField){
      <shared-validation-sign [formField]="formField"></shared-validation-sign>
      }
    </div>

    <p
      class="error-message"
      [style.visibility]="
        hasValidator && !formField.pristine && formField.status === 'INVALID' ? 'visible' : 'hidden'
      "
    >
      Field {{ label() }} {{ valueChangeListener() ? 'is invalid' : 'is required' }}
    </p>`,
  styleUrl: './select.component.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => SelectComponent),
      multi: true,
    },
  ],
})
export class SelectComponent {
  parentForm = input.required<FormGroup<any>>();
  fieldName = input.required<string>();
  label = input<string>('');
  iconSrc = input<string>('');
  placeholder = input<string>('');
  type = input<string>('text');
  options = input<any>();
  emitChangedValue = output<any>();

  isOpen = false;
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

  writeValue(value: any): void {
    if (!value) {
      this.valueChangeListener.set('');
      return;
    }

    if (value === undefined) {
      this.value = null;
    } else if (value instanceof Object) {
      this.value = value.name;
      this.valueChangeListener.set(value.name);
    } else {
      this.value = value;
      this.valueChangeListener.set(value);
    }
  }

  registerOnChange(fn: () => void): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  onSelect(value: any): void {
    this.onTouched();

    if (typeof value === 'object') {
      this.onChange(value);
      this.emitChangedValue.emit(value);
      this.value = value.name;
      return;
    }

    this.onChange(value);
    this.value = value;
  }
}
