import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { firstValueFrom, lastValueFrom } from 'rxjs';
import { JsonResponse } from './Models';

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

  async getAdminOnly(): Promise<JsonResponse> {
    try {
      const response = await firstValueFrom(this.http.get<JsonResponse>("https://localhost:7054/MyAPI/admin-only"));
      return response;
    }
    catch (error) {
      throw error;
    }
  }

  async getAdminOrManager(): Promise<JsonResponse> {
    try {
      const response = await firstValueFrom(this.http.get<JsonResponse>("https://localhost:7054/MyAPI/manager-or-admin"));
      return response;
    }
    catch (error) {
      throw error;
    }
  }

  async getAuthenticatedUser(): Promise<JsonResponse> {
    try {
      const response = await firstValueFrom(this.http.get<JsonResponse>("https://localhost:7054/MyAPI/authenticated-user"));
      return response;
    }
    catch (error) {
      throw error;
    }
  }

  async getMinimumAgeUser(): Promise<JsonResponse> {
    try {
      const response = await firstValueFrom(this.http.get<JsonResponse>("https://localhost:7054/MyAPI/minimum-age-user"));
      return response;
    }
    catch (error) {
      throw error;
    }
  }
}
