import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly baseURL: string = "https://localhost:44361/api/user/";
  constructor(private httpClient: HttpClient) { }

  public signin(username: string | null, pass: string | null) {
    const body = {
      Username: username,
      Password: pass
    }
    return this.httpClient.post(this.baseURL + "signin", body)
  }

  public signup(username: string | null, pass: string | null, email: string | null, realname: string | null) {
    const body = {
      Username: username,
      Password: pass,
      Email: email,
      RealName: realname,
    }
    return this.httpClient.post(this.baseURL+"signup", body)
  }


}
