import { Component, OnInit, ViewEncapsulation, OnDestroy } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { Location } from '@angular/common';
import { FuseUtils } from '@fuse/utils';
import { AdminRegionService } from './admin-region.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material';
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { AuthService } from 'app/_services/auth.service';
import { AdminRegion } from './admin-region.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-region',
  templateUrl: './admin-region.component.html',
  styleUrls: ['./admin-region.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations   : fuseAnimations
})
export class AdminRegionComponent implements OnInit, OnDestroy {

  adminRegion: AdminRegion;
  regionForm: FormGroup;
  pageType: string;
   // Private
   private _unsubscribeAll: Subject<any>;
   
  constructor(
    private _adminRegionService: AdminRegionService,
    private _formBuilder: FormBuilder,
    private _matSnackBar: MatSnackBar,
    private router: Router,
    private _location: Location,
    private authService: AuthService
    ) { 
      this.adminRegion = new AdminRegion();
      this._unsubscribeAll = new Subject();
    }

  ngOnInit(): void {
    this._adminRegionService.onRegionChanged
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe(region => {
        if ( region) {
          console.log('region001: ', region);
          
          this.adminRegion = new AdminRegion(region);
          console.log('adminRegion: ', this.adminRegion);
          
          this.pageType = 'edit';
        }
        else  {
          this.pageType = 'new';
        }

        this.regionForm = this.createRegionForm();
      });
  }

  ngOnDestroy(): void
    {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    // id: number;
    // userName: string;
    // gender: string;
    // dateOfBirth: string;
    // knownAs: string;
    // created: string;
    // lastActive: string;
    // introduction: string;
    // lookingFor: string;
    // interests: string;
    // city: string;
    // country: string;
    // photos: [
    //     {
    //        'id': number, 
    //        'url': string,
    //        'description': string,
    //        'dateAdded': string,
    //        'isMain': boolean,
    //        'publicId': string,
    //        'isApproved': boolean,
    //        'userId': number
    //     }
    // ];

    createRegionForm(): FormGroup {
      return this._formBuilder.group({
        id: [this.adminRegion.id],
        nameRu: [this.adminRegion.nameRu],
        nameEn: [this.adminRegion.nameEn],
        nameKz: [this.adminRegion.nameKz]
      });
    }

     /**
     * Save product
     */
    saveRegion(): void
    {
      const data = this.regionForm.getRawValue();
        // data.handle = FuseUtils.handleize(data.id);
        
        // this._adminRegionService.saveRegion(this.authService.decodedToken.nameid, data)
        this._adminRegionService.saveRegion(data.id, data)
            .then(() => {

                // Trigger the subscription with new data
                this._adminRegionService.onRegionChanged.next(data);

                // Show the success message
                this._matSnackBar.open('Region saved', 'OK', {
                    verticalPosition: 'top',
                    duration        : 2000
                });
            });
    }

    /**
     * Add product
     */
    addRegion(): void
    {
        const data = this.regionForm.getRawValue();
        // data.handle = FuseUtils.handleize(data.name);

        this._adminRegionService.addRegion(data)
            .then((region) => {
              // console.log('resp -- ', region);
              
              // Trigger the subscription with new data
               this._adminRegionService.onRegionChanged.next(data);
                
                

                // Show the success message
                this._matSnackBar.open('Region added', 'OK', {
                    verticalPosition: 'top',
                    duration        : 2000
                });

                // this.router.navigate(['/admin/admin-regions/']);
                // Change the location with new one
                // this._location.go('admin/admin-regions/' + this.region.id + '/' + this.product.handle);
                
                this.router.navigate(['/admin/admin-regions/', region.id]);
                // this._location.go('admin/admin-regions/' + response.id);
            });
    }

    /**
     * Add product
     */
    removeRegion(): void
    {
        const data = this.regionForm.getRawValue();
        // data.handle = FuseUtils.handleize(data.name);
        console.log('nameid: ', this.authService.decodedToken.nameid);
        
        // this._adminRegionService.saveRegion(this.authService.decodedToken.nameid, data)
        this._adminRegionService.removeRegion(data.id, data.id)
            .then(() => {

                // Trigger the subscription with new data
                this._adminRegionService.onRegionChanged.next(data);

                // Show the success message
                this._matSnackBar.open('Region deleted', 'OK', {
                    verticalPosition: 'top',
                    duration        : 2000
                });

                this.router.navigate(['/admin/admin-regions/']);
            });
    }

}
