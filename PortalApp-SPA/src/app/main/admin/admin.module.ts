import { NgModule } from '@angular/core';
import { AdminUsersModule } from './users/users.module';
import { AdminRegionModule } from './regions/region.module';
import { AdminDepartmentModule } from './departments/department.module';
import { DocumentConfigModule } from './document-config/document-config.module';


@NgModule({
  imports: [
    AdminUsersModule,
    AdminRegionModule,
    AdminDepartmentModule,
    DocumentConfigModule
  ]
})
export class AdminModule { }
