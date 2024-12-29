import { Component, input } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'shared-validation-sign',
  standalone: true,
  imports: [],
  template: `<div class="validator">
    @if(hasValidator && !this.formField().pristine && formField().status === 'INVALID'){
    <img width="25" height="25" src="global/assets/error.svg" alt="Error Icon" />
    } @if(hasValidator && !this.formField().pristine && formField().status === 'VALID'){
    <img width="25" height="25" src="global/assets/pass.svg" alt="Complete Icon" />
    }
  </div> `,
  styleUrl: './validation-sign.component.scss',
})
export class ValidationSignComponent {
  formField = input.required<FormControl>();

  get hasValidator(): boolean {
    return this.formField() && this.formField().validator ? true : false;
  }
}
