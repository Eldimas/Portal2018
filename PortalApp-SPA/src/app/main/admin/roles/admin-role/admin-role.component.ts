import { Component, OnInit, ViewEncapsulation, OnDestroy } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { Location } from '@angular/common';
import { FuseUtils } from '@fuse/utils';

import { FormBuilder, FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material';
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { AuthService } from 'app/_services/auth.service';
// import { AdminRegion } from './admin-region.model';
import { Router } from '@angular/router';
import { AdminRole } from './admin-role.model';
import { AdminRoleService } from './admin-role.service';


@Component({
  selector: 'app-admin-role',
  templateUrl: './admin-role.component.html',
  styleUrls: ['./admin-role.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations   : fuseAnimations
})
export class AdminRoleComponent implements OnInit, OnDestroy {

  adminRole: AdminRole;
  regionForm: FormGroup;
  pageType: string;
  adminSelected = false;

  // typesOfShoes: string[] = ['Boots', 'Clogs', 'Loafers', 'Moccasins', 'Sneakers'];
  typesOfAdmin = [];
  typesOfDocument = [];
  typesOfDostupDocuments = [];
  typesOfStat = [];
   // Private
   private _unsubscribeAll: Subject<any>;
   
  constructor(
    private _adminRegionService: AdminRoleService,
    private _formBuilder: FormBuilder,
    private _matSnackBar: MatSnackBar,
    private router: Router,
    private _location: Location,
    private authService: AuthService
    ) { 
      this.adminRole = new AdminRole();
      this._unsubscribeAll = new Subject();
    }

  ngOnInit(): void {
    this._adminRegionService.onRegionChanged
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe(region => {
        if ( region) {
          console.log('region001: ', region);
          
          this.adminRole = new AdminRole(region);
          this.adminSelected =  this.adminRole.id === 2 ? true : false;
          this.typesOfAdmin = [
            {name: 'Редактирование меню', selected: this.adminSelected },
            {name: 'Редактирование пользователей', selected: this.adminSelected  },
            {name: 'Редактирование регионов', selected: this.adminSelected  },
            {name: 'Редактирование ролей', selected: this.adminSelected  },
            {name: 'Редактирование департаментов', selected: this.adminSelected  }
            
          ];
        
          this.typesOfDocument = [
            {name: 'Право регистрировать документ', selected: this.adminSelected  },
            {name: 'Редактирование регистрационного номера', selected: this.adminSelected },
            {name: 'Отклонение документа', selected: this.adminSelected  },
            {name: 'Редактирование маршрута', selected: this.adminSelected  },
            {name: 'Редактирование содержания', selected: this.adminSelected  }
            
          ];

           this.typesOfDostupDocuments = [
            {name: 'СЗ на оплату', selected: this.adminSelected },
            {name: 'Служебная записка', selected: this.adminSelected  },
            {name: 'Совместный Приказ', selected: this.adminSelected  },
            {name: 'Заявление на отпуск', selected: this.adminSelected  },
            {name: 'Заявление на увольнение', selected: this.adminSelected },
            {name: 'Исх. письмо АУП', selected: this.adminSelected  },
            {name: 'Исх. факс ГУП', selected: this.adminSelected  },
            {name: 'Приказ', selected: this.adminSelected  }
            
          ];

          this.typesOfStat = [
            {name: 'Все документы', selected: this.adminSelected },
            {name: 'Контр.карточки', selected: this.adminSelected  },
            {name: 'Служебные записки на оплату', selected: this.adminSelected  },
            {name: 'Авансовые отчёты', selected: this.adminSelected  },
            {name: 'Статистика договоров, счетов, актов', selected: this.adminSelected },
            {name: 'Статистика КПД', selected: this.adminSelected  },
            {name: 'Статистика детализации КПД', selected: this.adminSelected  },
            {name: 'Статистика по входящим письмам', selected: this.adminSelected  }
            
          ];
          this.pageType = 'edit';
        }
        else  {
          this.pageType = 'new';
        }

        this.regionForm = this.createRegionForm();
      });
  }

  ngOnDestroy(): void
    {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    

    createRegionForm(): FormGroup {
      return this._formBuilder.group({
        id: [this.adminRole.id],
        name: [this.adminRole.name]
      });
    }

     /**
     * Save product
     */
    saveRegion(): void
    {
      const data = this.regionForm.getRawValue();
        // data.handle = FuseUtils.handleize(data.id);
        
        // this._adminRegionService.saveRegion(this.authService.decodedToken.nameid, data)
        this._adminRegionService.saveRegion(data.id, data)
            .then(() => {

                // Trigger the subscription with new data
                this._adminRegionService.onRegionChanged.next(data);

                // Show the success message
                this._matSnackBar.open('Region saved', 'OK', {
                    verticalPosition: 'top',
                    duration        : 2000
                });
            });
    }

    /**
     * Add product
     */
    addRegion(): void
    {
        const data = this.regionForm.getRawValue();
        // data.handle = FuseUtils.handleize(data.name);

        this._adminRegionService.addRegion(data)
            .then((region) => {
              // console.log('resp -- ', region);
              
              // Trigger the subscription with new data
               this._adminRegionService.onRegionChanged.next(data);
                
                

                // Show the success message
                this._matSnackBar.open('Region added', 'OK', {
                    verticalPosition: 'top',
                    duration        : 2000
                });

                // this.router.navigate(['/admin/admin-regions/']);
                // Change the location with new one
                // this._location.go('admin/admin-regions/' + this.region.id + '/' + this.product.handle);
                
                this.router.navigate(['/admin/admin-regions/', region.id]);
                // this._location.go('admin/admin-regions/' + response.id);
            });
    }

    /**
     * Add product
     */
    removeRegion(): void
    {
        const data = this.regionForm.getRawValue();
        // data.handle = FuseUtils.handleize(data.name);
        console.log('nameid: ', this.authService.decodedToken.nameid);
        
        // this._adminRegionService.saveRegion(this.authService.decodedToken.nameid, data)
        this._adminRegionService.removeRegion(data.id, data.id)
            .then(() => {

                // Trigger the subscription with new data
                this._adminRegionService.onRegionChanged.next(data);

                // Show the success message
                this._matSnackBar.open('Region deleted', 'OK', {
                    verticalPosition: 'top',
                    duration        : 2000
                });

                this.router.navigate(['/admin/admin-regions/']);
            });
    }

}
