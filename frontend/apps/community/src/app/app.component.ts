import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TravelBoardComponent } from '@flight-saver/community/features';
@Component({
  standalone: true,
  imports: [RouterModule, TravelBoardComponent],
  selector: 'app-root',
  template: `<community-travel-board></community-travel-board>`,
  styleUrl: './app.component.scss',
})
export class AppComponent {}
