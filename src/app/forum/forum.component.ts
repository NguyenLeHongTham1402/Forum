import { Component, OnInit } from '@angular/core';
import { PostService } from '../services/post.service';
import { CategoryService } from '../services/category.service';
import { TagService } from '../services/tag.service';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-forum',
  templateUrl: './forum.component.html',
  styleUrls: ['./forum.component.css']
})
export class ForumComponent implements OnInit {

  listPosts: any = []
  listCates: any = []
  listTags: any = []
  searchForm = this.formBuilder.group({
    searchVal: ["", Validators.required]
  })

  constructor(private postSvc: PostService, private cateSvc: CategoryService, private tagSvc: TagService, private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.getListPost()
    this.getListCates()
    this.getListTag()
  }

  getListPost() {
    this.postSvc.getListPosts().subscribe(data => {
      this.listPosts = data
    })
  }

  getListCates() {
    this.cateSvc.getListCategories().subscribe(data => {
      this.listCates = data
    })
  }

  getListTag() {
    this.tagSvc.getListTags().subscribe(data => {
      this.listTags = data
    })
  }

  updateViewPost(postId: any) {
    this.postSvc.updateViewPost(postId).subscribe((data: any) => {
      console.log(data)
    })
  }

  updateLikePost(postId: any) {
    this.postSvc.updateLikePost(postId).subscribe((data: any) => {
      console.log(data)
      window.location.reload()
    })
  }

  onSearch() {
    let kw = this.searchForm.controls["searchVal"].value
    this.postSvc.getListPostsByKeyword(kw).subscribe(data => {
      this.listPosts = data
    })
  }

  onReload() {
    window.location.reload()
  }
}
