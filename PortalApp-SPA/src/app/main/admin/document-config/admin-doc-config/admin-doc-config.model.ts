import { MatChipInputEvent } from '@angular/material';

import { FuseUtils } from '@fuse/utils';
import { WfConfigs } from '../admin-doc-config-vs-accord/wfConfigs.model';
import { ContentConfigs } from '../admin-doc-config-vs-accord/contentConfigs.model';

export class AdminDocConf
{
    id: string;
    documentType: string;
    // nameEn: string;
    // nameKz: string;
    documentConfigVs: [
        {
            id: string;
            category: string;
            titleGeneration: string;
            author: string;
            created: string;
            copyDocumentFunction: boolean;
            closeDocumentFunction: boolean;
            createControlcardFunction: boolean;
            title: string;
            documentConfigId: string;
            wfConfigsSerialized: [WfConfigs];
            contentConfigsSerialized: [ContentConfigs];
            /*  
            contentConfigs: [
                {
                   'id': any, 
                   'fieldName': string,
                   'fieldDisplayName': string,
                   'formula': string,
                   'group': string,
                   'contentType': string
                }
            ] */
        }
 ];
    

    /**
     *
     */
    constructor(conf?) {
        // console.log('user: ', user);
        
        conf = conf || {};
        this.id = conf.id || '00000000-0000-0000-0000-000000000000';
        this.documentType = conf.documentType || '';
        this.documentConfigVs = conf.documentConfigVs || [];
        
    }

} 
