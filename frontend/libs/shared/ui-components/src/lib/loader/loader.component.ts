import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
// eslint-disable-next-line @nx/enforce-module-boundaries
import { LoaderService } from '@shared/data-access';
import { toSignal } from '@angular/core/rxjs-interop';

@Component({
  selector: 'shared-loader',
  standalone: true,
  imports: [CommonModule],
  template: `
    @if(isLoading()){

    <div class="loader-container">
      <div class="loader">
        <span></span>
        <div id="dot-1" class="dot"></div>
        <div id="dot-2" class="dot"></div>
        <div id="dot-3" class="dot"></div>
        <div id="dot-4" class="dot"></div>
        <div id="dot-5" class="dot"></div>
      </div>
      <span class="loading-text">Loading...</span>
    </div>
    }
  `,
  styleUrl: './loader.component.scss',
})
export class LoaderComponent {
  isLoading = toSignal(inject(LoaderService).isLoading$);
}
