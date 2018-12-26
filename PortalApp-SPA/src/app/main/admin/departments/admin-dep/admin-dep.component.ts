import { Component, OnInit, ViewEncapsulation, OnDestroy, LOCALE_ID, Inject } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { Location, DatePipe, formatDate } from '@angular/common';
import { FuseUtils } from '@fuse/utils';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material';
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { AuthService } from 'app/_services/auth.service';
import { Router } from '@angular/router';
import { AdminDepartment } from './admin-dep.model';
import { AdminDepService } from './admin-dep.service';
import { AdminDepV } from '../models/admin-dep-vs.model';
import { RegionService } from 'app/_services/region.service';
import { RegionForSelection } from 'app/_models/region.model';


@Component({
  selector: 'app-admin-dep',
  templateUrl: './admin-dep.component.html',
  styleUrls: ['./admin-dep.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations   : fuseAnimations
})
export class AdminDepComponent implements OnInit, OnDestroy {

  adminDepartment: AdminDepartment;
  adminDepV: AdminDepV;
  depForm: FormGroup;
  pageType: string;
  regions: RegionForSelection[];
  // Private
  private _unsubscribeAll: Subject<any>;


  constructor(   private _adminDepService: AdminDepService,
    private _formBuilder: FormBuilder,
    private _matSnackBar: MatSnackBar,
    private router: Router,
    private _location: Location,
    private authService: AuthService,
    @Inject(LOCALE_ID) private locale: string,
    private _regionService: RegionService) { 
      this.adminDepartment = new AdminDepartment();
      this.adminDepV = new AdminDepV();
      this.regions = new Array<RegionForSelection>();
      this._unsubscribeAll = new Subject();
    }

    ngOnInit(): void {
      this._adminDepService.onDepartmentChanged
        .pipe(takeUntil(this._unsubscribeAll))
        .subscribe(dep => {
          if ( dep) {
            console.log('dep001: ', dep);
            
            this.adminDepartment = new AdminDepartment(dep);
            this.adminDepartment.departmentVs.sort((a, b) => b.created.localeCompare(a.created));
            console.log('adminDep: ', this.adminDepartment);
            
            this.pageType = 'edit';
          }
          else  {
            this.pageType = 'new';
          }
  
          this.depForm = this.createDepForm();
        });

        const http$ = this._regionService.getRegions();

        http$
        .pipe(takeUntil(this._unsubscribeAll))
        .subscribe(
            regs => {
                // console.log('regionszzz: ', regs);
                regs.forEach(region => {
                  const regForSel = new RegionForSelection(region);
                  this.regions.push(regForSel);
                });
                // this.regions = regs;
            },
            err => console.log(err),
            () => console.log('completed')

        );
    }

    ngOnDestroy(): void
    {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    createDepForm(): FormGroup {
      return this._formBuilder.group({
        id: [this.adminDepartment.id],
        keyIndex: [this.adminDepartment.keyIndex]
      });
    }

    addDepartment(): void {
      const data = this.depForm.getRawValue();
        // data.handle = FuseUtils.handleize(data.name);

        this._adminDepService.addDepartment(data)
            .then((dep) => {
              // console.log('resp -- ', region);
              
              // Trigger the subscription with new data
               this._adminDepService.onDepartmentChanged.next(data);
                
                

                // Show the success message
                this._matSnackBar.open('Department added', 'OK', {
                    verticalPosition: 'top',
                    duration        : 2000
                });

                // this.router.navigate(['/admin/admin-regions/']);
                // Change the location with new one
                // this._location.go('admin/admin-regions/' + this.region.id + '/' + this.product.handle);
                
                this.router.navigate(['/admin/admin-deps/', dep.id]);
                // this._location.go('admin/admin-regions/' + response.id);
            });
    }

    saveDepartment(): void
    {
      const data = this.depForm.getRawValue();
        // data.handle = FuseUtils.handleize(data.id);
        
        // this._adminRegionService.saveRegion(this.authService.decodedToken.nameid, data)
        this._adminDepService.saveDepartment(data.id, data)
            .then(() => {

                // Trigger the subscription with new data
                this._adminDepService.onDepartmentChanged.next(data);

                // Show the success message
                this._matSnackBar.open('Department saved', 'OK', {
                    verticalPosition: 'top',
                    duration        : 2000
                });
            });
    }

    removeDepartment(): void
    {
        const data = this.depForm.getRawValue();
        // data.handle = FuseUtils.handleize(data.name);
        console.log('nameid: ', this.authService.decodedToken.nameid);
        
        // this._adminRegionService.saveRegion(this.authService.decodedToken.nameid, data)
        this._adminDepService.removeDepartment(data.id, data.id)
            .then(() => {

                // Trigger the subscription with new data
                this._adminDepService.onDepartmentChanged.next(data);

                // Show the success message
                this._matSnackBar.open('Department deleted', 'OK', {
                    verticalPosition: 'top',
                    duration        : 2000
                });

                this.router.navigate(['/admin/admin-deps/']);
            });
    }

    addDepV(): void {
     this.adminDepV.name = 'New Version';
    //  this.adminDepV.created = this.datePipe.transform(Date.now(), 'yyyy-MM-dd');
    this.adminDepV.created = formatDate(Date.now(), 'yyyy-MM-dd', this.locale);
     this.adminDepartment.departmentVs.unshift(this.adminDepV);
    }



}
