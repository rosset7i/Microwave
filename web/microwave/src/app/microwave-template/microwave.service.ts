import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {MicrowaveOutput} from "./models/microwave-output";

@Injectable({
  providedIn: 'root'
})
export class MicrowaveService {
  basePath = 'api/v1/microwaves';
  token = localStorage.getItem('token');
  headers = new HttpHeaders().set('Authorization', `Bearer ${this.token}`);

  constructor(private httpClient: HttpClient) {
  }

  getAll(){
    return this.httpClient.get<MicrowaveOutput[]>(this.basePath, {headers: this.headers});
  }

  createMicrowave(microwave: MicrowaveOutput) {
    return this.httpClient.post(this.basePath, microwave);
  }

  updateMicrowave(idMicrowave: string, microwave: MicrowaveOutput) {
    return this.httpClient.put(`${this.basePath}/${idMicrowave}`, microwave, {headers: this.headers});
  }

  deleteMicrowave(idMicrowave: string) {
    return this.httpClient.delete(`${this.basePath}/${idMicrowave}`, {headers: this.headers});
  }
}
