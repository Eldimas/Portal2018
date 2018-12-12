import { MatChipInputEvent } from '@angular/material';

import { FuseUtils } from '@fuse/utils';

export class AdminDepartment
{
    id: string;
    keyIndex: string;
    // nameEn: string;
    // nameKz: string;
    // departmentVs: [
    //     {
    //         'created': string,
    //         'departmentId': string,
    //         'disabled': boolean,
    //         'displayNameEng': string,
    //         'displayNameKaz': string,
    //         'displayNameRus': string,
    //         'fromNameEng': string,
    //         'fromNameKaz': string,
    //         'fromNameRus': string,
    //         'id': string,
    //         'name': string,
    //         'priority': number,
    //         'regionId': string,
    //         'shortName': string,
    //         'toNameEng': string,
    //         'toNameKaz': string,
    //         'toNameRus': string
    //     }
    // ];
    

    /**
     *
     */
    constructor(dep?) {
        // console.log('user: ', user);
        
        dep = dep || {};
        this.id = dep.id || '';
        this.keyIndex = dep.keyIndex || '';
        
    }

} 
