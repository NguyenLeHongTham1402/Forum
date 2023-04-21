import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TagService {

  private readonly baseURL: string = "https://localhost:44361/api/tag/"
  constructor(private httpClient: HttpClient) { }

  public createTag(name: string | null) {
    let user = localStorage.getItem("userInfo");
    const userInfo = JSON.parse(user!);
    const header = new HttpHeaders({ 'Authorization': `Bearer ${userInfo.token}` })

    const body = {
      Name: name
    }
    return this.httpClient.post(this.baseURL + "create-tag", body, { headers: header })
  }

  public updateTag(id: number, name: string | null) {
    let user = localStorage.getItem("userInfo");
    const userInfo = JSON.parse(user!);
    const header = new HttpHeaders({ 'Authorization': `Bearer ${userInfo.token}` })

    const body = {
      Id: id,
      Name: name
    }

    return this.httpClient.patch(this.baseURL + "update-tag/" + id, body, { headers: header })
  }

  public getTagById(id: number) {
    return this.httpClient.get(this.baseURL + "get-tag/" + id)
  }

  public getListTags() {
    return this.httpClient.get(this.baseURL + "list-tags")
  }

  public getListTagsByPostId(id: number) {
    return this.httpClient.get(this.baseURL + "post/list-tags/" + id)
  }
}
