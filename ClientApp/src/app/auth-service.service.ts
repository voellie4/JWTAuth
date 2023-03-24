import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthServiceService {
  token: string = "";
  message: string = "";
  constructor(private http: HttpClient) { }

  getToken(): Promise<string> {
    return new Promise<string>((resolve, reject) => {
      var params = new HttpParams().set("username", "minimumAgeUser").set("password", "password3");
      this.http.get("https://localhost:7054/token", { params }).subscribe({
        next: (res: any) => {
          resolve(res.token);
        },
        error: (err:any) => {
          reject(err);
        }
      })
    });
  }

  getAdminOnly(): Promise<string> {
    return new Promise<string>((resolve, reject) => {
      this.http.get("https://localhost:7054/MyAPI/admin-only").subscribe({
        next: (res: any) => {
          resolve(res.displayedMessage);
        },
        error: (err: any) => {
          reject(err);
        }
      })
    });
  }

  async getAdminOrManager() {
    return new Promise<string>((resolve, reject) => {
      this.http.get("https://localhost:7054/MyAPI/manager-or-admin").subscribe({
        next: (res: any) => {
          resolve(res.displayedMessage);
        },
        error: (err: any) => {
          reject(err);
        }
      })
    });
  }

  async getAuthenticatedUser() {
    return new Promise<string>((resolve, reject) => {
      this.http.get("https://localhost:7054/MyAPI/authenticated-user").subscribe({
        next: (res: any) => {
          resolve(res.displayedMessage);
        },
        error: (err: any) => {
          reject(err);
        }
      })
    });
  }

  getMinimumAgeUser() {
    return new Promise<string>((resolve, reject) => {
      this.http.get("https://localhost:7054/MyAPI/minimum-age-user").subscribe({
        next: (res: any) => {
          resolve(res.displayedMessage);
        },
        error: (err: any) => {
          reject(err);
        }
      })
    });
  }
}
