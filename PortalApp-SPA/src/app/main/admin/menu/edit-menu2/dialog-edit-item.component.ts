import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog } from '@angular/material';
import {FormControl} from '@angular/forms';
import { Role } from 'app/_models/role.model';
import { createHttpObservable } from 'app/utils/util';
import { environment } from 'environments/environment';
import { noop } from 'rxjs';

@Component({
    selector: 'app-dialog-edit-item',
    template: `
        <form #f="ngForm" style="min-width: 300px;">
            <div fxLayout="row" fxLayoutAlign="end">
                <button mat-mini-fab [mat-dialog-close]="delete">
                    <mat-icon>delete</mat-icon>
                </button>
            </div>
            <div fxLayout="column" fxLayoutAlign="center center">
                <h2 mat-dialog-title>{{ navigItem.title }}</h2>
                <mat-dialog-content
                    style="margin-left: 10px; margin-right: 10px;"
                    fxLayout="column"
                    fxLayoutAlign="center center"
                >
                    <mat-form-field>
                        <input
                            matInput
                            [(ngModel)]="navigItem.title"
                            placeholder="Your title"
                            name="title"
                            required
                        />
                    </mat-form-field>
                    <mat-form-field>
                        <input
                            matInput
                            [(ngModel)]="navigItem.titleEng"
                            placeholder="Your titleEng"
                            name="titleEng"
                            required
                        />
                    </mat-form-field>
                    <mat-form-field>
                        <input
                            matInput
                            [(ngModel)]="navigItem.titleKaz"
                            placeholder="Your titleKaz"
                            name="titleKaz"
                            required
                        />
                    </mat-form-field>
                    <mat-form-field>
                        <mat-select
                            style="width: 180px;"
                            placeholder="Your type"
                            [(ngModel)]="navigItem.type"
                            name="type"
                            required
                        >
                            <mat-option value="item">Item</mat-option>
                            <mat-option value="group">Group</mat-option>
                            <mat-option value="collapsable"
                                >Collapsable</mat-option
                            >
                        </mat-select>
                    </mat-form-field>
                    <mat-form-field>
                        <input
                            matInput
                            [(ngModel)]="navigItem.icon"
                            placeholder="Your icon"
                            name="icon"
                            required
                        />
                    </mat-form-field>
                    <mat-form-field>
                        <input
                            matInput
                            [(ngModel)]="navigItem.url"
                            placeholder="Your url"
                            name="url"
                            required
                        />
                    </mat-form-field>

                    <mat-form-field>
                        <mat-select
                            placeholder="Roles"
                            [formControl]="roles"
                            [(ngModel)]="navigItem.roles"
                            name="roles"
                            multiple
                        >
                            <mat-option
                                *ngFor="let role of roleNewList"
                                [value]="role"
                                >{{ role }}</mat-option
                            >
                        </mat-select>
                    </mat-form-field>
                </mat-dialog-content>
                <mat-dialog-actions>
                    <button mat-raised-button [mat-dialog-close]="navigItem">
                        Сохранить
                    </button>
                    <button mat-raised-button [mat-dialog-close]="false">
                        Отменить
                    </button>
                </mat-dialog-actions>
            </div>
        </form>
    `
})
export class DialogEditItemComponent implements OnInit {

    roles = new FormControl();
    // roleList: string[] = ['Extra cheese', 'Mushroom', 'Onion', 'Pepperoni', 'Sausage', 'Tomato'];
    roleNewList: string[] = [];

    baseUrl = environment.apiUrl;
    allRoles: Role[];
    

    navigItem: any;
    delete = 'delete';
    constructor(@Inject(MAT_DIALOG_DATA) private passedData: any, private editDialog: MatDialog) {}

    ngOnInit(): void {
        this.navigItem = this.passedData;
        this.navigItem.roles = '';

        const http$ = createHttpObservable(this.baseUrl + 'role/getAllRoles');
        http$.subscribe(
            roles => {
              console.log(roles);
              this.allRoles = roles;
                this.allRoles.forEach(role => {
                    this.roleNewList.push(role.name);
                });

            //   this.allRoles.forEach(role => {
            //     (this.adminUser.userRoles).forEach(aUser => {
            //       if (role.name.toUpperCase() === aUser.role.name.toUpperCase() ){
            //         console.log('aUser', aUser);
            //         role.isUserRole = true;
    
            //       }
            //     });
            //   });
            },
            noop,
            () => console.log('completed')
          );
    }
}
