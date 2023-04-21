import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { TagService } from '../services/tag.service';

@Component({
  selector: 'app-tag-management',
  templateUrl: './tag-management.component.html',
  styleUrls: ['./tag-management.component.css']
})
export class TagManagementComponent implements OnInit {

  editTag = {
    id: 0,
    name: "",
    createdDate: Date.now
  }
  listTags: any = []
  editFormTag = this.formBuilder.group({
    edId: [0, Validators.min],
    edName: ["", Validators.required],
    edCreared: [Date.now, Validators.max]
  })
  success: boolean = false
  message: string = ""
  showMessage: boolean = false

  constructor(private formBuilder: FormBuilder, private tagSvc: TagService) { }

  ngOnInit(): void {
    this.getListTag()
  }

  getListTag() {
    this.tagSvc.getListTags().subscribe((data) => {
      console.log(data)
      this.listTags = data
    })
  }

  edit(tag: any) {
    this.editTag = tag
  }

  onSubmit() {
    let id = Number(this.editFormTag.controls["edId"].value)
    let name = this.editFormTag.controls["edName"].value
    this.tagSvc.updateTag(id, name).subscribe((d: any) => {
      if (d.success) {
        this.success = true
      }
      this.message = d.message
      this.showMessage = true
    })
  }

}
