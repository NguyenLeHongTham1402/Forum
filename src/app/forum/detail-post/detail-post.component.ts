import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CommentService } from 'src/app/services/comment.service';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-detail-post',
  templateUrl: './detail-post.component.html',
  styleUrls: ['./detail-post.component.css']
})
export class DetailPostComponent implements OnInit {

  postId: number = 0
  private sub: any
  postDtl: any
  comments: any = []

  cmtForm = this.formBuilder.group({
    cmtTitle: ["", Validators.required],
    cmtContent: ["", Validators.required]
  })

  repCmtForm = this.formBuilder.group({
    repTitle: ["", Validators.required],
    repContent: ["", Validators.required],
    repParentId: ["", Validators.required]
  })

  constructor(private route: ActivatedRoute, private postSvc: PostService,
    private commentSvc: CommentService, private formBuilder: FormBuilder,
    private router: Router) { }

  ngOnInit(): void {
    this.sub = this.route.params.subscribe(params => {
      this.postId = Number(params["postId"])
      this.getPostDetail(this.postId)
      if (this.isUserlogin) {
        this.getCommentByPostId(this.postId)
      }

    })
  }

  get isUserlogin() {
    const user = localStorage.getItem("userInfo");
    return user && user.length > 0;
  }

  getPostDetail(id: any) {
    this.postSvc.getPostById(id).subscribe(data => {
      this.postDtl = data
    })
  }

  getCommentByPostId(id: any) {
    this.commentSvc.commentByPostId(id).subscribe(data => {
      this.comments = data
      //console.info(data)
    })
  }

  onPostCmt() {
    let title = this.cmtForm.controls["cmtTitle"].value
    let content = this.cmtForm.controls["cmtContent"].value
    this.commentSvc.createComment(title, content, this.postId).subscribe((data: any) => {
      alert(data.message)
      window.location.reload()
    })
  }

  transmitCmtId(id: any) {
    this.repCmtForm.controls["repParentId"].setValue(id)
  }

  repCmtPost() {
    let title = this.repCmtForm.controls["repTitle"].value
    let content = this.repCmtForm.controls["repContent"].value
    let parentId = Number(this.repCmtForm.controls["repParentId"].value)
    this.commentSvc.createReply(title, content, this.postId, parentId).subscribe((data: any) => {
      alert(data.message)
      window.location.reload()
    })
  }

  updateLikePost(postId: any) {
    this.postSvc.updateLikePost(postId).subscribe((data: any) => {
      console.log(data)
      window.location.reload()
    })
  }

  updateLikeComment(cmtId: any) {
    this.commentSvc.updateLikeComment(cmtId).subscribe((data: any) => {
      console.log(data)
      window.location.reload()
    })
  }

  updateLikeReply(repId: any) {
    this.commentSvc.updateLikeReply(repId).subscribe((data: any) => {
      console.log(data)
      window.location.reload()
    })
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }
}
