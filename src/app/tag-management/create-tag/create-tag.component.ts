import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { TagService } from 'src/app/services/tag.service';

@Component({
  selector: 'app-create-tag',
  templateUrl: './create-tag.component.html',
  styleUrls: ['./create-tag.component.css']
})
export class CreateTagComponent implements OnInit {

  formCreateTag = this.formBuilder.group({
    Name: ["", Validators.required]
  })
  success: boolean = false
  message: string = ""
  showMessage: boolean = false

  constructor(private formBuilder: FormBuilder, private tagSvc: TagService) { }

  ngOnInit(): void {
  }

  clear(){
    this.formCreateTag.controls["Name"].setValue("")
  }

  onSubmit() {
    let name = this.formCreateTag.controls["Name"].value
    this.tagSvc.createTag(name).subscribe((d: any) => {
      if (d.success) {
        this.success = true
      }
      this.message = d.message
      this.showMessage = true
      this.clear()
    })
  }
}
