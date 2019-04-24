import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { WfConfigs } from '../admin-doc-config-vs-accord/wfConfigs.model';

// export interface PeriodicElement {
//   name: string;
//   position: number;
//   weight: number;
//   symbol: string;
// }

export interface DocConfV {
  category: string;
  titleGeneration: string;
  author: string;
  created: string;
  copyDocumentFunction: boolean;
  closeDocumentFunction: boolean;
  createControlCardFunction: boolean;
  documentType: string;
  title: string;
  wfConfigs: WfConfigs;
  contentConfigs: [
      {
         'id': any, 
         'fieldName': string,
         'fieldDisplayName': string,
         'formula': string,
         'group': string,
         'contentType': string
      }
  ];
}



// const ELEMENT_DATA: PeriodicElement[] = [
//   {position: 1, name: 'Hydrogen', weight: 1.0079, symbol: 'H'},
//   {position: 2, name: 'Helium', weight: 4.0026, symbol: 'He'},
//   {position: 3, name: 'Lithium', weight: 6.941, symbol: 'Li'},
//   {position: 4, name: 'Beryllium', weight: 9.0122, symbol: 'Be'},
//   {position: 5, name: 'Boron', weight: 10.811, symbol: 'B'},
//   {position: 6, name: 'Carbon', weight: 12.0107, symbol: 'C'},
//   {position: 7, name: 'Nitrogen', weight: 14.0067, symbol: 'N'},
//   {position: 8, name: 'Oxygen', weight: 15.9994, symbol: 'O'},
//   {position: 9, name: 'Fluorine', weight: 18.9984, symbol: 'F'},
//   {position: 10, name: 'Neon', weight: 20.1797, symbol: 'Ne'},
// ];

@Component({
  selector: 'app-admin-doc-config-vs',
  templateUrl: './admin-doc-config-vs.component.html',
  styleUrls: ['./admin-doc-config-vs.component.scss']
})
export class AdminDocConfVsComponent implements OnInit {

  @Input() docConfVs: DocConfV[];

  // displayedColumns: string[] = ['position', 'name', 'weight', 'symbol'];
  // dataSource = ELEMENT_DATA;

  displayedColumns: string[] = ['created', 'documentType', 'star'];
  dataSource: any;


  constructor() { }

  ngOnInit(): void {
    this.dataSource = this.docConfVs;

  }

  removeDocConfV(id: string): void{
    alert(id);
  }

}
