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
    MatTreeModule,
    MatDialogModule
} from '@angular/material';

import { FuseSharedModule } from '@fuse/shared.module';
import { FuseWidgetModule } from '@fuse/components';
import { AdminMenuComponent } from './admin-menu/admin-menu.component';
import { TranslateModule } from '@ngx-translate/core';
import { EditMenuComponent } from './edit-menu/edit-menu.component';
import { EditMenu2Component } from './edit-menu2/edit-menu2.component';
import { DialogEditItemComponent } from './edit-menu2/dialog-edit-item.component';
import { DialogAddItemComponent } from './edit-menu2/dialog-add-item.component';
// import { AdminUsersService } from './admin-users/admin-users.service';
// import { AdminUsersComponent } from './admin-users/admin-users.component';

// import { AdminUserComponent } from './admin-user/admin-user.component';
// import { AdminUserService } from './admin-user/admin-user.service';

const routes: Routes = [
    {
        path: 'admin-menu',
        component: EditMenu2Component,
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
        MatDialogModule,

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
        MatDialogModule,

        TranslateModule,
        MatTreeModule
    ],
    declarations: [
        AdminMenuComponent,
        EditMenuComponent,
        EditMenu2Component,
        DialogEditItemComponent,
        DialogAddItemComponent
    ],
    entryComponents: [
        DialogEditItemComponent,
        DialogAddItemComponent
    ]
})
export class AdminMenuModule {}
