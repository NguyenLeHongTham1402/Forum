import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserSigninComponent } from './user-signin/user-signin.component'
import { UserSignupComponent } from './user-signup/user-signup.component'
import { CategoryManagementComponent } from './category-management/category-management.component';
import { CreateCateComponent } from './category-management/create-cate/create-cate.component';
import { TagManagementComponent } from './tag-management/tag-management.component';
import { CreateTagComponent } from './tag-management/create-tag/create-tag.component';
import { PostManagementComponent } from './post-management/post-management.component';
import { CreatePostComponent } from './post-management/create-post/create-post.component';
import { ForumComponent } from './forum/forum.component';
import { TagPostComponent } from './forum/tag-post/tag-post.component';
import { CategoryPostComponent } from './forum/category-post/category-post.component';
import { DetailPostComponent } from './forum/detail-post/detail-post.component';
import { MypostComponent } from './mypost/mypost.component';

const routes: Routes = [
  { path: "signin", component: UserSigninComponent },
  { path: "signup", component: UserSignupComponent },
  { path: "category-management", component: CategoryManagementComponent },
  { path: "category-management/create", component: CreateCateComponent },
  { path: "tag-management", component: TagManagementComponent },
  { path: "tag-management/create", component: CreateTagComponent },
  { path: "post-management", component: PostManagementComponent },
  { path: "create-post", component: CreatePostComponent },
  { path: "forum", component: ForumComponent },
  { path: "postOfCate/:cateId", component: CategoryPostComponent },
  { path: "postOfTag/:tagId", component: TagPostComponent },
  { path: "detailPost/:postId", component: DetailPostComponent },
  { path: "myPost", component: MypostComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
