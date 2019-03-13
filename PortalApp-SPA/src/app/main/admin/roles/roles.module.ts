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
    MatListModule
} from '@angular/material';

import { FuseSharedModule } from '@fuse/shared.module';
import { FuseWidgetModule } from '@fuse/components';
import { AdminRolesComponent } from './admin-roles/admin-roles.component';
import { AdminRolesService } from './admin-roles/admin-roles.service';
import { AdminRegionsService } from './admin-roles/admin-regions.service';
import { AdminRoleComponent } from './admin-role/admin-role.component';
import { AdminRoleService } from './admin-role/admin-role.service';

// import { AdminRegionsComponent } from './admin-regions/admin-regions.component';
// import { AdminRegionsService } from './admin-regions/admin-regions.service';
// import { AdminRegionService } from './admin-region/admin-region.service';
// import { AdminRegionComponent } from './admin-region/admin-region.component';



const routes: Routes = [
    {
        path: 'admin-roles',
        component: AdminRolesComponent,
        resolve  : {
          data: AdminRolesService
      }
    },
    {
        path     : 'admin-role/:id',
        component: AdminRoleComponent,
        resolve  : {
            data: AdminRoleService
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
        MatListModule
    ],
    declarations: [
        AdminRolesComponent,
        AdminRoleComponent
    ]
})
export class AdminRolesModule {}
