import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { CategoryService } from 'src/app/services/category.service';
import { PostService } from 'src/app/services/post.service';
import { TagService } from 'src/app/services/tag.service';

@Component({
  selector: 'app-create-post',
  templateUrl: './create-post.component.html',
  styleUrls: ['./create-post.component.css']
})
export class CreatePostComponent implements OnInit {

  listCategories: any = []
  listTags: any = []

  createPost = this.formBuilder.group({
    title: ["", Validators.required],
    content: ["", Validators.required],
    categoryId: [0, Validators.min],

  })
  cateId: string = ""
  tagIds: Array<number> = new Array()
  username: string = ""

  success: boolean = false
  message: string = ""
  showMessage: boolean = false

  constructor(private formBuilder: FormBuilder, private cateSvc: CategoryService, private tagSvc: TagService, private postSvc: PostService) { }

  ngOnInit(): void {
    this.getListCategories()
    this.getTags()
    this.username = JSON.parse(localStorage.getItem("userInfo")!).username
  }

  getListCategories() {
    this.cateSvc.getListCategories().subscribe(data => {
      this.listCategories = data
    })
  }

  getTags() {
    this.tagSvc.getListTags().subscribe(data => {
      this.listTags = data
    })
  }

  getSelectValue(event: any) {
    this.cateId = event.target.value
  }

  onCheckBoxChange(event: any) {
    if (event.target.checked) {
      this.tagIds.push(event.target.value)
    }
  }

  onSubmit() {
    let title = this.createPost.controls["title"].value
    let content = this.createPost.controls["content"].value
    let categoryId = this.cateId
    let post_tags:Array<Object> = []
    this.tagIds.forEach(element => {
      post_tags.push({
        TagId : Number(element.valueOf())
      })
    });

    this.postSvc.createPost(title, content, Number(categoryId), this.username, post_tags).subscribe((d: any) => {
      if (d.success) {
        this.success = true
      }
      this.message = d.message
      this.showMessage = true
    })

  }
}
