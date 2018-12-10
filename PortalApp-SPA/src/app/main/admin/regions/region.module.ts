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
    MatInputModule
} from '@angular/material';

import { FuseSharedModule } from '@fuse/shared.module';
import { FuseWidgetModule } from '@fuse/components';
import { AdminRegionsComponent } from './admin-regions/admin-regions.component';
import { AdminRegionsService } from './admin-regions/admin-regions.service';



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
        component: AdminRegionsComponent,
        resolve  : {
            data: AdminRegionsService
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
        MatInputModule
    ],
    declarations: [
        AdminRegionsComponent,
        AdminRegionsComponent
    ]
})
export class AdminRegionModule {}
