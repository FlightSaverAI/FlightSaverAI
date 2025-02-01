import { ChangeDetectionStrategy, Component, inject, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModalComponent } from '@shared/ui-components';
import { ButtonComponent } from '@shared/ui-components';
import { NgOptimizedImage } from '@angular/common';
import { ImageCroppedEvent, ImageCropperComponent } from 'ngx-image-cropper';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  standalone: true,
  imports: [CommonModule, ModalComponent, ButtonComponent, NgOptimizedImage, ImageCropperComponent],
  template: `
    <shared-modal [title]="modalTitle()" (confirmEvent)="confirmCrop()">
      <div
        class="body-container"
        [ngClass]="isDragging() ? 'dragging' : ''"
        (dragover)="onDragOver($event)"
        (dragleave)="onDragLeave($event)"
        (drop)="onDrop($event)"
      >
        @if(!isCropping()) {
        <div class="paste-photo-container">
          <img [ngSrc]="'global/assets/upload.svg'" width="60" height="60" alt="" />
          <div class="drag-and-drop">
            <p>Drag photos here</p>
            <span>OR</span>
            <shared-button
              content="Select from device"
              (click)="triggerFileInput(fileInput)"
            ></shared-button>
            <input
              #fileInput
              type="file"
              accept="image/*"
              (change)="onFileSelected($event)"
              style="display: none"
            />
          </div>
        </div>
        } @if(isCropping()) {
        <div class="cropper-container">
          <image-cropper
            [imageFile]="imageFile()"
            [maintainAspectRatio]="true"
            [aspectRatio]="photoType === 'Profile' ? 1 / 1 : 16 / 9"
            [cropperMinHeight]="photoType === 'Profile' ? 250 : 400"
            [cropperMinHeight]="photoType === 'Profile' ? 250 : 400"
            [roundCropper]="false"
            (imageCropped)="onImageCropped($event)"
          ></image-cropper>
        </div>
        }
      </div>
    </shared-modal>
  `,
  changeDetection: ChangeDetectionStrategy.OnPush,
  styleUrl: './cropped-photo-modal.component.scss',
})
export class CroppedPhotoModalComponent {
  protected photoType = inject(MAT_DIALOG_DATA).photoType;
  private _modalRef = inject(MatDialogRef);

  modalTitle = signal(`Upload ${this.photoType} Photo`);

  imageFile: WritableSignal<File | undefined> = signal(undefined);
  croppedImage: WritableSignal<Blob | null | undefined> = signal(undefined);

  isCropping = signal(false);
  isDragging = signal(false);

  protected onDragOver(event: DragEvent) {
    event.preventDefault();
    this.isDragging.set(true);
  }

  protected onDragLeave(event: DragEvent) {
    event.preventDefault();
    this.isDragging.set(false);
  }

  protected onDrop(event: DragEvent) {
    event.preventDefault();
    this.isDragging.set(false);

    if (event.dataTransfer && event.dataTransfer.files.length > 0) {
      this.imageFile.set(event.dataTransfer.files[0]);
      this.isCropping.set(true);
    }
  }

  protected triggerFileInput(fileInput: HTMLInputElement) {
    fileInput.click();
  }

  protected onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;

    if (input.files && input.files.length > 0) {
      this.imageFile.set(input.files[0]);
      this.isCropping.set(true);
    }
  }

  protected onImageCropped({ blob }: ImageCroppedEvent) {
    this.croppedImage.set(blob);
  }

  protected confirmCrop() {
    this._modalRef.close(this.croppedImage());
  }
}
