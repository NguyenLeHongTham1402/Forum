import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { UserService } from '../services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-signin',
  templateUrl: './user-signin.component.html',
  styleUrls: ['./user-signin.component.css']
})
export class UserSigninComponent implements OnInit {

  public signinForm = this.formBuilder.group({
    username:['',Validators.required],
    pass:['',Validators.required]
  })

  success:boolean=true
  message:string=""

  constructor(private formBuilder:FormBuilder, private userSvc:UserService, private router:Router) { }
  ngOnInit(): void {
  }

  onSubmit(){
    let username = this.signinForm.controls["username"].value
    let pass = this.signinForm.controls["pass"].value

    this.userSvc.signin(username, pass).subscribe((d:any) => {
      if(d.success===true){
        localStorage.setItem("userInfo", JSON.stringify(d.data))
        this.router.navigate(["/"])
      }
      else{
        this.message=d.message
        this.success=false
      }
    })
  }

}
