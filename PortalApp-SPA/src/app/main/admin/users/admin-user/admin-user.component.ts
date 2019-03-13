import { Component, OnInit, ViewEncapsulation, OnDestroy, Inject, LOCALE_ID } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { FuseUtils } from '@fuse/utils';
import { Location, DatePipe, formatDate } from '@angular/common';
import { AdminUserService } from './admin-user.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material';
import { takeUntil } from 'rxjs/operators';
import { Subject, Observable, noop, Subscription } from 'rxjs';
import { AdminUser } from './admin-user.model';
import { AuthService } from 'app/_services/auth.service';
import { environment } from 'environments/environment';
import { createHttpObservable } from 'app/utils/util';
import { Role } from 'app/_models/role.model';
import { AdminUserV } from './admin-userv.model';
import * as uuid from 'uuid';
import { AdminDepsService } from '../../departments/admin-deps/admin-deps.service';

@Component({
  selector: 'app-admin-user',
  templateUrl: './admin-user.component.html',
  styleUrls: ['./admin-user.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations   : fuseAnimations
})
export class AdminUserComponent implements OnInit, OnDestroy {
  baseUrl = environment.apiUrl;
  adminUser: AdminUser;
  adminUserV: AdminUserV;
  userForm: FormGroup;
  pageType: string;
  allRoles: Role[];
  deps: any;
   // Private
   private _unsubscribeAll: Subject<any>;
   depsSubscription = new Subscription;
   
  constructor(
    private _adminUserService: AdminUserService,
    private _adminDepsService: AdminDepsService,
    private _formBuilder: FormBuilder,
    private _matSnackBar: MatSnackBar,
    private authService: AuthService,
    @Inject(LOCALE_ID) private locale: string,
    ) { 
      this.adminUser = new AdminUser();
      this.adminUserV = new AdminUserV();
      this._unsubscribeAll = new Subject();
    }

  ngOnInit(): void {

  //  this._adminDepsService.getDepartments().;
  const httpDeps$ = createHttpObservable(environment.apiUrl + 'department/' + 'getDepartmentVs/');
  this.depsSubscription = httpDeps$.subscribe(res => {
    this.deps = res;
    this.deps.sort(this.compare);
  });

    this._adminUserService.onUserChanged
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe(user => {
        if ( user) {
          this.adminUser = new AdminUser(user);
          this.adminUser.userVs.sort(this.compareDepVCreated);
          this.pageType = 'edit';
        }
        else  {
          console.log('some error');
          this.pageType = 'new';
        }

        this.userForm = this.createUserForm();
      });

      ///////////////////////////////////////

      const http$ = createHttpObservable(this.baseUrl + 'role/getAllRoles');

      // const http$ = Observable.create(observer => {
      //   fetch(this.baseUrl + '/role/getAllRoles')
      //   .then(response => {
      //     return response.json();
      //   })
      //   .then(body => {
      //     observer.next(body);
      //     observer.complete();
      //   })
      //   .catch(err => {
      //     observer.error(err);
      //   });
      // });

      http$.subscribe(
        roles => {
          console.log(roles);
          this.allRoles = roles;
          this.allRoles.forEach(role => {
            (this.adminUser.userRoles).forEach(aUser => {
              if (role.name.toUpperCase() === aUser.role.name.toUpperCase() ){
                console.log('aUser', aUser);
                role.isUserRole = true;

              }
            });
          });
        },
        noop,
        () => console.log('completed')
      );
  }

  // tslint:disable-next-line:typedef
  compare(a, b) {
    // tslint:disable-next-line:curly
    if (a.name < b.name)
      return -1;
    // tslint:disable-next-line:curly
    if (a.name > b.name)
      return 1;
    return 0;
  }

   // tslint:disable-next-line:typedef
   compareDepVCreated(a, b) {
    // tslint:disable-next-line:curly
    if (a.created < b.created)
      return 1;
    // tslint:disable-next-line:curly
    if (a.created > b.created)
      return -1;
    return 0;
  }


  ngOnDestroy(): void
    {
      this.depsSubscription.unsubscribe();
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

    createUserForm(): FormGroup {
      return this._formBuilder.group({
        id: [this.adminUser.id],
        userName: [this.adminUser.userName],
        email: [this.adminUser.email],
        phoneNumber: [this.adminUser.phoneNumber],
        created: [this.adminUser.created],
        lastActive: [this.adminUser.lastActive]
        // vCreated: [this.adminUser.vCreated],
        // vDisplayNameRus: [this.adminUser.vDisplayNameRus],
        // vDisplayNameEng: [this.adminUser.vDisplayNameEng],
        // vDisplayNameKaz: [this.adminUser.vDisplayNameKaz],
        // vFromNameRus: [this.adminUser.vFromNameRus],
        // vFromNameEng: [this.adminUser.vFromNameEng],
        // vFromNameKaz: [this.adminUser.vFromNameKaz],
        // vToNameRus: [this.adminUser.vToNameRus],
        // vToNameEng: [this.adminUser.vToNameEng],
        // vToNameKaz: [this.adminUser.vToNameKaz],
        // vPriority: [this.adminUser.vPriority],
        // vDisabled: [this.adminUser.vDisabled]
        
      });
    }

     /**
     * Save product
     */
    saveUser(): void
    {
        const data = this.userForm.getRawValue();
        data.handle = FuseUtils.handleize(data.userName);
        // console.log('save: ', data);
        

        this._adminUserService.saveUser(this.authService.decodedToken.nameid, data)
            .then(() => {

                // Trigger the subscription with new data
                this._adminUserService.onUserChanged.next(data);

                // Show the success message
                this._matSnackBar.open('User saved', 'OK', {
                    verticalPosition: 'top',
                    duration        : 2000
                });
            });
    }

    addUserV(adminUserVs: AdminUserV[]): void {
    //  const lastVersion = new AdminUserV();
     let lastV: AdminUserV;
      if (adminUserVs.length > 0) {
        let lastVersion = new AdminUserV(adminUserVs[0]);
        adminUserVs.forEach(uv => {
          if (formatDate(uv.created, 'yyyy-MM-dd', this.locale) > formatDate(lastVersion.created, 'yyyy-MM-dd', this.locale)) {
            lastVersion = new AdminUserV(uv);
          }
        });
        lastV = new AdminUserV(lastVersion);
      } else {
        lastV = new AdminUserV();
      }
      
   
    // this.adminUserV.created = formatDate(Date.now(), 'yyyy-MM-dd', this.locale);
    // this.adminUser.userVs.unshift(this.adminUserV);

    lastV.created = formatDate(Date.now(), 'yyyy-MM-dd', this.locale);
    lastV.id = uuid.v4();
    this.adminUser.userVs.unshift(lastV);

    //   this.adminDepV.name = 'New Version';
    //  this.adminDepV.created = formatDate(Date.now(), 'yyyy-MM-dd', this.locale);
    //   this.adminDepartment.departmentVs.unshift(this.adminDepV);
    
     }

     // tslint:disable-next-line:typedef
     addUserVersion(userV: AdminUserV) {
        console.log('UserV: ', userV);
        this._adminUserService.saveUserV(this.adminUser.id, userV);
     }

}
