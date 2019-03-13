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
    MatCardModule,
    MatMenuModule
} from '@angular/material';

import { FuseSharedModule } from '@fuse/shared.module';
import { FuseWidgetModule } from '@fuse/components';
import { AdminRegionsComponent } from './admin-regions/admin-regions.component';
import { AdminRegionsService } from './admin-regions/admin-regions.service';
import { AdminRegionService } from './admin-region/admin-region.service';
import { AdminRegionComponent } from './admin-region/admin-region.component';



const routes: Routes = [
    {
        path: 'admin-regions',
        component: AdminRegionsComponent,
        resolve  : {
          data: AdminRegionsService
      }
    },
    {
        path     : 'admin-regions/:id',
        component: AdminRegionComponent,
        resolve  : {
            data: AdminRegionService
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
        MatCardModule,
        MatMenuModule
    ],
    declarations: [
        AdminRegionsComponent,
        AdminRegionComponent
    ]
})
export class AdminRegionModule {}
