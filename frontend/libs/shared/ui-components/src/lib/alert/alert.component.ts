import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AlertService } from '@shared/data-access';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { NgOptimizedImage } from '@angular/common';
import { tap } from 'rxjs';
import { toSignal } from '@angular/core/rxjs-interop';

@Component({
  selector: 'shared-alert',
  standalone: true,
  imports: [CommonModule, NgOptimizedImage],
  template: `
    <div class="alert" [@slideInOut]="isVisible ? 'visible' : 'hidden'">
      <div class="alert__type" [ngClass]="'alert__type--' + alert()?.type"></div>
      <strong>{{ alert()?.message }}</strong>
      <button class="cancel-btn" (click)="close()">
        <img ngSrc="global/assets/cancel.svg" width="20" height="20" alt="Cancel Alert" />
      </button>
    </div>
  `,
  styleUrl: './alert.component.scss',
  animations: [
    trigger('slideInOut', [
      state(
        'hidden',
        style({
          opacity: 0,
          transform: 'translateX(100%)',
        })
      ),
      state(
        'visible',
        style({
          opacity: 1,
          transform: 'translateX(0)',
        })
      ),
      transition('hidden => visible', [animate('300ms ease-out')]),
      transition('visible => hidden', [animate('300ms ease-in')]),
    ]),
  ],
})
export class AlertComponent {
  private _alertService = inject(AlertService);

  protected isVisible = false;
  protected alert = toSignal(
    this._alertService.alert$.pipe(tap((alert) => (this.isVisible = !!alert)))
  );

  protected close() {
    this._alertService.clearAlert();
  }
}
