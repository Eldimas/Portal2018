import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'environments/environment';
import { AdminDepV } from '../models/admin-dep-vs.model';


@Injectable({
    providedIn: 'root'
})
export class AdminDepVsService {
    baseUrl = environment.apiUrl + 'department/';
    // routeParams: any;
    depV: any;
    onDepartmentVChanged: BehaviorSubject<any>;

    /**
     *
     */
    constructor(private _httpClient: HttpClient) {
        this.onDepartmentVChanged = new BehaviorSubject({});
    }

    // resolve(
    //     route: ActivatedRouteSnapshot,
    //     state: RouterStateSnapshot
    // ): Observable<any> | Promise<any> {
    //     this.routeParams = route.params;

    //     return new Promise((resolve, reject) => {
    //         Promise.all([this.getDepVs()]).then(() => {
    //             resolve();
    //         }, reject);
    //     });
    // }

    // getDepVs(): Promise<any>
    // {
    //     return new Promise((resolve, reject) => {
    //         if ( this.routeParams.id === 'new' )
    //         {
    //             this.onDepartmentChanged.next(false);
    //             resolve(false);
    //         }
    //         else
    //         {
    //             this._httpClient.get(this.baseUrl + 'getDepartment/' +  this.routeParams.id)
    //                 .subscribe((response: any) => {
    //                     this.depV = response;
    //                     // console.log(this.region);
                        
    //                     this.onDepartmentChanged.next(this.depV);
    //                     resolve(response);
    //                 }, reject);
    //         }
    //     });
    // }


    saveDepV(id: string, dep: AdminDepV): Promise<any>
    {
       
        return new Promise((resolve, reject) => {
            this._httpClient.put(this.baseUrl + 'depv/update/' + dep.id, dep)
                .subscribe((response: any) => {
                    resolve(response);
                }, reject);
        });
    }

    // removeDepartment(id: string, depId: string): Promise<any>
    // {
    //     return new Promise((resolve, reject) => {
    //         this._httpClient.delete(this.baseUrl + depId)
    //             .subscribe((response: any) => {
    //                 resolve(response);
    //             }, reject);
    //     });
        
    // }

    
    addDepV(dep): Promise<any>
    {
        return new Promise((resolve, reject) => {
            this._httpClient.post(this.baseUrl + 'depv/add', dep)
                .subscribe((response: any) => {
                    console.log('response: ', response);
                    resolve(response);
                }, reject);
        });
    }
}
