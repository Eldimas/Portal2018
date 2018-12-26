import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { createHttpObservable } from 'app/utils/util';
import { RegionForSelection } from 'app/_models/region.model';

@Injectable({
  providedIn: 'root'
})
export class RegionService {

baseUrl = environment.apiUrl;

constructor() { }

getRegions(): any {
  return createHttpObservable(this.baseUrl + 'region/GetRegionsForSelect');
}

}
