import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'ForumUI';

  constructor(private router:Router){}

  onSignOut(){
    localStorage.removeItem("userInfo");
  }

  getUser():any{
    const user = localStorage.getItem("userInfo");
    let userinfo = JSON.parse(user!) 
    return userinfo
  }

  get isAdmin(){
    const user = localStorage.getItem("userInfo");
    let userinfo = JSON.parse(user!) 
    return userinfo.role=="ADMIN"
  }

  get isUserlogin(){
    const user = localStorage.getItem("userInfo");    
    return user && user.length>0;
  }
}
