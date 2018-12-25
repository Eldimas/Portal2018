import { Component, OnInit, OnDestroy, ViewEncapsulation } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AuthService } from 'app/_services/auth.service';
import { Subject } from 'rxjs';
import { fuseAnimations } from '@fuse/animations';
@Component({
  selector: 'app-document-config',
  templateUrl: './document-config.component.html',
  styleUrls: ['./document-config.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations   : fuseAnimations
})
export class DocumentConfigComponent implements OnInit, OnDestroy {
  userForm: FormGroup;
  pageType: string;
  
  constructor(
    private _formBuilder: FormBuilder,
    private _matSnackBar: MatSnackBar,
    private authService: AuthService
  ) {
    this._unsubscribeAll = new Subject();
   }

  ngOnInit() {

  }
  ngOnDestroy() {

  }

}
