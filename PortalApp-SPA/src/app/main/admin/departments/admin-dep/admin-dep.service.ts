import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'environments/environment';
import { AdminDepartment } from './admin-dep.model';
// import { AdminRegion } from './admin-region.model';
// import { Region } from 'app/_models/user';


@Injectable({
    providedIn: 'root'
})
export class AdminDepService implements Resolve<any> {
  baseUrl = environment.apiUrl + 'department/';
    routeParams: any;
    department: any;
    onDepartmentChanged: BehaviorSubject<any>;

    /**
     *
     */
    constructor(private _httpClient: HttpClient) {
        this.onDepartmentChanged = new BehaviorSubject({});
    }

    resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<any> | Promise<any> {
        this.routeParams = route.params;

        return new Promise((resolve, reject) => {
            Promise.all([this.getDepartment()]).then(() => {
                resolve();
            }, reject);
        });
    }

    getDepartment(): Promise<any>
    {
        return new Promise((resolve, reject) => {
            if ( this.routeParams.id === 'new' )
            {
                this.onDepartmentChanged.next(false);
                resolve(false);
            }
            else
            {
                this._httpClient.get(this.baseUrl + 'getDepartment/' +  this.routeParams.id)
                    .subscribe((response: any) => {
                        this.department = response;
                        
                        // console.log(this.region);
                        
                        this.onDepartmentChanged.next(this.department);
                        resolve(response);
                    }, reject);
            }
        });
    }


    saveDepartment(id: string, dep: AdminDepartment): Promise<any>
    {
       
        return new Promise((resolve, reject) => {
            this._httpClient.put(this.baseUrl + dep.id, dep)
                .subscribe((response: any) => {
                    resolve(response);
                }, reject);
        });
    }

    removeDepartment(id: string, depId: string): Promise<any>
    {
        return new Promise((resolve, reject) => {
            this._httpClient.delete(this.baseUrl + depId)
                .subscribe((response: any) => {
                    resolve(response);
                }, reject);
        });
        
    }

    
    addDepartment(dep): Promise<any>
    {
        return new Promise((resolve, reject) => {
            this._httpClient.post(this.baseUrl + 'addDepartment/', dep)
                .subscribe((response: any) => {
                    console.log('response: ', response);
                    resolve(response);
                }, reject);
        });
    }
}
