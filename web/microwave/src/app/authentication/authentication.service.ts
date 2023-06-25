import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {RegisterRequest} from "./models/register-request";
import {LoginRequest} from "./models/login-request";
import {LoginResponse} from "./models/login-response";

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  public isAuthenticated = false;

  constructor(private httpClient: HttpClient) {
  }

  register(registerRequest: RegisterRequest) {
    return this.httpClient.post<RegisterRequest>(
      'api/v1/authentication/register',
      registerRequest);
  }

  login(loginRequest: LoginRequest) {
    return this.httpClient.post<LoginResponse>(
      'api/v1/authentication/login',
      loginRequest);
  }
}
