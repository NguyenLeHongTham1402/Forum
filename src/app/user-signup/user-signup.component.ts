import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { UserService } from '../services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-signup',
  templateUrl: './user-signup.component.html',
  styleUrls: ['./user-signup.component.css']
})
export class UserSignupComponent implements OnInit {


  public signupForm = this.formBuilder.group({
    realname: ['', Validators.required],
    username: ['', Validators.required],
    pass: ['', Validators.required],
    email: ['', [Validators.email, Validators.required]],
    CheckInput: [false, Validators.requiredTrue],
  })

  show: boolean = false
  success: boolean = true
  message: string = ''
  image: any;

  constructor(private formBuilder: FormBuilder, private userSvc: UserService, private router: Router) { }

  ngOnInit(): void {}

  onSubmit() {
    let realname = this.signupForm.controls["realname"].value;
    let username = this.signupForm.controls["username"].value;
    let pass = this.signupForm.controls["pass"].value;
    let email = this.signupForm.controls["email"].value;

    this.userSvc.signup(username, pass, email, realname).subscribe((d: any) => {
      console.info(d);

      if (d.success === true) {
        this.message = d.message
        this.success = true
      }
      else {
        this.message = d.message
        this.success = false
      }

      this.show = true
    })
  }
}
