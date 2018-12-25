import { MatChipInputEvent } from '@angular/material';

import { FuseUtils } from '@fuse/utils';

export class AdminDepV
{
    id: string;
    name: string;
    created: string;
    departmentId: string;
    disabled: boolean;
    displayNameEng: string;
    displayNameKaz: string;
    displayNameRus: string;
    fromNameEng: string;
    fromNameKaz: string;
    fromNameRus: string;
    priority: number;
    regionId: string;
    shortName: string;
    toNameEng: string;
    toNameKaz: string;
    toNameRus: string;
    
    

    /**
     *
     */
    constructor(depv?) {
        // console.log('user: ', user);
        
        depv = depv || {};
        this.id = depv.id || '';
        this.name = depv.name || '';
        this.created = depv.created || '';
        this.departmentId = depv.departmentId || '';
        this.disabled = depv.disabled || false;
        this.displayNameEng = depv.displayNameEng || '';
        this.displayNameKaz = depv.isplayNameKaz || '';
        this.displayNameRus = depv.displayNameRus || '';
        this.fromNameEng = depv.fromNameEng || '';
        this.fromNameKaz = depv.fromNameKaz || '';
        this.fromNameRus = depv.fromNameRus || '';
        this.priority = depv.priority || 0;
        this.regionId = depv.regionId || '';
        this.shortName = depv.shortName || '';
        this.toNameEng = depv.toNameEng || '';
        this.toNameKaz = depv.toNameKaz || '';
        this.toNameRus = depv.toNameRus || '';
       
        
    }

} 
