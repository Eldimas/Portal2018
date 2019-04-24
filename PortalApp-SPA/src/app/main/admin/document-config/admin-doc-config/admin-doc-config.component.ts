import { Component, OnInit, ViewEncapsulation, OnDestroy } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { Location } from '@angular/common';
import { FuseUtils } from '@fuse/utils';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material';
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { AuthService } from 'app/_services/auth.service';
import { Router } from '@angular/router';
import { DocumentConfig } from '../document-config.model';
import { AdminDocConf } from './admin-doc-config.model';
import { AdminDocConfService } from './admin-doc-config.service.service';


 @Component({
  selector: 'app-admin-doc-config',
  templateUrl: './admin-doc-config.component.html',
  styleUrls: ['./admin-doc-config.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations   : fuseAnimations
})
export class AdminDocConfComponent implements OnInit, OnDestroy {

  adminDocConfig: AdminDocConf;
  adminDocConfV: DocumentConfig;
  docConfForm: FormGroup;
  pageType: string;
  // Private
  private _unsubscribeAll: Subject<any>;


  constructor(   
    private _adminDocConfService: AdminDocConfService,
    private _formBuilder: FormBuilder,
    private _matSnackBar: MatSnackBar,
    private router: Router,
    private _location: Location,
    private authService: AuthService) { 
      this.adminDocConfig = new AdminDocConf();
      this.adminDocConfV = new DocumentConfig();
      this._unsubscribeAll = new Subject();
    }

    ngOnInit(): void {
      this._adminDocConfService.onDocConfChanged
        .pipe(takeUntil(this._unsubscribeAll))
        .subscribe(docConf => {
          if ( docConf ) {
            console.log('docConf001: ', docConf);
            /* console.log('docConfForm: ', docConfForm); */
            
            this.adminDocConfig = new AdminDocConf(docConf);
            this.adminDocConfig.documentConfigVs.sort((a, b) => b.created.localeCompare(a.created));
            console.log('adminDocConf: ', this.adminDocConfig);
            
            this.pageType = 'edit';
          }
          else  {
            this.pageType = 'new';
          }
  
          this.docConfForm = this.createDocConfForm();
        });
    }

    ngOnDestroy(): void
    {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    createDocConfForm(): FormGroup {
      return this._formBuilder.group({
        id: [this.adminDocConfig.id],
        documentType: [this.adminDocConfig.documentType]
      });
    }

    addDocConf(): void {
      const data = this.docConfForm.getRawValue();
        // data.handle = FuseUtils.handleize(data.name);

        this._adminDocConfService.addDocConf(data)
            .then((docConf) => {
              // console.log('resp -- ', region);
              
              // Trigger the subscription with new data
               this._adminDocConfService.onDocConfChanged.next(data);
                
                

                // Show the success message
                this._matSnackBar.open('DocConf added', 'OK', {
                    verticalPosition: 'top',
                    duration        : 2000
                });

                // this.router.navigate(['/admin/admin-regions/']);
                // Change the location with new one
                // this._location.go('admin/admin-regions/' + this.region.id + '/' + this.product.handle);
                
                this.router.navigate(['/admin/admin-docConfs/', docConf.id]);
                // this._location.go('admin/admin-regions/' + response.id);
            });
    }

    saveDocConf(): void
    {
      const data = this.docConfForm.getRawValue();
        // data.handle = FuseUtils.handleize(data.id);
        
        // this._adminRegionService.saveRegion(this.authService.decodedToken.nameid, data)
        this._adminDocConfService.saveDocConf(data.id, data)
            .then(() => {

                // Trigger the subscription with new data
                this._adminDocConfService.onDocConfChanged.next(data);

                // Show the success message
                this._matSnackBar.open('DocConf saved', 'OK', {
                    verticalPosition: 'top',
                    duration        : 2000
                });
            });
    }

    removeDocConf(): void
    {
        const data = this.docConfForm.getRawValue();
        // data.handle = FuseUtils.handleize(data.name);
        console.log('nameid: ', this.authService.decodedToken.nameid);
        
        // this._adminRegionService.saveRegion(this.authService.decodedToken.nameid, data)
        this._adminDocConfService.removeDocConf(data.id, data.id)
            .then(() => {

                // Trigger the subscription with new data
                this._adminDocConfService.onDocConfChanged.next(data);

                // Show the success message
                this._matSnackBar.open('DocConfartment deleted', 'OK', {
                    verticalPosition: 'top',
                    duration        : 2000
                });

                this.router.navigate(['/admin/document-config/']);
            });
    }

    addDocConfV(): void 
    {
      this.adminDocConfV.title = 'New Version';
      this.adminDocConfig.documentConfigVs.push(this.adminDocConfV);
    }



}
