import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'environments/environment';
import { DocumentConfig } from '../document-config.model';


@Injectable({
    providedIn: 'root'
})
export class AdminDocConfVsService {
    baseUrl = environment.apiUrl + 'document/';
    // routeParams: any;
    docConfV: any;
    onDocConfVChanged: BehaviorSubject<any>;

    constructor(private _httpClient: HttpClient) {
        this.onDocConfVChanged = new BehaviorSubject({});
    }
    saveDocConfV(id: string, docConf: DocumentConfig): Promise<any>
    {
       
        return new Promise((resolve, reject) => {
            this._httpClient.post(this.baseUrl + 'docConfV/update', docConf)
                .subscribe((response: any) => {
                    resolve(response);
                }, reject);
        });
    }
  
    addDocConfV(docConf): Promise<any>
    {
        return new Promise((resolve, reject) => {
            this._httpClient.post(this.baseUrl + 'docConfV/add', docConf)
                .subscribe((response: any) => {
                    console.log('response: ', response);
                    resolve(response);
                }, reject);
        });
    }
}
