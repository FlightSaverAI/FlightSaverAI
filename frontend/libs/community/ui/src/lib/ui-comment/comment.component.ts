import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgOptimizedImage } from '@angular/common';

@Component({
  selector: 'community-comment',
  standalone: true,
  imports: [CommonModule, NgOptimizedImage],
  template: `<div class="comment-container">
    <img class="image" [ngSrc]="comment().photo" alt="" width="40" height="40" />
    <div class="u-w-100">
      <div class="comment">
        <span class="comment__userName">{{ comment().userName }}</span>
        <p class="comment__content">{{ comment().comment }}</p>
      </div>
      <div class="comment__actions u-w-100">
        <span>{{ comment().time }}</span>
        <span>Like</span>
        <span>Reply</span>
      </div>
    </div>
  </div>`,
  styleUrl: './comment.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CommentComponent {
  comment = input.required<any>();
}
