import { NgModule } from '@angular/core';
import { AdminUsersModule } from './users/users.module';
import { AdminRegionModule } from './regions/region.module';
import { AdminDepartmentModule } from './departments/department.module';
import { AdminMenuModule } from './menu/menu.module';
import { AdminRolesComponent } from './roles/admin-roles/admin-roles.component';
import { AdminRolesModule } from './roles/roles.module';


@NgModule({
  imports: [
    AdminUsersModule,
    AdminRegionModule,
    AdminRolesModule,
    AdminDepartmentModule,
    AdminMenuModule
  ]
})
export class AdminModule { }
