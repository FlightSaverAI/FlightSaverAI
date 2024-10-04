import { Component } from '@angular/core';
import { ButtonComponent } from '@shared/ui-components';

@Component({
  selector: 'web-main',
  standalone: true,
  imports: [ButtonComponent],
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss',
})
export class MainComponent {}
