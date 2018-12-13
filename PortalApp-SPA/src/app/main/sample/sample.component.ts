import { Component } from '@angular/core';

import { FuseTranslationLoaderService } from '@fuse/services/translation-loader.service';

import { locale as english } from './i18n/en';
import { locale as kazakh } from './i18n/kz';
import { locale as russian } from './i18n/ru';

@Component({
    selector   : 'sample',
    templateUrl: './sample.component.html',
    styleUrls  : ['./sample.component.scss']
})
export class SampleComponent
{
    // /**
    //  * Constructor
    //  *
    //  * @param {FuseTranslationLoaderService} _fuseTranslationLoaderService
    //  */
    constructor(
        private _fuseTranslationLoaderService: FuseTranslationLoaderService
    )
    {
        this._fuseTranslationLoaderService.loadTranslations(english, kazakh, russian);
    }
}
