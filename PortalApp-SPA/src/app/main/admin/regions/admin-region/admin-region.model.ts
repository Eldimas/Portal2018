import { MatChipInputEvent } from '@angular/material';

import { FuseUtils } from '@fuse/utils';

export class AdminRegion
{
    id: string;
    nameRu: string;
    nameEn: string;
    nameKz: string;
    departmentVs: [
        {
            'created': string,
            'departmentId': string,
            'disabled': boolean,
            'displayNameEng': string,
            'displayNameKaz': string,
            'displayNameRus': string,
            'fromNameEng': string,
            'fromNameKaz': string,
            'fromNameRus': string,
            'id': string,
            'name': string,
            'priority': number,
            'regionId': string,
            'shortName': string,
            'toNameEng': string,
            'toNameKaz': string,
            'toNameRus': string
        }
    ];
    // photos: [
    //     {
    //        'id': number, 
    //        'url': string,
    //        'description': string,
    //        'dateAdded': string,
    //        'isMain': boolean,
    //        'publicId': string,
    //        'isApproved': boolean,
    //        'userId': number
    //     }
    // ];

    /**
     *
     */
    constructor(region?) {
        // console.log('user: ', user);
        
        region = region || {};
        this.id = region.id || '';
        this.nameRu = region.nameRu || '';
        this.nameEn = region.nameEn || '';
        this.nameKz = region.nameKz || '';
        this.departmentVs = region.departmentVs || [];
        
    }

} 
