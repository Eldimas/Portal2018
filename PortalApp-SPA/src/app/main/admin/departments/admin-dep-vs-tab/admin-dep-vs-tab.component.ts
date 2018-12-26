import { Component, OnInit, Input } from '@angular/core';
import { AdminDepV } from '../models/admin-dep-vs.model';
import { AdminDepartment } from '../admin-dep/admin-dep.model';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatSnackBar } from '@angular/material';
import { AdminDepVsService } from '../admin-dep-vs-accord/admin-dep-vs.service';
import { createHttpObservable } from 'app/utils/util';
import { environment } from 'environments/environment';
import { RegionService } from 'app/_services/region.service';
import { RegionForSelection } from 'app/_models/region.model';

export interface Food {
    value: string;
    viewValue: string;
  }

@Component({
    selector: 'app-admin-dep-vs-tab',
    templateUrl: './admin-dep-vs-tab.component.html',
    styleUrls: ['./admin-dep-vs-tab.component.scss']
})
export class AdminDepVsTabComponent implements OnInit {
    baseUrl = environment.apiUrl;
    @Input() depV: AdminDepV;
    @Input() department: AdminDepartment;
    @Input() regions: Array<RegionForSelection>;
    depVForm: FormGroup;

    

    foods: Food[] = [
        {value: 'steak-0', viewValue: 'Steak'},
        {value: 'pizza-1', viewValue: 'Pizza'},
        {value: 'tacos-2', viewValue: 'Tacos'}
      ];

    constructor(
        private _formBuilder: FormBuilder,
        private _matSnackBar: MatSnackBar,
        private _adminDepVsService: AdminDepVsService,
        private _regionService: RegionService
    ) {}

    ngOnInit(): void {
        this.depVForm = this.createDepForm();
        // console.log('regions from tab: ', this.regionsFromTab);
        // console.log('department: ', this.department);
        
    }

    saveDepV(): void {
        const data = this.depVForm.getRawValue();
        console.log('data', data);
        console.log('data.id: ', data.id);

        this._adminDepVsService.saveDepV(data.id, data).then(() => {
            // Trigger the subscription with new data
            this._adminDepVsService.onDepartmentVChanged.next(data);
            // Show the success message
            this._matSnackBar.open('DepVs saved', 'OK', {
                verticalPosition: 'top',
                duration: 2000
            });
            // this.panelOpenState = false;
            this.depVForm.markAsPristine();
        });
    }

    addDepV(): void {
        const data = this.depVForm.getRawValue();
        console.log('data', data);
        console.log('data.id: ', data.id);
        data.departmentId = this.department.id;

        this._adminDepVsService.addDepV(data).then(dep => {
            // console.log('resp -- ', region);

            // Trigger the subscription with new data
            this._adminDepVsService.onDepartmentVChanged.next(data);

            // Show the success message
            this._matSnackBar.open('DepartmentV added', 'OK', {
                verticalPosition: 'top',
                duration: 2000
            });

            // this.router.navigate(['/admin/admin-regions/']);
            // Change the location with new one
            // this._location.go('admin/admin-regions/' + this.region.id + '/' + this.product.handle);

            // this.router.navigate(['/admin/admin-deps/', dep.id]);
            // this._location.go('admin/admin-regions/' + response.id);
        });
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
