// import { MatChipInputEvent } from '@angular/material';

// import { FuseUtils } from '@fuse/utils';

export class Role
{
    id: number;
    userRoles: [];
    name: string;
    normalizedName: string;
    concurrencyStamp: string;
    isUserRole: boolean;
    /**
     *
     */
    constructor(identityRole?) {
        // console.log('user: ', user);
        
        identityRole = identityRole || {};
        this.id = identityRole.id || 0;
        this.userRoles = identityRole.userRoles || [];
        this.name = identityRole.userName || '';
        this.normalizedName = identityRole.email || '';
        this.concurrencyStamp = identityRole.concurrencyStamp || '';
        this.isUserRole = identityRole.isRole || false;
      
    }

} 
