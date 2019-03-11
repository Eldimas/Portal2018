import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material';

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
    navigItem: any;
    delete = 'delete';
    constructor(@Inject(MAT_DIALOG_DATA) private passedData: any) {}

    ngOnInit(): void {
        this.navigItem = this.passedData;
    }
}
