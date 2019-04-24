import { WfConfigs } from './admin-doc-config-vs-accord/wfConfigs.model';
import { ContentConfigs } from './admin-doc-config-vs-accord/contentConfigs.model';

export class DocumentConfig
{
    id: string;
    category: string;
    titleGeneration: string;
    author: string;
    created: string;
    copyDocumentFunction: boolean;
    closeDocumentFunction: boolean;
    createControlcardFunction: boolean;
    documentConfigId: string;
    /*documentType: string;*/
    title: string;
    wfConfigsSerialized: [WfConfigs];
    contentConfigsSerialized: [ContentConfigs];
    /*
    contentConfigs: [
        {
           'fieldName': string,
           'fieldDisplayName': string,
           'formula': string,
           'group': string,
           'contentType': string
        }
    ]; */

    /**
     *
     */
    constructor(documentConfig?) {
        // console.log('user: ', user);
        
        documentConfig = documentConfig || {};
        this.id = documentConfig.id || '00000000-0000-0000-0000-000000000000';
        this.category = documentConfig.category || '';
        this.titleGeneration = documentConfig.titleGeneration || '';
        this.author = documentConfig.author || '';
        this.created = documentConfig.created || '';
        this.created = documentConfig.created || '';
        this.copyDocumentFunction = documentConfig.copyDocumentFunction || false;
        this.closeDocumentFunction = documentConfig.closeDocumentFunction || false;
        this.createControlcardFunction = documentConfig.createControlcardFunction || false;
        this.documentConfigId = documentConfig.documentConfigId || '';
        /* this.documentType = documentConfig.documentType || ''; */
         this.wfConfigsSerialized = documentConfig.wfConfigs || [];
        this.contentConfigsSerialized = documentConfig.contentConfigsSerialized || [];
    }

} 
