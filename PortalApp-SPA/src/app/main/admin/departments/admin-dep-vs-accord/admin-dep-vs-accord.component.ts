import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder, Form } from '@angular/forms';
import { AdminDepV } from '../models/admin-dep-vs.model';
import { json } from 'body-parser';
import { AdminDepVsService } from './admin-dep-vs.service';
import { MatSnackBar } from '@angular/material';
import { AdminDepartment } from '../admin-dep/admin-dep.model';

// export interface DepartmentV {
//   id: string;
//   created: string;
//   name: string;
//   shortName: string;
//   displayNameRus: string;
//   displayNameEng: string;
//   displayNameKaz: string;
//   fromNameRus: string;
//   fromNameEng: string;
//   fromNameKaz: string;
//   toNameRus: string;
//   toNameEng: string;
//   toNameKaz: string;
//   priority: number;
//   disabled: boolean;
//   regionId: string;
//   // users: [];
//   departmentId: string;
// }

@Component({
  selector: 'app-admin-dep-vs-accord',
  templateUrl: './admin-dep-vs-accord.component.html',
  styleUrls: ['./admin-dep-vs-accord.component.scss']
})
export class AdminDepVsAccordComponent implements OnInit {

  @Input() depV: AdminDepV;
  @Input() department: AdminDepartment;
  depVForm: FormGroup;
  

  panelOpenState = false;
  constructor(
    private _formBuilder: FormBuilder,
    private _matSnackBar: MatSnackBar,
    private _adminDepVsService: AdminDepVsService
  ) { }

  ngOnInit(): void {
    this.depVForm = this.createDepForm();
  }

  saveDepV(): void {
    const data = this.depVForm.getRawValue();
    console.log('data', data);
    console.log('data.id: ', data.id);

    this._adminDepVsService.saveDepV(data.id, data)
      .then(() => {
        // Trigger the subscription with new data
        this._adminDepVsService.onDepartmentVChanged.next(data);
          // Show the success message
          this._matSnackBar.open('DepVs saved', 'OK', {
            verticalPosition: 'top',
            duration        : 2000
        });
        this.panelOpenState = false;
        this.depVForm.markAsPristine();
      });
      
  }

  addDepV(): void {
    const data = this.depVForm.getRawValue();
    console.log('data', data);
    console.log('data.id: ', data.id);
    data.departmentId = this.department.id;

    this._adminDepVsService.addDepV(data)
    .then((dep) => {
      // console.log('resp -- ', region);
      
      // Trigger the subscription with new data
       this._adminDepVsService.onDepartmentVChanged.next(data);
        
        

        // Show the success message
        this._matSnackBar.open('DepartmentV added', 'OK', {
            verticalPosition: 'top',
            duration        : 2000
        });

        // this.router.navigate(['/admin/admin-regions/']);
        // Change the location with new one
        // this._location.go('admin/admin-regions/' + this.region.id + '/' + this.product.handle);
        
        // this.router.navigate(['/admin/admin-deps/', dep.id]);
        // this._location.go('admin/admin-regions/' + response.id);
    });

    // this._adminDepVsService.saveDepV(data.id, data)
    //   .then(() => {
    //     // Trigger the subscription with new data
    //     this._adminDepVsService.onDepartmentVChanged.next(data);
    //       // Show the success message
    //       this._matSnackBar.open('DepVs saved', 'OK', {
    //         verticalPosition: 'top',
    //         duration        : 2000
    //     });
    //     this.panelOpenState = false;
    //     this.depVForm.markAsPristine();
    //   });
      
  }

  createDepForm(): FormGroup {
    return this._formBuilder.group({
      id: [this.depV.id],
      name: [this.depV.name],
      created: [this.depV.created],
      departmentId: [this.depV.departmentId],
      disabled: [this.depV.disabled],
      displayNameEng: [this.depV.displayNameEng],
      displayNameKaz: [this.depV.displayNameKaz],
      displayNameRus: [this.depV.displayNameRus],
      fromNameEng: [this.depV.fromNameEng],
      fromNameKaz: [this.depV.fromNameKaz],
      fromNameRus: [this.depV.fromNameRus],
      priority: [this.depV.priority],
      regionId: [this.depV.regionId],
      shortName: [this.depV.shortName],
      toNameEng: [this.depV.toNameEng],
      toNameKaz: [this.depV.toNameKaz],
      toNameRus: [this.depV.toNameRus],
      
    });
  }

}
