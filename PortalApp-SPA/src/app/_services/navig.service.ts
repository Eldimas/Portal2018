import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { createHttpObservable } from 'app/utils/util';
import { Navig } from 'app/_models/navig';
import { NavigUpdate } from 'app/_models/navigUpdate.model';
import { HttpClient } from '@angular/common/http';
import { FuseNavigationService } from '@fuse/components/navigation/navigation.service';
import { map } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class NavigService {
    baseUrl = environment.apiUrl + 'navig/';

    constructor(private http: HttpClient) {}

    getNavig(lang: string): any {
        return createHttpObservable(
            this.baseUrl + `getNavig/${lang}`
        );
    }

    // tslint:disable-next-line:typedef
    addNavig(navig: Navig) {
        console.log('NavigUpdate: ', navig);
        console.log('Path: ', this.baseUrl + 'add/');

       return this.http.post(this.baseUrl + 'add/', navig);
        // this.http.post(this.baseUrl + 'addId/', 1);
    }

    // tslint:disable-next-line:typedef
    updateNavig(navig: Navig) {
        return this.http.put(this.baseUrl + 'update/', navig);
    }

    // tslint:disable-next-line:typedef
    deleteNavig(navigId: string) {
        return this.http.delete(this.baseUrl + 'delete/' + navigId);
    }

    getNavigById(id: string): any {
        // alert(`navigId: ${id}`);

      return createHttpObservable(
            this.baseUrl + `getNavigById/${id}`
        ); 
        
    }
}
