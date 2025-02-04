import { Component, inject, OnInit, signal } from '@angular/core';
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
  template: `<shared-modal [title]="modalData.title" (confirmEvent)="confirmEvent()">
    <form [formGroup]="createPostForm()">
      <div
        class="paste-photo-container"
        [ngClass]="{
          dragging: isDragging(),
        }"
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
        <img
          (click)="this.imageUrl = null"
          class="cancel-image-icon"
          ngSrc="global/assets/cancel.svg"
          width="20"
          height="20"
          alt=""
        />
        <div class="shadow"></div>
        <div class="image-preview-container">
          <img class="image-preview" [src]="imageUrl" alt="Selected Image" />
        </div>
        }
      </div>
      <div class="form-area">
        <shared-select
          label="Country"
          placeholder="Select Country"
          formControlName="country"
          fieldName="country"
          [options]="modalData.countries"
          [parentForm]="createPostForm()"
        ></shared-select>
        <shared-input
          label="City"
          placeholder="Select City"
          formControlName="city"
          fieldName="city"
          [parentForm]="createPostForm()"
        ></shared-input>
        <shared-textarea
          label="Description"
          formControlName="content"
          fieldName="content"
          placeholder="Share your opinion..."
          [parentForm]="createPostForm()"
        >
        </shared-textarea>
      </div>
    </form>
  </shared-modal>`,
  styleUrl: './manage-post-modal.component.scss',
})
export class ManagePostModalComponent implements OnInit {
  private _modalRef = inject(MatDialogRef);

  protected modalData = inject(MAT_DIALOG_DATA);
  protected createPostForm = signal(createPostForm());
  protected imageUrl: any = null;
  protected isDragging = signal(false);

  public ngOnInit(): void {
    if (this.modalData?.post) {
      this.imageUrl = this.modalData.post.imageUrl;

      const location = this._divideLocation(this.modalData.post.location);

      this.createPostForm().patchValue({
        country: location.country,
        city: location.city,
        content: this.modalData.post.content,
      });
    }
  }

  private _divideLocation(location: string) {
    if (!location) {
      return {
        country: '',
        city: '',
      };
    }

    const updateLocation = location.split(',');

    return {
      country: updateLocation?.[0],
      city: updateLocation?.[1],
    };
  }

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

    if (!this.imageUrl.includes('flightsaverblobstorage')) {
      this.createPostForm().controls.image.setValue(this.imageUrl);
    }

    this._modalRef.close(this.createPostForm().getRawValue());
  }
}
