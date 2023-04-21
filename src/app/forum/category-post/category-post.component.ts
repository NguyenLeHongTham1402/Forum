import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryService } from 'src/app/services/category.service';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-category-post',
  templateUrl: './category-post.component.html',
  styleUrls: ['./category-post.component.css']
})
export class CategoryPostComponent implements OnInit {

  CateId: number = 0
  private sub: any
  listPostByCate: any = []
  category: any


  constructor(private postSvc: PostService, private route: ActivatedRoute, private cateSvc: CategoryService, private router: Router) { }

  ngOnInit(): void {
    this.sub = this.route.params.subscribe(param => {
      this.CateId = Number(param["cateId"])
      this.getPostByCate(this.CateId)
      this.getCategory(this.CateId)
    })
  }

  getPostByCate(cateId: any) {
    this.postSvc.getListByCategory(cateId).subscribe(data => {
      this.listPostByCate = data
    })
  }

  getCategory(cateId: any) {
    this.cateSvc.getCategory(cateId).subscribe(data => {
      this.category = data
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
