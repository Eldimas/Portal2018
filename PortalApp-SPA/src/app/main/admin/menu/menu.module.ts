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
    MatCheckboxModule,
    MatTreeModule
} from '@angular/material';

import { FuseSharedModule } from '@fuse/shared.module';
import { FuseWidgetModule } from '@fuse/components';
import { AdminMenuComponent } from './admin-menu/admin-menu.component';
import { TranslateModule } from '@ngx-translate/core';
// import { AdminUsersService } from './admin-users/admin-users.service';
// import { AdminUsersComponent } from './admin-users/admin-users.component';

// import { AdminUserComponent } from './admin-user/admin-user.component';
// import { AdminUserService } from './admin-user/admin-user.service';

const routes: Routes = [
    {
        path: 'admin-menu',
        component: AdminMenuComponent,
    //     resolve  : {
    //       data: AdminUsersService
    //   }
    }
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
        MatCheckboxModule,

        TranslateModule,
        MatTreeModule

    ],
    exports: [
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
        MatCheckboxModule,

        TranslateModule,
        MatTreeModule
    ],
    declarations: [
        AdminMenuComponent
    ]
})
export class AdminMenuModule {}
