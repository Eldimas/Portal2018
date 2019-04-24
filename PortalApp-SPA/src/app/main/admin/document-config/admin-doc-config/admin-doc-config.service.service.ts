import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { BehaviorSubject, Observable, from } from 'rxjs';
import { environment } from 'environments/environment';
import { AdminDocConf } from './admin-doc-config.model';
// import { AdminRegion } from './admin-region.model';
// import { Region } from 'app/_models/user';


@Injectable({
    providedIn: 'root'
})
export class AdminDocConfService implements Resolve<any> {
  baseUrl = environment.apiUrl + 'document/';
    routeParams: any;
    docConf: any;
    onDocConfChanged: BehaviorSubject<any>;

    /**
     *
     */
    constructor(private _httpClient: HttpClient) {
        this.onDocConfChanged = new BehaviorSubject({});
    }

    resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<any> | Promise<any> {
        this.routeParams = route.params;

        return new Promise((resolve, reject) => {
            Promise.all([this.getDocConf()]).then(() => {
                resolve();
            }, reject);
        });
    }

    getDocConf(): Promise<any>
    {
        
        return new Promise((resolve, reject) => {
            if ( this.routeParams.id === 'new' )
            {
                this.onDocConfChanged.next(false);
                resolve(false);
            }
            else
            {
                const options = { withCredentials: true, headers: {'Content-Type': 'application/x-www-form-urlencoded'}};

                this._httpClient.get(this.baseUrl + 'getDocumentConfigById/' + this.routeParams.id)
                    .subscribe((response: any) => {
                        this.docConf = response;
                        
                         // console.log('This is wfconfig: ' + this.docConf);
                        
                        this.onDocConfChanged.next(this.docConf);
                        resolve(response);
                    }, reject);
            }
        });
    }


    saveDocConf(id: string, docConf: AdminDocConf): Promise<any>
    {
       
        return new Promise((resolve, reject) => {
            this._httpClient.put(this.baseUrl + docConf.id, docConf)
                .subscribe((response: any) => {
                    resolve(response);
                }, reject);
        });
    }

    removeDocConf(id: string, docConfId: string): Promise<any>
    {
        return new Promise((resolve, reject) => {
            this._httpClient.delete(this.baseUrl + docConfId)
                .subscribe((response: any) => {
                    resolve(response);
                }, reject);
        });
        
    }

    
    addDocConf(docConf): Promise<any>
    {
        return new Promise((resolve, reject) => {
            this._httpClient.post(this.baseUrl + 'addDocumentConfig/', docConf)
                .subscribe((response: any) => {
                    console.log('response: ', response);
                    resolve(response);
                }, reject);
        });
    }
}
