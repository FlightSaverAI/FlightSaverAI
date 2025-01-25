import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AvatarComponent } from '@flight-saver/user-profile/ui';

@Component({
  selector: 'lib-settings',
  standalone: true,
  imports: [CommonModule, AvatarComponent],
  template: `<user-profile-avatar [isSettingsSection]="true"></user-profile-avatar>`,
  styleUrl: './settings.component.scss',
})
export class SettingsComponent {}
