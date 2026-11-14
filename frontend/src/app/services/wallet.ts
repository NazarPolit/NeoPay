import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

export interface WalletDto {
  availableBalance: number;
  heldAmount: number;
  currency: string;
}

@Injectable({
  providedIn: 'root',
})
export class WalletService {
  private http = inject(HttpClient);

  private apiUrl = `${environment.apiUrl}/Wallet`; 

  createWallet(userId: string, currency: string): Observable<{ walletId: string }> {
    return this.http.post<{ walletId: string }>(this.apiUrl, { userId, currency });
  }

  getWallet(userId: string): Observable<WalletDto> {
    return this.http.get<WalletDto>(`${this.apiUrl}/${userId}`);
  }
}
