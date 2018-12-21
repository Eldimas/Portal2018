import { MatChipInputEvent } from '@angular/material';

import { FuseUtils } from '@fuse/utils';

export class AdminUser
{
    id: number;
    userName: string;
    email: string;
    phoneNumber: string;
    created: string;
    lastActive: string;
    userRoles: [];
    userVs: [];
    // vCreated: string;
    // vDisplayNameRus: string;
    // vDisplayNameEng: string;
    // vDisplayNameKaz: string;
    // vFromNameRus: string;
    // vFromNameEng: string;
    // vFromNameKaz: string;
    // vToNameRus: string;
    // vToNameEng: string;
    // vToNameKaz: string;
    // vPriority: number;
    // vDisabled: boolean;


    /**
     *
     */
    constructor(user?) {
        // console.log('user: ', user);
        
        user = user || {};
        this.id = user.id || 0;
        this.userName = user.userName || '';
        this.email = user.email || '';
        this.phoneNumber = user.phoneNumber || '';
        this.created = user.created || '';
        this.lastActive = user.lastActive || '';
        this.userVs = user.userVs || [];
        this.userRoles = user.userRoles || [];
        // this.vCreated = user.vCreated || '';
        // this.vDisplayNameRus = user.vDisplayNameRus || '';
        // this.vDisplayNameEng = user.vDisplayNameEng || '';
        // this.vDisplayNameKaz = user.vDisplayNameKaz || '';
        // this.vFromNameRus = user.vFromNameRus || '';
        // this.vFromNameEng = user.vFromNameEng || '';
        // this.vFromNameKaz = user.vFromNameKaz || '';
        // this.vToNameRus = user.vToNameRus || '';
        // this.vToNameEng = user.vToNameEng || '';
        // this.vToNameKaz = user.vToNameKaz || '';
        // this.vPriority = user.vPriority || 0;
        // this.vDisabled = user.vDisabled || false;
      
    }

} 
