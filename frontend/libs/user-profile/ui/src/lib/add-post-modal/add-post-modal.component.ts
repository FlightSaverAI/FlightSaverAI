import { Component, inject, signal } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ModalComponent } from '@shared/ui-components';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { createPostForm } from '@flight-saver/user-profile/utils';
import { ButtonComponent } from '@shared/ui-components';
import { NgOptimizedImage } from '@angular/common';
import { SelectComponent } from '@shared/ui-components';
import { TextareaComponent } from '@shared/ui-components';
import { InputComponent } from '@shared/ui-components';

@Component({
  standalone: true,
  imports: [
    CommonModule,
    ModalComponent,
    ReactiveFormsModule,
    ButtonComponent,
    NgOptimizedImage,
    SelectComponent,
    TextareaComponent,
    InputComponent,
  ],
  template: `<shared-modal [title]="title()" (confirmEvent)="confirmEvent()">
    <form [formGroup]="createPostForm()">
      <div
        class="paste-photo-container"
        [ngClass]="isDragging() ? 'dragging' : ''"
        (dragover)="onDragOver($event)"
        (dragleave)="onDragLeave($event)"
        (drop)="onDrop($event)"
      >
        @if(!imageUrl){
        <div class="drag-and-drop">
          <img [ngSrc]="'global/assets/upload.svg'" width="60" height="60" alt="" />
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
        } @else {
        <div class="image-preview">
          <img [src]="imageUrl" alt="Selected Image" />
        </div>
        }
      </div>
      <div class="form-area">
        <shared-select
          label="Country"
          placeholder="Select Country"
          formControlName="Country"
          fieldName="Country"
          [options]="modalData.countries"
          [parentForm]="createPostForm()"
        ></shared-select>
        <shared-input
          label="City"
          placeholder="Select City"
          formControlName="City"
          fieldName="City"
          [parentForm]="createPostForm()"
        ></shared-input>
        <shared-textarea
          label="Description"
          formControlName="Content"
          fieldName="Content"
          placeholder="Share your opinion..."
          [parentForm]="createPostForm()"
        >
        </shared-textarea>
      </div>
    </form>
  </shared-modal>`,
  styleUrl: './add-post-modal.component.scss',
})
export class AddPostModalComponent {
  private _modalRef = inject(MatDialogRef);

  protected modalData = inject(MAT_DIALOG_DATA);
  protected title = signal('Add Post');
  protected createPostForm = signal(createPostForm());
  protected imageUrl: any = null;
  protected isDragging = signal(false);

  protected triggerFileInput(fileInput: HTMLInputElement) {
    fileInput.click();
  }

  protected onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;

    if (input.files && input.files[0]) {
      const file = input.files[0];
      this._readFile(file);
    }
  }

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
      const file = event.dataTransfer.files[0];
      this._readFile(file);
    }
  }

  private _readFile(file: File) {
    const reader = new FileReader();

    reader.onload = (e) => {
      this.imageUrl = reader.result;
    };
    reader.readAsDataURL(file);
  }

  protected confirmEvent() {
    if (this.createPostForm().invalid) {
      alert('Incorrect form');
      return;
    }

    if (this.imageUrl) {
      const blob = this._dataURLtoBlob(this.imageUrl);
      const file = new File([blob], 'uploaded-image.png', { type: blob.type });

      const formData = new FormData();
      formData.append('Post.Image', file, 'image.png');

      this.createPostForm().controls.Image.setValue(formData);
    }

    this._modalRef.close(this.createPostForm().getRawValue());
  }

  private _dataURLtoBlob(dataURL: string): Blob {
    const arr = dataURL.split(',');
    const mimeMatch = arr[0].match(/:(.*?);/);
    const mime = mimeMatch ? mimeMatch[1] : 'image/png';
    const byteString = atob(arr[1]);
    const arrayBuffer = new ArrayBuffer(byteString.length);
    const uint8Array = new Uint8Array(arrayBuffer);

    for (let i = 0; i < byteString.length; i++) {
      uint8Array[i] = byteString.charCodeAt(i);
    }

    return new Blob([arrayBuffer], { type: mime });
  }
}
