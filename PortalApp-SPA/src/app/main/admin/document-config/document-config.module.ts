import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import {
  MatButtonModule,
  MatIconModule,
  MatTableModule,
  MatPaginatorModule,
  MatRippleModule,
  MatSelectModule,
  MatSortModule,
  MatSnackBarModule,
  MatTabsModule,
  MatFormFieldModule,
  MatInputModule,
  MatExpansionModule,
  MatMenuModule,
  MatSlideToggleModule,
  MatCheckboxModule,
  MatChipsModule,
  MatAutocompleteModule
} from '@angular/material';
import { FuseSharedModule } from '@fuse/shared.module';
import { FuseWidgetModule } from '@fuse/components';
import { AdminDocConfsComponent } from './admin-doc-configs/admin-doc-configs.component';
import { AdminDocConfComponent } from './admin-doc-config/admin-doc-config.component';
import { AdminDocConfService } from './admin-doc-config/admin-doc-config.service.service';
import { AdminDocConfVsComponent } from './admin-doc-config-vs/admin-doc-config-vs.component';
import { AdminDocConfVsAccordComponent } from './admin-doc-config-vs-accord/admin-doc-config-vs-accord.component';
import { AdminDocConfsService } from './admin-doc-configs/admin-doc-configs.service.service';
import { TranslateModule } from '@ngx-translate/core';



const routes: Routes = [
  {
      path: 'document-config',
      component: AdminDocConfsComponent,
       resolve  : {
        data: AdminDocConfsService
    }
  },
    {
      path     : 'document-config/:id',
      component: AdminDocConfComponent,
       resolve  : {
          data: AdminDocConfService
      }
  }, 
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatPaginatorModule,
    MatRippleModule,
    MatSelectModule,
    MatSortModule,
    FuseSharedModule,
    FuseWidgetModule,
    MatSnackBarModule,
    MatTabsModule,
    MatFormFieldModule,
    MatInputModule,
    MatExpansionModule,
    MatMenuModule,
    HttpClientModule,
    MatCheckboxModule,
    MatSlideToggleModule,
    MatChipsModule,
    MatAutocompleteModule,
    TranslateModule
  ],
  declarations: [AdminDocConfComponent, AdminDocConfVsComponent, AdminDocConfsComponent, AdminDocConfVsAccordComponent]
})
export class DocumentConfigModule { }
