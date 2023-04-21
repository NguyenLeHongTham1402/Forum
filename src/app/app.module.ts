import { CUSTOM_ELEMENTS_SCHEMA, NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {HttpClientModule} from '@angular/common/http'
import { UserSignupComponent } from './user-signup/user-signup.component';
import { UserSigninComponent } from './user-signin/user-signin.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {DxFileUploaderModule} from 'devextreme-angular/ui/file-uploader';
import { CategoryManagementComponent } from './category-management/category-management.component';
import { CreateCateComponent } from './category-management/create-cate/create-cate.component';
import { TagManagementComponent } from './tag-management/tag-management.component';
import { CreateTagComponent } from './tag-management/create-tag/create-tag.component';
import { PostManagementComponent } from './post-management/post-management.component';
import { CreatePostComponent } from './post-management/create-post/create-post.component';
import { ForumComponent } from './forum/forum.component';
import { DetailPostComponent } from './forum/detail-post/detail-post.component';
import { CategoryPostComponent } from './forum/category-post/category-post.component';
import { TagPostComponent } from './forum/tag-post/tag-post.component';
import { MypostComponent } from './mypost/mypost.component'

@NgModule({
  declarations: [
    AppComponent,
    UserSignupComponent,
    UserSigninComponent,
    CategoryManagementComponent,
    CreateCateComponent,
    TagManagementComponent,
    CreateTagComponent,
    PostManagementComponent,
    CreatePostComponent,
    ForumComponent,
    DetailPostComponent,
    CategoryPostComponent,
    TagPostComponent,
    MypostComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    DxFileUploaderModule,
    ReactiveFormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
  schemas:[
    NO_ERRORS_SCHEMA,
    CUSTOM_ELEMENTS_SCHEMA
  ]
})
export class AppModule { }
