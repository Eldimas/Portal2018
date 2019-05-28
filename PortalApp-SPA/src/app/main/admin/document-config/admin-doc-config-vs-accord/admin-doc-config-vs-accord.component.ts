import { Component, OnInit, Input, ElementRef, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, FormArray } from '@angular/forms';
import {COMMA, ENTER} from '@angular/cdk/keycodes';
import { AdminDocConfVsService } from './admin-doc-config-vs-accord.service.service';
import { MatSnackBar, MatAutocompleteSelectedEvent, MatChipInputEvent, MatAutocomplete, MatChip  } from '@angular/material';
import { DocumentConfig } from '../document-config.model';
import { AdminDocConf } from '../admin-doc-config/admin-doc-config.model';
import { Observable } from 'rxjs';
import { startWith, map } from 'rxjs/internal/operators';
import { WfConfigs } from './wfConfigs.model';
import { ContentConfigs } from './contentConfigs.model';
import { locale as english } from '../i18n/en';
import { locale as russian } from '../i18n/ru';
import { locale as kazakh } from '../i18n/kz';
import { FuseTranslationLoaderService } from '@fuse/services/translation-loader.service';

// export interface DocConfV {
//   id: string;
//   created: string;
//   name: string;
//   shortName: string;
//   displayNameRus: string;
//   displayNameEng: string;
//   displayNameKaz: string;
//   fromNameRus: string;
//   fromNameEng: string;
//   fromNameKaz: string;
//   toNameRus: string;
//   toNameEng: string;
//   toNameKaz: string;
//   priority: number;
//   disabled: boolean;
//   regionId: string;
//   // users: [];
//   docConfId: string;
// }

@Component({
  selector: 'app-admin-doc-config-vs-accord',
  templateUrl: './admin-doc-config-vs-accord.component.html',
  styleUrls: ['./admin-doc-config-vs-accord.component.scss']
})
export class AdminDocConfVsAccordComponent implements OnInit {
  // @Input() indexOfDocConfV: number;
  @Input() docConfV: DocumentConfig;
  @Input() docConf: AdminDocConf;
  // @Input() wfConfigs: WfConfigs;
  docConfVForm: FormGroup;
  route: FormGroup;
  visible = true;
  selectable = true;
  removable = true;
  addOnBlur = true;
  contentConfigs: ContentConfigs;
  separatorKeysCodes: number[] = [ENTER, COMMA];
  userCtrl = new FormControl();
  filteredUsers: Observable<string[]>;
  wfConfig: WfConfigs[] = [{priority: 3, processType: 'Sender', computed: '', editable: true, hint: '', required: false, ous: 
  ['Abulkhair Zhamiyev']}];
  users: string[] = ['spadm'];
  allUsers: string[] = [ 'spadm', 'Abulkhair Zhamiyev', 'Anvar Mukhamedgaliyev', 'Gulmira Kaliullina', 'Eldar Absalyamov'];
  processTypes: string[] = ['Approval', 'TopApproval', 'FormatApproval', 'Copy', 'Sender', 'Reciever',
                            'Registration', 'Assignment', 'Action', 'Supervisor', 'Consulting', 'Informed', 'Any',
                            'Executor', 'Control', 'Coexecutor', 'ForInformation', 'CopyForRegistration'];
  contentTypes: string[] = ['String', 'HTML', 'Boolean', 'Number', 'Table', 'Date', 'List', 'RegNumberControl'];
  panelOpenState = false;
  ousForCopy = [];
  computedForCopy = [];
  editableForCopy = [];
  hintForCopy = [];
  requiredForCopy = [];
  processTypeForCopy = [];
  priorityForCopy = [];


  @ViewChild('userInput') userInput: ElementRef<HTMLInputElement>;
  @ViewChild('auto') matAutocomplete: MatAutocomplete;
  @ViewChild('chipList') matChip: MatChip;
  constructor(
    private _formBuilder: FormBuilder, 
    private _matSnackBar: MatSnackBar,
    private _admindocConfVsService: AdminDocConfVsService,
    private _fuseTranslationLoaderService: FuseTranslationLoaderService
  ) { 
    this.filteredUsers = this.userCtrl.valueChanges.pipe(
      startWith(null),
      map((user: string | null) => user ? this._filter(user) : this.allUsers.slice()));
      this._fuseTranslationLoaderService.loadTranslations(english, kazakh, russian);
      
  }

  ngOnInit(): void {
    this.representWfConfigs();
    this.docConfVForm = this.createDocConfForm();
    console.log(this.docConfVForm.get('wfConfigsSerialized'));
    console.log(this.docConfVForm);
    
    // this.route = this.createRoute();
    // this.wfConfigs = this.createRoute();
  }
  add(event: MatChipInputEvent, index): void {
    if (!this.matAutocomplete.isOpen) {
      const input = event.input;
      const value = event.value;

      // Add our fruit
      if ((value || '').trim()) {
        this.docConfVForm.get('wfConfigsSerialized')['controls'][index]['controls']['ous'].setValue(event.value.trim());
      }

      // Reset the input value
      if (input) {
        input.value = '';
      }
    }
  }

  remove(user: string, ous: any): void {
    const index = this.users.indexOf(user);
    const arr = [];
    arr.push(ous);
    if (index >= 0) {
      arr.splice(index, 1);
    }
  }

