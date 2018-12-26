import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ActivatedRouteSnapshot, RouterStateSnapshot, Resolve } from '@angular/router';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AdminDepsService implements Resolve<any> {
  baseUrl = environment.apiUrl + 'department/';
  departments: any[];
  onDepartmentsChanged: BehaviorSubject<any>;


  constructor(private _httpClient: HttpClient) { 
    this.onDepartmentsChanged = new BehaviorSubject({});
    
  }

  //  /**
  //    * Resolver
  //    *
  //    * @param {ActivatedRouteSnapshot} route
  //    * @param {RouterStateSnapshot} state
  //    * @returns {Observable<any> | Promise<any> | any}
  //    */
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> | Promise<any> | any
    {
        
        
        return new Promise((resolve, reject) => {

            Promise.all([
                this.getDepartments()
            ]).then(
                () => {
                    resolve();
                },
                reject
            );
        });
    }

    //  /**
    //  * Get products
    //  *
    //  * @returns {Promise<any>}
    //  */
    getDepartments(): Promise<any>
    {
        return new Promise((resolve, reject) => {
            this._httpClient.get(this.baseUrl + 'getDepartments/')
                .subscribe((response: any) => {
                    this.departments = response;
                    // console.log(this.departments);
                    
                    this.onDepartmentsChanged.next(this.departments);
                    resolve(response);
                }, reject);
        });
    }
}
