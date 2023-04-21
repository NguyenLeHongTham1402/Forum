import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CommentService {

  private readonly baseURL: string = "https://localhost:44361/api/comment/"
  constructor(private httpClient: HttpClient) { }

  public createComment(title: string | null, content: string | null, postId: number) {
    let user = localStorage.getItem("userInfo");
    let userInfo = JSON.parse(user!);
    const header = new HttpHeaders({ 'Authorization': `Bearer ${userInfo.token}` })

    const body = {
      Title: title,
      Content: content,
      Username: userInfo.username,
      PostId: postId
    }

    return this.httpClient.post(this.baseURL + "create-comment", body, { headers: header })
  }

  public deleteComment(id: number) {
    let user = localStorage.getItem("userInfo");
    let userInfo = JSON.parse(user!);
    const header = new HttpHeaders({ 'Authorization': `Bearer ${userInfo.token}` })

    return this.httpClient.delete(this.baseURL + "delete-comment/" + id, { headers: header })
  }

  public updateLikeComment(id: number) {
    let user = localStorage.getItem("userInfo");
    let userInfo = JSON.parse(user!);
    const header = new HttpHeaders({ 'Authorization': `Bearer ${userInfo.token}` })

    const body = {
      id: id
    }
    return this.httpClient.patch(this.baseURL + "like-comment/" + id, body, { headers: header })
  }

  public createReply(title: string | null, content: string | null, postId: number, parentId: number) {
    let user = localStorage.getItem("userInfo");
    let userInfo = JSON.parse(user!);
    const header = new HttpHeaders({ 'Authorization': `Bearer ${userInfo.token}` })

    const body = {
      Title: title,
      Content: content,
      Username: userInfo.username,
      PostId: postId,
      ParentId: parentId
    }

    return this.httpClient.post(this.baseURL + "create-reply", body, { headers: header })
  }

  public deleteReply(id: number) {
    let user = localStorage.getItem("userInfo");
    let userInfo = JSON.parse(user!);
    const header = new HttpHeaders({ 'Authorization': `Bearer ${userInfo.token}` })

    return this.httpClient.delete(this.baseURL + "delete-reply/" + id, { headers: header })
  }

  public updateLikeReply(id: number) {
    let user = localStorage.getItem("userInfo");
    let userInfo = JSON.parse(user!);
    const header = new HttpHeaders({ 'Authorization': `Bearer ${userInfo.token}` })

    const body = {
      id: id
    }
    return this.httpClient.patch(this.baseURL + "like-reply/" + id, body, { headers: header })
  }

  public commentByPostId(id: number) {
    let user = localStorage.getItem("userInfo");
    let userInfo = JSON.parse(user!);
    const header = new HttpHeaders({ 'Authorization': `Bearer ${userInfo.token}` })

    return this.httpClient.get(this.baseURL + "comments/" + id, { headers: header })
  }

  public replyByParentId(parentId: number) {
    return this.httpClient.get(this.baseURL + "replies/" + parentId)
  }
}
