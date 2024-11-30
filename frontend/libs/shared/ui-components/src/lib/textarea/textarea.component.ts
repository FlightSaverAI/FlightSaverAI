import { Component, forwardRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InputComponent } from '../input/input.component';
import { ValidationSignComponent } from '../validation-sign/validation-sign.component';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'shared-textarea',
  standalone: true,
  imports: [CommonModule, ValidationSignComponent, FormsModule],
  template: `<label for="textarea">{{ label() }}</label>

    <div id="textarea" class="textarea-container">
      <textarea
        [ngModel]="value"
        class="textarea"
        [placeholder]="placeholder()"
        (input)="setInput($event.target)"
        (blur)="onTouched()"
        type="text"
        autocomplete="off"
        role="presentation"
      ></textarea>

      @if(formField){
      <shared-validation-sign [formField]="formField"></shared-validation-sign>
      }
    </div> `,
  styleUrl: './textarea.component.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => TextareaComponent),
      multi: true,
    },
  ],
})
export class TextareaComponent extends InputComponent {
  constructor() {
    super();
  }
}
