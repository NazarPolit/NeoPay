import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './layout/header/header';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent],
  template: `
    <div class="app-container">
      <app-header></app-header>
      
      <router-outlet></router-outlet> 
    </div>
  `,
  styles: [`
    .app-container {
      font-family: 'Segoe UI', sans-serif;
      background-color: #f4f7f6;
      min-height: 100vh;
      display: flex;
      flex-direction: column;
      align-items: center;
      padding: 2rem;
      color: #333;
    }
  `]
})
export class AppComponent {}