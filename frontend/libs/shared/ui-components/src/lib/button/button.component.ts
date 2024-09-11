import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-button',
  standalone: true,
  imports: [CommonModule],
  template: `<p>button works!</p>`,
  styleUrl: './button.component.scss',
})
export class ButtonComponent {
  
}
