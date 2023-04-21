import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  private readonly baseURL: string = "https://localhost:44361/api/category/"
  constructor(private httpClient: HttpClient) { }

  public createCategory(name: string | null) {
    let user = localStorage.getItem("userInfo");
    let userInfo = JSON.parse(user!);
    const header = new HttpHeaders({ 'Authorization': `Bearer ${userInfo.token}` })
    const body = {
      Name: name
    }
    return this.httpClient.post(this.baseURL + "create-category", body, { headers: header })
  }

  public updateCategory(name: string | null, id: number) {
    let user = localStorage.getItem("userInfo");
    let userInfo = JSON.parse(user!);
    const header = new HttpHeaders({ 'Authorization': `Bearer ${userInfo.token}` })
    const body = {
      Id: id,
      Name: name
    }
    return this.httpClient.patch(this.baseURL + "update-category/" + id, body, { headers: header });
  }

  public deleteCategory(id: number) {
    let user = localStorage.getItem("userInfo");
    let userInfo = JSON.parse(user!);
    const header = new HttpHeaders({ 'Authorization': `Bearer ${userInfo.token}` })
    return this.httpClient.delete(this.baseURL + "delete-category/" + id, { headers: header });
  }

  public getListCategories() {
    return this.httpClient.get(this.baseURL + "list-categories");
  }

  public getCategory(id: number){
    return this.httpClient.get(this.baseURL + "categories/" + id)
  }
}
