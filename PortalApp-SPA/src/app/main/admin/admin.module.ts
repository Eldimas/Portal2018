import { NgModule } from '@angular/core';
import { AdminUsersModule } from './users/users.module';
import { AdminRegionModule } from './regions/region.module';


@NgModule({
  imports: [
    AdminUsersModule,
    AdminRegionModule
  ]
})
export class AdminModule { }
