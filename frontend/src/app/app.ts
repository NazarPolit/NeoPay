import { Component, OnInit, inject, PLATFORM_ID, signal } from '@angular/core';
import { WalletService, WalletDto } from './services/wallet'; 
import { isPlatformBrowser } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [], 
  templateUrl: './app.html', 
  styleUrl: './app.scss'
})
export class AppComponent implements OnInit {
  wallet = signal<WalletDto | null>(null);
  isLoading = signal(true);
  errorMessage = signal('');

  private walletService = inject(WalletService);
  private platformId = inject(PLATFORM_ID);
  private testUserId = '11111111-2222-3333-4444-555555555555';

  ngOnInit(): void {
    if (isPlatformBrowser(this.platformId)) {
      this.loadWallet();
    } else {
      this.isLoading.set(true);
    }
  }

  loadWallet(): void {
    this.isLoading.set(true);
    this.errorMessage.set('');

    this.walletService.getWallet(this.testUserId).subscribe({
      next: (data) => {
        console.log('Data received from backend:', data);
        this.wallet.set(data);
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error('HTTP request error:', err);
        if (err.status === 404) {
          this.createNewWallet();
        } else {
          this.errorMessage.set('Connection error. Please check if the backend is running.');
          this.isLoading.set(false);
        }
      }
    });
  }

  createNewWallet(): void {
    console.log('Wallet not found, creating a new one...');
    this.walletService.createWallet(this.testUserId, 'USD').subscribe({
      next: () => {
        console.log('Wallet created successfully, reloading data...');
        this.loadWallet();
      },
      error: (err) => {
        console.error('Creation error:', err);
        this.errorMessage.set('Failed to create wallet.');
        this.isLoading.set(false);
      }
    });
  }
}