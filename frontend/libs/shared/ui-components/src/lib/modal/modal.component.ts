import { Component, inject, input, output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonComponent } from '../button/button.component';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'shared-modal',
  standalone: true,
  imports: [CommonModule, ButtonComponent, MatDialogModule],
  template: `<div class="modal">
    <div class="modal__header">
      <h3>{{ title() }}</h3>
    </div>

    <div class="modal__body">
      <ng-content></ng-content>
    </div>

    <div class="modal__footer">
      <shared-button content="Cancel" category="secondary" (emitEvent)="close()"></shared-button>
      <shared-button content="Confirm" (emitEvent)="confirmEvent.emit()"></shared-button>
    </div>
  </div> `,
  styleUrl: './modal.component.scss',
})
export class ModalComponent {
  title = input.required<string>();
  confirmEvent = output<void>();

  private _modalRef = inject(MatDialogRef);

  protected close(): void {
    this._modalRef.close(null);
  }
}
