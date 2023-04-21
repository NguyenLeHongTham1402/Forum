import { Component, OnInit } from '@angular/core';
import { PostService } from '../services/post.service';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-post-management',
  templateUrl: './post-management.component.html',
  styleUrls: ['./post-management.component.css']
})
export class PostManagementComponent implements OnInit {

  public listPosts: any = []
  detailPost: any

  success: boolean = false
  message: string = ""
  showMessage: boolean = false

  editPost = this.formBuider.group({
    id: ["", Validators.required],
    title: ["", Validators.required],
    content: ["", Validators.required]
  })
  upd_success: boolean = false
  upd_message: string = ""
  upd_showMessage: boolean = false
  constructor(private postSvc: PostService, private formBuider: FormBuilder) { }

  ngOnInit(): void {
    this.getListPosts()
  }

  getListPosts() {
    this.postSvc.getListPosts().subscribe(data => {
      this.listPosts = data
    })
  }


  getPostDetail(id: any) {
    this.postSvc.getPostById(id).subscribe(data => {
      this.detailPost = data
    })
  }

  delPost(id: any) {
    if (confirm("Do you want delete it")) {
      this.postSvc.deletePost(id).subscribe((d: any) => {
        if (d.success) {
          this.success = true
        }
        this.message = d.message
        this.showMessage = true
      })
    }
  }

  onSubmit() {
    let id = Number(this.editPost.controls["id"].value)
    let title = this.editPost.controls["title"].value
    let content = this.editPost.controls["content"].value
    this.postSvc.updatePost(id, title, content).subscribe((d: any) => {
      if (d.success) {
        this.upd_success = true
      }
      this.upd_message = d.message
      this.upd_showMessage = true
    })
  }

  onRefresh(){
    window.location.reload()
  }
}
