import { Component, OnInit, ViewEncapsulation, ViewChild, ElementRef } from '@angular/core';
import { DataSource } from '@angular/cdk/collections';
import { fuseAnimations } from '@fuse/animations';
import { BehaviorSubject, Observable, merge, Subject, fromEvent } from 'rxjs';
import { map, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { FuseUtils } from '@fuse/utils';
import { MatPaginator, MatSort } from '@angular/material';
import { takeUntil } from 'rxjs/internal/operators';
import { AdminDocConfsService } from './admin-doc-configs.service.service';
import { locale as english } from '../i18n/en';
import { locale as russian } from '../i18n/ru';
import { locale as kazakh } from '../i18n/kz';
import { FuseTranslationLoaderService } from '@fuse/services/translation-loader.service';

@Component({
  selector: 'app-admin-doc-configs',
  templateUrl: './admin-doc-configs.component.html',
  styleUrls: ['./admin-doc-configs.component.scss'],
  animations   : fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class AdminDocConfsComponent implements OnInit {

  dataSource: FilesDataSource | null;
   displayedColumns: string[] = ['id', 'documentType'];
   //  dataSource = ELEMENT_DATA;
 
   @ViewChild(MatPaginator)
   paginator: MatPaginator;
 
   @ViewChild(MatSort)
   sort: MatSort;
 
   @ViewChild('filter')
   filter: ElementRef;
 
   // Private
   private _unsubscribeAll: Subject<any>;

   constructor(private _admindocConfsService: AdminDocConfsService,
    private _fuseTranslationLoaderService: FuseTranslationLoaderService
    ) {
    // Set the private defaults
    this._unsubscribeAll = new Subject();
    this._fuseTranslationLoaderService.loadTranslations(english, kazakh, russian);
  }

  ngOnInit(): void {
    
     
    //   this.dataSource = this._adminUsersService.users;
    
  this.dataSource = new FilesDataSource(this._admindocConfsService, this.paginator, this.sort);
 //  console.log(this.dataSource);
  
  
    fromEvent(this.filter.nativeElement, 'keyup')
        .pipe(
            takeUntil(this._unsubscribeAll),
            debounceTime(150),
            distinctUntilChanged()
        )
        .subscribe(() => {
            if ( !this.dataSource )
            {
                return;
            }
  
            this.dataSource.filter = this.filter.nativeElement.value;
        });
  }

}

export class FilesDataSource extends DataSource<any>
 {
     private _filterChange = new BehaviorSubject('');
     private _filteredDataChange = new BehaviorSubject('');
 
     // /**
     //  * Constructor
     //  *
     //  * @param {EcommerceProductsService} _ecommerceProductsService
     //  * @param {MatPaginator} _matPaginator
     //  * @param {MatSort} _matSort
     //  */
     constructor(
         private _admindocConfsService: AdminDocConfsService,
         private _matPaginator: MatPaginator,
         private _matSort: MatSort
     )
     {
         super();
 
         this.filteredData = this._admindocConfsService.docConfs;
         
         
     }
 
     // /**
     //  * Connect function called by the table to retrieve one stream containing the data to render.
     //  *
     //  * @returns {Observable<any[]>}
     //  */
     connect(): Observable<any[]>
     {
         const displayDataChanges = [
             this._admindocConfsService.onDocConfsChanged,
             this._matPaginator.page,
             this._filterChange,
             this._matSort.sortChange
         ];
 
         return merge(...displayDataChanges)
             .pipe(
                 map(() => {
                         let data = this._admindocConfsService.docConfs.slice();
 
                         data = this.filterData(data);
 
                         this.filteredData = [...data];
 
                         data = this.sortData(data);
 
                         // Grab the page's slice of data.
                         const startIndex = this._matPaginator.pageIndex * this._matPaginator.pageSize;
                         return data.splice(startIndex, this._matPaginator.pageSize);
                     }
                 ));
     }
 
     // -----------------------------------------------------------------------------------------------------
     // @ Accessors
     // -----------------------------------------------------------------------------------------------------
 
     // Filtered data
     get filteredData(): any
     {
         return this._filteredDataChange.value;
     }
 
     set filteredData(value: any)
     {
         this._filteredDataChange.next(value);
     }
 
     // Filter
     get filter(): string
     {
         return this._filterChange.value;
     }
 
     set filter(filter: string)
     {
         this._filterChange.next(filter);
     }
 
     // -----------------------------------------------------------------------------------------------------
     // @ Public methods
     // -----------------------------------------------------------------------------------------------------
 
     // /**
     //  * Filter data
     //  *
     //  * @param data
     //  * @returns {any}
     //  */
     filterData(data): any
     {
         if ( !this.filter )
         {
             return data;
         }
         return FuseUtils.filterArrayByString(data, this.filter);
     }
 
     // /**
     //  * Sort data
     //  *
     //  * @param data
     //  * @returns {any[]}
     //  */
     sortData(data): any[]
     {
         if ( !this._matSort.active || this._matSort.direction === '' )
         {
             return data;
         }
 
         return data.sort((a, b) => {
             let propertyA: number | string = '';
             let propertyB: number | string = '';
 
             switch ( this._matSort.active )
             {
                 case 'id':
                     [propertyA, propertyB] = [a.id, b.id];
                     break;
                 case 'documentType':
                     [propertyA, propertyB] = [a.documentType, b.documentType];
                     break;
             
             }
 
             const valueA = isNaN(+propertyA) ? propertyA : +propertyA;
             const valueB = isNaN(+propertyB) ? propertyB : +propertyB;
 
             return (valueA < valueB ? -1 : 1) * (this._matSort.direction === 'asc' ? 1 : -1);
         });
     }
 
     /**
      * Disconnect
      */
     disconnect(): void
     {
     }
 }
