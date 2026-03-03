import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { environment } from '../../../environments/environment';
import { User } from '../models/user.model';
import { UserRequest } from '../models/user-request.model';
import { UserResponse, UsersResponse } from '../models/users-response.model';


@Injectable({ providedIn: 'root' })
export class UsersService {
  private readonly baseUrl = `${environment.apiUrl}/api/users`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<User[]> {
    return this.http.get<UsersResponse>(this.baseUrl).pipe(
      map(response => response.users)
    );
  }

  getById(id: string): Observable<User> {
  return this.http
    .get<UserResponse>(`${this.baseUrl}/${id}`)
    .pipe(
      map(response => ({
        ...response.user,
        dateOfBirth: this.normalizeDate(response.user.dateOfBirth)
      }))
    );
}

  create(user: UserRequest): Observable<void> {
    return this.http.post<void>(this.baseUrl, user);
  }

  update(id: string, user: any): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, user);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

  private normalizeDate(date?: string | null): string | null {
    if (!date || date.startsWith('0001-01-01')) {
      return null;
    }
    return date;
  }
}