  selected(event: MatAutocompleteSelectedEvent): void {
    this.users.push(event.option.viewValue);
    this.userInput.nativeElement.value = '';
    this.userCtrl.setValue(null);
  }
  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.allUsers.filter(user => user.toLowerCase().indexOf(filterValue) === 0);
  }

  saveDocConfV(): void {
    
    const data = this.docConfVForm.getRawValue();
    console.log('data', data);
    console.log('data.id: ', data.id);
    console.log('docConf: ', this.docConf);
    data.documentConfigId = this.docConf.id;
    this._admindocConfVsService.saveDocConfV(data.id, data)
      .then(() => {
        // Trigger the subscription with new data
        this._admindocConfVsService.onDocConfVChanged.next(data);
          // Show the success message
          this._matSnackBar.open('Version of document config was successfully saved', 'OK', {
            verticalPosition: 'top',
            duration        : 2000
        });
        this.panelOpenState = false;
        this.docConfVForm.markAsPristine();
      });
      
  }

  addDocConfV(): void {
    const data = this.docConfVForm.getRawValue();
    data.documentConfigId = this.docConf.id;
    console.log('data', data);
    console.log('data.id: ', data.documentConfigId);

    this._admindocConfVsService.addDocConfV(data)
    .then((dep) => {
      // console.log('resp -- ', region);
      
      // Trigger the subscription with new data
       this._admindocConfVsService.onDocConfVChanged.next(data);
        
        

        // Show the success message
        this._matSnackBar.open('DocConfV added', 'OK', {
            verticalPosition: 'top',
            duration        : 2000
        });

    });


      
  }

/*   setForm(): void {
    this.docConfV.wfConfigsSerialized.forEach(x => {
      (this.docConfVForm.controls.wfConfigsSerialized as FormArray).push(
        this._fb.group({
          computed: x.computed,
          editable: x.editable,
          hint: x.hint,
          primary: x.primary,
          thumbs: this._fb.group({
            default: x.thumbs.default
          })        
        })
      );
    });
  } */
  get wfConfigsArray(): FormArray {
    return this.docConfVForm.get('wfConfigsSerialized') as FormArray;
  }

  get contentConfigsArray(): FormArray {
    return this.docConfVForm.get('contentConfigsSerialized') as FormArray;
  }

  addRoute(): void {
    const wf = this._formBuilder.group(new WfConfigs);
    this.wfConfigsArray.push(wf);
  }

  ConvertToJSON(wf: any): any[] {
    wf.map(item =>  {
      return {
        id: item.id,
        name: item.name
      };
    });
    return wf;
}

objToArray(wf: any): any[] {
  const ous = [];
  ous.push(wf);
  return ous;
}

showInConsole(var1: any): void {
  console.log(var1);
}

removeFromFormControl(user: string, index: any): void{
  this.docConfVForm.get('wfConfigsSerialized')['controls'][index]['controls']['ous'].setValue({id: '', name: ''});
  const var1 = this.docConfVForm.get('wfConfigsSerialized')['controls'][index]['controls']['ous'];
  console.log(var1);
}

  representWfConfigs(): void {
    this.docConfV.wfConfigsSerialized.forEach(element => {
      this.ousForCopy.push(element.ous);
      this.computedForCopy.push(element.computed);
      this.editableForCopy.push(element.editable);
      this.requiredForCopy.push(element.required);
      this.priorityForCopy.push(element.priority);
      this.processTypeForCopy.push(element.processType);
      this.hintForCopy.push(element.hint);

    });
  }

  createOus(arrIn: string[]): FormArray {
    const arr = this._formBuilder.array([]);
    let formGroup: FormGroup;
    arrIn.forEach((element, index) => {
      formGroup = this._formBuilder.group({
        [index]: element
      });
      arr.push(formGroup);
    });
    return arr;
  }

  createRoute(): FormArray {
    const d = this._formBuilder.array([]);
    let t: FormGroup;
    this.docConfV.wfConfigsSerialized.forEach((element) => {
      t = this._formBuilder.group({
          ous: this.objToArray(element.ous),
          computed: element.computed,
          editable: element.editable,
          hint: element.hint,
          priority: element.priority,
          processType: element.processType,
          required: element.required
      });
      d.push(t);
    });
    return d;
  }
  createContentConfigs(): FormArray {
    const d = this._formBuilder.array([]);
    let t: FormGroup;
    this.docConfV.contentConfigsSerialized.forEach((element) => {
      t = this._formBuilder.group({
        fieldName: element.fieldName,
        fieldDisplayName: element.fieldDisplayName,
        contentType: element.contentType,
        formula: element.formula,
        group: element.group
      });
      d.push(t);
    });
    return d;
  }

  createNewRoute(): void {
    const t = this._formBuilder.group({
      ous: ['Abulkhair Zhamiyev'],
      computed: '',
      editable: true,
      hint: '',
      priority: 1,
      processType: 4,
      required: false
    });
    this.wfConfigsArray.push(t);
  }

  createNewContentConfig(): void {
    const t = this._formBuilder.group({
      fieldName: 'none',
      fieldDisplayName: 'none',
      contentType: 'String',
      formula: '',
      group: ''
    });
    this.contentConfigsArray.push(t);
  }

  deleteRoute(index: number): void {
    this.wfConfigsArray.removeAt(index);
  }
  deleteContentConfigs(index: number): void {
    this.contentConfigsArray.removeAt(index);
  }

  createDocConfForm(): FormGroup {
    return this._formBuilder.group({
      id: [this.docConfV.id],
      category: [this.docConfV.category],
      titleGeneration: [this.docConfV.titleGeneration],
      author: [this.docConfV.author],
      created: [this.docConfV.created],
      copyDocumentFunction: [this.docConfV.copyDocumentFunction],
      closeDocumentFunction: [this.docConfV.closeDocumentFunction],
      createControlCardFunction: [this.docConfV.createControlcardFunction],
      /* documentType: [this.docConfV.documentType], */
      title: [this.docConfV.title],
      wfConfigsSerialized: this.createRoute(),
      contentConfigsSerialized: this.createContentConfigs()
       /*
      contentConfigs: [this.docConfV.contentConfigs], */
      
    });
  }

}
