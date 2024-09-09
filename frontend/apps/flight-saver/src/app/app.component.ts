import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  standalone: true,
  imports: [RouterModule],
  selector: 'app-root',
  template: `<p>Hello World</p>`,
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'flight-saver';
}
