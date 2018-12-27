import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { createHttpObservable } from 'app/utils/util';

@Injectable({
    providedIn: 'root'
})
export class NavigService {
    baseUrl = environment.apiUrl + 'navig/';

    constructor() {}
    getNavig(): any {
        return createHttpObservable(
            this.baseUrl + 'getNavig/'
        );
    }
}
