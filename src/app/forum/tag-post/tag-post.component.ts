import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PostService } from 'src/app/services/post.service';
import { TagService } from 'src/app/services/tag.service';

@Component({
  selector: 'app-tag-post',
  templateUrl: './tag-post.component.html',
  styleUrls: ['./tag-post.component.css']
})
export class TagPostComponent implements OnInit {

  tagId: number = 0
  private sub: any
  listPostByTag: any = []
  tag: any

  constructor(private route: ActivatedRoute, private postSvc: PostService, private tagSvc: TagService, private router: Router) { }

  ngOnInit(): void {
    this.sub = this.route.params.subscribe(param => {
      this.tagId = Number(param["tagId"])
      this.getPostByTag(this.tagId)
      this.getTag(this.tagId)
    })
  }

  getPostByTag(tagId: any) {
    this.postSvc.getListPostByTagId(tagId).subscribe(data => {
      this.listPostByTag = data
    })
  }

  getTag(tagId: any) {
    this.tagSvc.getTagById(tagId).subscribe(data => {
      this.tag = data
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

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

}
