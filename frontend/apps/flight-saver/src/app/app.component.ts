import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { LoaderComponent } from '@shared/ui-components';
import { AlertComponent } from '@shared/ui-components';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterModule, LoaderComponent, AlertComponent],
  template: ` <shared-alert></shared-alert>
    <shared-loader></shared-loader>
    <router-outlet></router-outlet>`,
})
export class AppComponent {}
