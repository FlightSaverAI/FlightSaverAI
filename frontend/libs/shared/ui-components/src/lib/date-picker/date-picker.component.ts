import dayjs from 'dayjs';
import {
  Component,
  ElementRef,
  forwardRef,
  HostListener,
  inject,
  input,
  signal,
} from '@angular/core';
import { CommonModule, NgClass } from '@angular/common';
import { NgOptimizedImage } from '@angular/common';
import { FormControl, FormGroup, NG_VALUE_ACCESSOR } from '@angular/forms';
import { ValidationSignComponent } from '../validation-sign/validation-sign.component';
import { FormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { provideNativeDateAdapter } from '@angular/material/core';
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'shared-date-picker',
  standalone: true,
  imports: [
    CommonModule,
    NgOptimizedImage,
    ValidationSignComponent,
    NgClass,
    FormsModule,
    MatDatepickerModule,
    MatCardModule,
  ],
  template: `@if(label() ){
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
    >
      @if(iconSrc()){
      <div class="icon-container">
        <img [ngSrc]="iconSrc()" alt="" width="20" height="20" />
      </div>
      }
      <input
        [(ngModel)]="value"
        [style.padding]="!iconSrc() ? '10px' : null"
        [type]="type()"
        [placeholder]="placeholder()"
        (blur)="onTouched()"
        [readOnly]="true"
      />

      <div class="u-align-center u-padding-vertical-05" (click)="openDate()">
        <img class="hehe" [ngSrc]="'global/assets/date-picker.svg'" alt="" width="25" height="25" />
      </div>

      @if(formField){
      <shared-validation-sign [formField]="formField"></shared-validation-sign>
      }
      <mat-card class="card" [style.display]="chuj ? 'block' : 'none'">
        <mat-calendar [(selected)]="value" (selectedChange)="onDateChange($event)"></mat-calendar>
      </mat-card>
    </div>

    <p
      class="error-message"
      [style.visibility]="
        hasValidator && !formField.pristine && formField.status === 'INVALID' ? 'visible' : 'hidden'
      "
    >
      Field {{ label() }} {{ valueChangeListener() ? 'is invalid' : 'is required' }}
    </p>`,
  styleUrl: './date-picker.component.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => DatePickerComponent),
      multi: true,
    },
    provideNativeDateAdapter(),
  ],
})
export class DatePickerComponent {
  parentForm = input.required<FormGroup<any>>();
  fieldName = input.required<string>();
  label = input<string>('');
  iconSrc = input<string>('');
  placeholder = input<string>('');
  type = input<string>('text');
  value: any;

  chuj = false;
  valueChangeListener = signal('');

  elementRef = inject(ElementRef);

  onChange!: <T>(value: T) => void;
  onTouched!: () => void;

  get formField(): FormControl {
    return this.parentForm().get(this.fieldName()) as FormControl;
  }

  get hasValidator(): boolean {
    return this.formField && this.formField.validator ? true : false;
  }

  @HostListener('document:click', ['$event'])
  handleClickOutside(event: Event) {
    if (!this.elementRef.nativeElement.contains(event.target) && this.chuj) {
      this.chuj = false;
    }
  }

  writeValue(value: any) {
    if (!value) {
      this.valueChangeListener.set('');
      return;
    }

    if (value === undefined) {
      this.value = null;
    } else {
      this.value = dayjs(value).format('DD/MM/YYYY');
      this.valueChangeListener.set(value);
    }
  }

  registerOnChange(fn: () => void): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  openDate() {
    this.chuj = !this.chuj;
  }

  onDateChange(value: any) {
    const formatter = new Intl.DateTimeFormat('en-GB', {
      day: '2-digit',
      month: '2-digit',
      year: 'numeric',
    });

    this.onChange(value.toISOString());
    this.value = formatter.format(value);
    this.chuj = !this.chuj;
  }
}
