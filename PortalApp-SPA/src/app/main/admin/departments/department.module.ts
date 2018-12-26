import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


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
    MatMenuModule,
    MatExpansionModule
} from '@angular/material';

import { FuseSharedModule } from '@fuse/shared.module';
import { FuseWidgetModule } from '@fuse/components';
import { AdminDepsComponent } from './admin-deps/admin-deps.component';
import { AdminDepComponent } from './admin-dep/admin-dep.component';
import { AdminDepsService } from './admin-deps/admin-deps.service';
import { AdminDepService } from './admin-dep/admin-dep.service';
import { AdminDepVsComponent } from './admin-dep-vs/admin-dep-vs.component';
import { AdminDepVsAccordComponent } from './admin-dep-vs-accord/admin-dep-vs-accord.component';
import { AdminDepVsTabComponent } from './admin-dep-vs-tab/admin-dep-vs-tab.component';
import { DatePipe, CommonModule } from '@angular/common';
// import { AdminRegionsComponent } from './admin-regions/admin-regions.component';
// import { AdminRegionsService } from './admin-regions/admin-regions.service';
// import { AdminRegionService } from './admin-region/admin-region.service';
// import { AdminRegionComponent } from './admin-region/admin-region.component';

// const routes: Routes = [];

const routes: Routes = [
    {
        path: 'admin-deps',
        component: AdminDepsComponent,
        resolve  : {
          data: AdminDepsService
      }
    },
    {
        path     : 'admin-deps/:id',
        component: AdminDepComponent,
        resolve  : {
            data: AdminDepService
        }
    },
];

@NgModule({
    imports: [
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
        MatMenuModule,
        MatExpansionModule,
        CommonModule
    ],
    declarations: [
        AdminDepsComponent,
        AdminDepComponent,
        AdminDepVsComponent,
        AdminDepVsAccordComponent,
        AdminDepVsTabComponent
        // AdminRegionsComponent,
        // AdminRegionComponent
    ]
})
export class AdminDepartmentModule {}
