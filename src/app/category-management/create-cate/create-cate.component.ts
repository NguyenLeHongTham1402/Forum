import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-create-cate',
  templateUrl: './create-cate.component.html',
  styleUrls: ['./create-cate.component.css']
})
export class CreateCateComponent implements OnInit {

  formCreate = this.formBuilder.group({
    Name: ["", Validators.required]
  })
  success: boolean = false
  message: string = ""
  showMessage: boolean = false

  constructor(private formBuilder: FormBuilder, private cateSvc: CategoryService) { }

  ngOnInit(): void {
    this.showMessage = false
  }

  onSubmit() {
    let name = this.formCreate.controls["Name"].value
    this.cateSvc.createCategory(name).subscribe((d: any) => {
      if (d.success) {
        this.success = true
      }
      this.message = d.message
      this.showMessage = true
    })
  }

}
