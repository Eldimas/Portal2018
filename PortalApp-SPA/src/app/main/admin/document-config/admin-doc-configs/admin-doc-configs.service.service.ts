import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ActivatedRouteSnapshot, RouterStateSnapshot, Resolve } from '@angular/router';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AdminDocConfsService implements Resolve<any> {
  baseUrl = environment.apiUrl + 'document/';
  docConfs: any[];
  onDocConfsChanged: BehaviorSubject<any>;


  constructor(private _httpClient: HttpClient) { 
    this.onDocConfsChanged = new BehaviorSubject({});
    
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
                this.getDocumentConfs()
            ]).then(
                () => {
                    resolve();
                },
                reject
            );
        });
    }

    //  /**
    //  * Get Document Configurations
    //  *
    //  * @returns {Promise<any>}
    //  */
    getDocumentConfs(): Promise<any>
    {
        return new Promise((resolve, reject) => {
            this._httpClient.get(this.baseUrl + 'getDocumentConfigs/')
                .subscribe((response: any) => {
                    this.docConfs = response;
                    console.log(this.docConfs);
                    
                    this.onDocConfsChanged.next(this.docConfs);
                    resolve(response);
                }, reject);
        });
    }
}
