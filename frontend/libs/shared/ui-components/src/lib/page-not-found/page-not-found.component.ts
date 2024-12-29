import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="u-center">
      <div class="not-found-page">
        <div class="u-column-center u-w-100">
          <div class="error-title">
            <span>Error 404:</span><br />
            <span>Page Not Found</span>
          </div>
          <div class="error-message">
            <p>
              Uh-oh, looks like we've hit some turbulence! <br />
              It seems the page you're looking for <br />
              has taken an unexpected detour.
            </p>
          </div>
          <div class="fun-fact">
            <p>
              In the meantime, here's a fun travel fact to keep you <br />
              entertained: Did you know that the longest non-stop flight <br />
              in the world is from Singapore to New York, covering a <br />
              distance of approximately 15,349 kilometers (9,534 miles)? <br />
              Safe travels, and we hope to see you back on the right path soon! ✈️
            </p>
          </div>
        </div>
        <img src="global/assets/images/not-found.png" alt="Lost boy image" />
      </div>
    </div>
  `,
  styleUrl: './page-not-found.component.scss',
})
export class PageNotFoundComponent {}
