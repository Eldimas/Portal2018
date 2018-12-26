// import { MatChipInputEvent } from '@angular/material';

// import { FuseUtils } from '@fuse/utils';

export class RegionForSelection
{
    id: string;
    nameRu: string;
    nameEn: string;
    nameKz: string;
    /**
     *
     */
    constructor(reg?) {
        // console.log('user: ', user);
        
        reg = reg || {};
        this.id = reg.id || 0;
        this.nameRu = reg.nameRu || '';
        this.nameEn = reg.nameEn || '';
        this.nameKz = reg.nameKz || '';
      
    }

} 
