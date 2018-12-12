import { NgModule } from '@angular/core';
import { AdminUsersModule } from './users/users.module';
import { AdminRegionModule } from './regions/region.module';
import { AdminDepartmentModule } from './departments/department.module';


@NgModule({
  imports: [
    AdminUsersModule,
    AdminRegionModule,
    AdminDepartmentModule
  ]
})
export class AdminModule { }
