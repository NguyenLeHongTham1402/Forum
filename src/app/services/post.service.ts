import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  private readonly baseURL: string = "https://localhost:44361/api/post/"
  constructor(private httpClient: HttpClient) { }

  public createPost(title: string | null, content: string | null, categoryId: number, username: string, post_tag: Object[]) {
    let user = localStorage.getItem("userInfo")
    const userInfo = JSON.parse(user!)

    const header = new HttpHeaders({ 'Authorization': `Bearer ${userInfo.token}` })

    const body = {
      Title: title,
      Content: content,
      CategoryId: categoryId,
      Username: username,
      PostTags: post_tag
    }
    return this.httpClient.post(this.baseURL + "create-post", body, { headers: header })
  }

  public deletePost(id: number) {
    let user = localStorage.getItem("userInfo")
    const userInfo = JSON.parse(user!)
    const header = new HttpHeaders({ 'Authorization': `Bearer ${userInfo.token}` })

    return this.httpClient.delete(this.baseURL + "delete-post/" + id, { headers: header })
  }

  public getPostById(id: number) {
    return this.httpClient.get(this.baseURL + "detail-post/" + id)
  }

  public getListPosts() {
    return this.httpClient.get(this.baseURL + "list-posts");
  }

  public getListPostsByKeyword(kw: string | null) {
    return this.httpClient.get(this.baseURL + "list-posts?kw=" + kw)
  }

  public getListByCategory(cateId: number) {
    return this.httpClient.get(this.baseURL + "posts-cate/" + cateId)
  }

  public updatePost(postId: number, title: string | null, content: string | null) {
    let user = localStorage.getItem("userInfo")
    const userInfo = JSON.parse(user!)
    const header = new HttpHeaders({ 'Authorization': `Bearer ${userInfo.token}` })
    const body = {
      Title: title,
      Content: content
    }
    return this.httpClient.patch(this.baseURL + "update-post/" + postId, body, { headers: header })
  }

  public updateViewPost(id: number) {
    const body = {
      id: id
    }
    return this.httpClient.patch(this.baseURL + "update-view/" + id, body)
  }

  public updateLikePost(id: number) {
    const body = {
      id: id
    }
    return this.httpClient.patch(this.baseURL + "update-like/" + id, body)
  }

  public getListPostByTagId(tagId: number) {
    return this.httpClient.get(this.baseURL + "posts-tag/" + tagId)
  }

  public getCategoryByPostId(id: number) {
    return this.httpClient.get(this.baseURL + "category/" + id)
  }

  public getPostsByUsename() {
    let user = localStorage.getItem("userInfo")
    const userInfo = JSON.parse(user!)
    const header = new HttpHeaders({ 'Authorization': `Bearer ${userInfo.token}` })

    return this.httpClient.get(this.baseURL + "posts-user/" + userInfo.username, { headers: header })
  }
}
