import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'environments/environment';
// import { AdminRegion } from './admin-region.model';
import { AdminRole } from './admin-role.model';
// import { Region } from 'app/_models/user';


@Injectable({
    providedIn: 'root'
})
export class AdminRoleService implements Resolve<any> {
  baseUrl = environment.apiUrl + 'role/';
    routeParams: any;
    region: any;
    onRegionChanged: BehaviorSubject<any>;

    /**
     *
     */
    constructor(private _httpClient: HttpClient) {
        this.onRegionChanged = new BehaviorSubject({});
    }

    resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<any> | Promise<any> {
        this.routeParams = route.params;

        return new Promise((resolve, reject) => {
            Promise.all([this.getRegion()]).then(() => {
                resolve();
            }, reject);
        });
    }

    getRegion(): Promise<any>
    {
        return new Promise((resolve, reject) => {
            if ( this.routeParams.id === 'new' )
            {
                this.onRegionChanged.next(false);
                resolve(false);
            }
            else
            {
                this._httpClient.get(this.baseUrl + 'getRoleById/' +  this.routeParams.id)
                    .subscribe((response: any) => {
                        this.region = response;
                        // console.log(this.region);
                        
                        this.onRegionChanged.next(this.region);
                        resolve(response);
                    }, reject);
            }
        });
    }

    // saveUser(user): Promise<any>
    // {
    //     return new Promise((resolve, reject) => {
    //         this._httpClient.post(this.baseUrl + user.id, user)
    //             .subscribe((response: any) => {
    //                 resolve(response);
    //             }, reject);
    //     });
    // }

    saveRegion(id: string, region: AdminRole): Promise<any>
    {
       
        return new Promise((resolve, reject) => {
            this._httpClient.put(this.baseUrl + region.id, region)
                .subscribe((response: any) => {
                    resolve(response);
                }, reject);
        });
    }

    removeRegion(id: string, regionId: string): Promise<any>
    {
    //    console.log('regionId: ', this.baseUrl + 'remove/' +  regionId);
       
        return new Promise((resolve, reject) => {
            this._httpClient.delete(this.baseUrl + regionId)
                .subscribe((response: any) => {
                    resolve(response);
                }, reject);
        });
        
    }

    // /**
    //  * Add product
    //  *
    //  * @param product
    //  * @returns {Promise<any>}
    //  */
    addRegion(region): Promise<any>
    {
        return new Promise((resolve, reject) => {
            this._httpClient.post(this.baseUrl + 'addRegion/', region)
                .subscribe((response: any) => {
                    console.log('response: ', response);
                    resolve(response);
                }, reject);
        });
    }
}
