import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-input',
  standalone: true,
  imports: [CommonModule],
  template: `<div>
    <input />
  </div> `,
  styleUrl: './input.component.scss',
})
export class InputComponent {}
