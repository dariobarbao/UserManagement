import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { environment } from '../../../environments/environment';

const TOKEN_KEY = 'auth_token';
const EMAIL_KEY = 'user_email';

export interface LoginResponse {
  token: string;
}

@Injectable({ providedIn: 'root' })
export class AuthService {

  constructor(private http: HttpClient) {}

  login(email: string, password: string): Observable<LoginResponse> {
    return this.http
      .post<LoginResponse>(`${environment.apiUrl}/api/auth/login`, {
        email,
        password
      })
      .pipe(
        tap(res => {
          this.setToken(res.token);
          this.setEmail(email);
        })
      );
  }

  get token(): string | null {
    return localStorage.getItem(TOKEN_KEY);
  }

  get email(): string | null {
    return localStorage.getItem(EMAIL_KEY);
  }

  setToken(token: string): void {
    localStorage.setItem(TOKEN_KEY, token);
  }

  setEmail(email: string): void {
    localStorage.setItem(EMAIL_KEY, email);
  }

  clear(): void {
    localStorage.removeItem(TOKEN_KEY);
    localStorage.removeItem(EMAIL_KEY);
  }

  isAuthenticated(): boolean {
    return !!this.token;
  }

  logout(): void {
    this.clear();
  }
}
