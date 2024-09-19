import { Injectable } from '@angular/core';
import { of, throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  authentication<T>(formData: T) {
    if (!formData) {
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
