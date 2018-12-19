import { Component, OnInit } from '@angular/core';

import { FuseTranslationLoaderService } from '@fuse/services/translation-loader.service';

import { locale as english } from './i18n/en';
import { locale as kazakh } from './i18n/kz';
import { locale as russian } from './i18n/ru';
import { AdminService } from 'app/_services/admin.service';
import { createHttpObservable } from 'app/utils/util';
import { environment } from 'environments/environment';
import { map } from 'rxjs/operators';
import { AuthService } from 'app/_services/auth.service';
import { Observable } from 'rxjs';

@Component({
    selector: 'sample',
    templateUrl: './sample.component.html',
    styleUrls: ['./sample.component.scss']
})
export class SampleComponent implements OnInit {
    users: any;
    regions: any;
    baseUrl: string = environment.apiUrl + 'region/';
    // /**
    //  * Constructor
    //  *
    //  * @param {FuseTranslationLoaderService} _fuseTranslationLoaderService
    //  */
    constructor(
        private _fuseTranslationLoaderService: FuseTranslationLoaderService,
        private _adminService: AdminService,
        private _authService: AuthService
    ) {
        this._fuseTranslationLoaderService.loadTranslations(
            english,
            kazakh,
            russian
        );
    }

    ngOnInit(): void {
        // this._adminService.getUsersWithRoles().subscribe((users: any) => {
        //     this.users = users;
        //   }, error => {
        //     console.log(error);
        //   });

        const http$ = createHttpObservable(this.baseUrl + 'getRegions/');


        // http$.subscribe(
        //     regions => {
        //         console.log(regions);
        //         this.regions = regions;
        //     },
        //     error => console.log(error),
        //     () => console.log('completed')
        // );

        

        // const regions$ = http$
        // .pipe(
        //     map(res => Object.values(res))
        // );

       http$.subscribe(regions => {
           this.regions = regions;
       });
    }
}
