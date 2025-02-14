import { ChangeDetectionStrategy, Component, input, model, output, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgOptimizedImage } from '@angular/common';

@Component({
  selector: 'community-comment',
  standalone: true,
  imports: [CommonModule, NgOptimizedImage],
  template: `<div class="comment-container">
    <img
      class="image"
      [ngSrc]="comment().user.profilePictureUrl || defaultUserPhoto()"
      alt=""
      width="40"
      height="40"
    />
    <div class="u-w-100">
      <div class="comment">
        <span class="comment__userName">{{ comment().user.username }}</span>
        <p class="comment__content">{{ comment().content }}</p>
      </div>
      <div class="comment__actions u-w-100">
        <span>{{ comment().postedAt | date: 'dd MMM yyyy, HH:mm:ss' }}</span>
        @if(comment().user.username === loggedInUsername()){
        <button
          class="edit-btn"
          (click)="editMode().state ? cancelEditing() : editComment.emit(comment())"
        >
          {{ editMode().state ? 'Cancel' : 'Edit' }}
        </button>
        <button class="delete-btn" (click)="deleteComment.emit(comment().id)">Delete</button>
        }
      </div>
    </div>
  </div>`,
  styleUrl: './comment.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CommentComponent {
  comment = input.required<any>();
  editComment = output<any>();
  deleteComment = output<string>();
  loggedInUsername = input.required<string>();
  editMode = model.required<any>();
  cancelEditMode = output();

  defaultUserPhoto = signal('global/assets/default-user-photo.png');

  protected cancelEditing() {
    this.editMode.update(({ state, ...data }) => ({
      state: !state,
      ...data,
    }));

    this.cancelEditMode.emit();
  }
}
