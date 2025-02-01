import { ChangeDetectionStrategy, Component, input, output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InputComponent } from '@shared/ui-components';
import { ReactiveFormsModule } from '@angular/forms';
import { ButtonComponent } from '@shared/ui-components';

@Component({
  selector: 'user-profile-edit',
  standalone: true,
  imports: [CommonModule, InputComponent, ReactiveFormsModule, ButtonComponent],
  template: `
    <div class="container">
      <h2 class="title">User Data</h2>
      <form class="form" [formGroup]="userProfileForm()">
        <shared-input
          [parentForm]="userProfileForm()"
          [isDisabled]="true"
          formControlName="username"
          placeholder="User Name"
          label="User Name"
          fieldName="username"
        ></shared-input>
        <shared-input
          [parentForm]="userProfileForm()"
          [isDisabled]="true"
          formControlName="email"
          fieldName="email"
          placeholder="Enter"
          label="Email"
        ></shared-input>
      </form>
      <h2 class="title">Change Password</h2>
      <form class="form" [formGroup]="changePasswordForm()">
        <shared-input
          [parentForm]="changePasswordForm()"
          formControlName="password"
          placeholder="Enter password"
          label="Password"
          type="password"
          fieldName="password"
        ></shared-input>
        <shared-input
          [parentForm]="changePasswordForm()"
          formControlName="confirmPassword"
          placeholder="Confirm password"
          label="Confirm Password"
          type="password"
          fieldName="confirmPassword"
        ></shared-input>
      </form>
      <shared-button
        content="Save"
        [ngClass]="{ disabled: !changePasswordForm().dirty }"
        (emitEvent)="save.emit()"
      ></shared-button>
    </div>
  `,
  styleUrl: './user-profile-edit.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class UserProfileEditComponent {
  userData = input.required<any>();
  userProfileForm = input.required<any>();
  changePasswordForm = input.required<any>();

  save = output<void>();
}
