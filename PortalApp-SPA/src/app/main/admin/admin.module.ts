import { NgModule } from '@angular/core';
import { AdminUsersModule } from './users/users.module';
import { AdminRegionModule } from './regions/region.module';
import { AdminDepartmentModule } from './departments/department.module';
import { AdminMenuModule } from './menu/menu.module';


@NgModule({
  imports: [
    AdminUsersModule,
    AdminRegionModule,
    AdminDepartmentModule,
    AdminMenuModule
  ]
})
export class AdminModule { }
