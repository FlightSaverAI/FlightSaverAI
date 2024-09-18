import { Injectable } from '@angular/core';
import { LoginFormData } from '../models/login-form-data.interface';
import { of, throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  login(loginData: LoginFormData) {
    if (loginData.email === 'error@example.com') {
      const errorResponse = new HttpErrorResponse({
        status: 401,
        statusText: 'Unauthorized',
        error: {
          message: 'Invalid email or password',
        },
      });

      return throwError(() => errorResponse);
    }

    return of({
      id: 12345,
      username: 'John Doe',
      email: 'john.doe@example.com',
      token: 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9',
      userRole: 'admin',
    });
  }
}
