import { Component, OnInit } from '@angular/core';
import { PostService } from '../services/post.service';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-mypost',
  templateUrl: './mypost.component.html',
  styleUrls: ['./mypost.component.css']
})
export class MypostComponent implements OnInit {

  listPosts:any=[]
  detailPost: any

  editPost = this.formBuilder.group({
    id: ["", Validators.required],
    title: ["", Validators.required],
    content: ["", Validators.required]
  })
  upd_success: boolean = false
  upd_message: string = ""
  upd_showMessage: boolean = false
  constructor(private postSvc:PostService, private formBuilder:FormBuilder) { }

  ngOnInit(): void {
    this.getListPostByUsername()
  }

  getListPostByUsername(){
    this.postSvc.getPostsByUsename().subscribe(data=>{
      this.listPosts=data
    })
  }

  getPostDetail(id: any) {
    this.postSvc.getPostById(id).subscribe(data => {
      this.detailPost = data
      console.log(this.detailPost)
    })
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
