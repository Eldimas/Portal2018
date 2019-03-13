import { MatChipInputEvent } from '@angular/material';

import { FuseUtils } from '@fuse/utils';
import { GridAutoDirective } from '@angular/flex-layout/grid/typings/auto/auto';
import * as uuid from 'uuid';

export class AdminUserV
{
    id: string;
    created: string;
    displayNameRus: string;
    displayNameEng: string;
    displayNameKaz: string;
    fromNameRus: string;
    fromNameEng: string;
    fromNameKaz: string;
    toNameRus: string;
    toNameEng: string;
    toNameKaz: string;
    priority: number;
    disabled: boolean;
    userId: number;
    departmentVId: string;

    /**
     *
     */
    constructor(user?) {
        // console.log('user: ', user);
        const d = new Date();
        const n = d.toString();

        user = user || {};
        this.id = user.id || uuid.v4();
        
        this.created = user.created || n;
        this.displayNameRus = user.displayNameRus || '';
        this.displayNameEng = user.displayNameEng || '';
        this.displayNameKaz = user.displayNameKaz || '';
        this.fromNameRus = user.fromNameRus || '';
        this.fromNameEng = user.fromNameEng || '';
        this.fromNameKaz = user.fromNameKaz || '';
        this.toNameRus = user.toNameRus || '';
        this.toNameEng = user.toNameEng || '';
        this.toNameKaz = user.toNameKaz || '';
        this.priority = user.priority || 0;
        this.disabled = user.disabled || false;
        this.userId = user.userId || 0;
        this.departmentVId = user.departmentVId ||  '';
      
    }

} 
