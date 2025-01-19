import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { LoaderComponent } from '@shared/ui-components';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterModule, LoaderComponent],
  template: `<shared-loader></shared-loader> <router-outlet></router-outlet>`,
})
export class AppComponent {}
