import { Component, OnInit, inject, signal } from '@angular/core';
import { WalletService } from '../../services/wallet.service';
import { WalletDto } from '../../models/wallet';
import { SpinnerComponent } from '../../../../shared/ui/spinner/spinner';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-wallet-dashboard',
  standalone: true,
  imports: [SpinnerComponent, FormsModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class DashboardComponent implements OnInit {
  wallet = signal<WalletDto | null>(null);
  isLoading = signal(true);
  errorMessage = signal('');

  depositAmount = signal<number | null>(null);
  isProcessing = signal(false);

  private walletService = inject(WalletService);
  private testUserId = '11111111-2222-3333-4444-555555555555';

  ngOnInit(): void {
    this.loadWallet();
  }

  loadWallet(): void {
    this.isLoading.set(true);
    this.errorMessage.set('');

    this.walletService.getWallet(this.testUserId).subscribe({
      next: (data) => {
        this.wallet.set(data);
        this.isLoading.set(false);
      },
      error: (err) => {
        if (err.status === 404) {
          this.createNewWallet();
        } else {
          this.errorMessage.set('Connection error. Please check backend.');
          this.isLoading.set(false);
        }
      }
    });
  }

  createNewWallet(): void {
    this.walletService.createWallet(this.testUserId, 'USD').subscribe({
      next: () => this.loadWallet(),
      error: () => {
        this.errorMessage.set('Failed to create wallet.');
        this.isLoading.set(false);
      }
    });
  }

onDeposit(): void {
    const amount = this.depositAmount();
    const currentWallet = this.wallet();

    if (!amount || amount <= 0 || !currentWallet) return;

    this.isProcessing.set(true);

    this.walletService.deposit(this.testUserId, amount, currentWallet.currency).subscribe({
      next: () => {
        this.loadWallet();
        this.depositAmount.set(null);
        this.isProcessing.set(false);
      },
      error: () => {
        this.errorMessage.set('Deposit failed. Please try again.');
        this.isProcessing.set(false);
      }
    });
  }
}