import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ActivatedRouteSnapshot, RouterStateSnapshot, Resolve } from '@angular/router';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AdminRegionsService implements Resolve<any> {
  baseUrl = environment.apiUrl + 'region/';
  regions: any[];
  onRegionsChanged: BehaviorSubject<any>;


  constructor(private _httpClient: HttpClient) { 
    this.onRegionsChanged = new BehaviorSubject({});
    
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
        console.log('regions');
        
        return new Promise((resolve, reject) => {

            Promise.all([
                this.getRegions()
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
    getRegions(): Promise<any>
    {
        return new Promise((resolve, reject) => {
            this._httpClient.get(this.baseUrl + 'getRegions/')
                .subscribe((response: any) => {
                    this.regions = response;
                    console.log(this.regions);
                    
                    this.onRegionsChanged.next(this.regions);
                    resolve(response);
                }, reject);
        });
    }
}
