import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgOptimizedImage } from '@angular/common';

@Component({
  selector: 'friends-no-user-found',
  standalone: true,
  imports: [CommonModule, NgOptimizedImage],
  template: `<div class="no-user-found">
    <img
      ngSrc="global/assets/assets-community/user-not-found.svg"
      width="100"
      height="100"
      alt="No user found"
    />
    <p>No friends found</p>
  </div>`,
  styleUrl: './no-user-found.component.scss',
})
export class NoUserFoundComponent {}
