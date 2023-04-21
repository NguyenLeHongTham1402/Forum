import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { CategoryService } from '../services/category.service';

@Component({
  selector: 'app-category-management',
  templateUrl: './category-management.component.html',
  styleUrls: ['./category-management.component.css']
})
export class CategoryManagementComponent implements OnInit {

  // Edit Category
  public listCategories: any = []
  editCategory = {
    id: 0,
    name: ""
  }
  editCate = this.formBuilder.group({
    editId: ["", Validators.required],
    editName: ["", Validators.required]
  })
  showNofiEdit: boolean = false
  editSuccess: boolean = false
  editMessage: string = ""
  isEditSubmit: boolean = false

  // Delete Category  
  cateId: number = 0
  showNofiDel: boolean = false
  delSuccess: boolean = false
  delMessage: string = ""
  constructor(private formBuilder: FormBuilder, private cateSvc: CategoryService) { }

  ngOnInit(): void {
    this.getListCategory()
  }

  getListCategory() {
    this.cateSvc.getListCategories().subscribe(data => {
      console.log(data)
      this.listCategories = data
    })
  }

  edit(cate: any) {
    this.editCategory = cate
  }

  updateCategory() {
    let id = Number(this.editCate.controls["editId"].value)
    let name = this.editCate.controls["editName"].value

    this.cateSvc.updateCategory(name, id).subscribe((d: any) => {
      console.log(d)
      if (d.success) {
        this.editSuccess = true
      }
      this.editMessage = d.message
      this.showNofiEdit = true
      this.isEditSubmit = true
    })
  }

  checkSubmitEdit() {
    if (!this.isEditSubmit) {
      alert("Warning: You have not clicked the 'Submit' button to update the data!!!\nRefresh to see unupdated data.")
    }
    else {
      this.isEditSubmit = false
    }
  }

  delCate(id: number) {
    this.cateId = id
  }

  removeCate() {
    this.cateSvc.deleteCategory(this.cateId).subscribe((d: any) => {
      console.log(d)
      if (d.success) {
        this.delSuccess = true
      }
      this.delMessage = d.message
      this.showNofiDel = true
    })
  }

  textChange(item:any){
    console.log(item)
  }
}
