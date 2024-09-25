import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-authorized-user',
  standalone: true,
  imports: [RouterModule],
  template: `
    <nav class="nav">
      <div class="logo">
        <img src="" alt="" />
        <span>Flight Saver</span>
      </div>
      <div class="links">
        <a routerLink="/authorized/home">Home</a>
        <a>Statistics</a>
        <a routerLink="/authorized/community">Community</a>
        <button>Add Flight</button>
        <div class="photo"></div>
      </div>
    </nav>
    <main>
      <router-outlet></router-outlet>
    </main>
  `,
  styleUrl: './authorized-user.component.scss',
})
export class AuthorizedUserComponent {}
