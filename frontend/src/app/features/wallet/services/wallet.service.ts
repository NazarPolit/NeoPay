import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { WalletDto } from '../models/wallet'; // Імпорт моделі
import { environment } from '../../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
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

  deposit(userId: string, amount: number, currency: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/${userId}/deposit`, { amount, currency });
  }
}