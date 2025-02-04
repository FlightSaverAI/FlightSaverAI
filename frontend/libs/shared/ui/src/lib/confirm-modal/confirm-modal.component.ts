import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModalComponent } from '@shared/ui-components';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'shared-confirm-modal',
  standalone: true,
  imports: [CommonModule, ModalComponent],
  template: `<shared-modal [title]="modalData.title" (confirmEvent)="confirm()">
    <p class="content">{{ modalData.content }}</p>
  </shared-modal>`,
  styleUrl: './confirm-modal.component.scss',
})
export class ConfirmModalComponent {
  private _modalRef = inject(MatDialogRef);
  protected modalData = inject(MAT_DIALOG_DATA);

  protected confirm() {
    this._modalRef.close(true);
  }
}
