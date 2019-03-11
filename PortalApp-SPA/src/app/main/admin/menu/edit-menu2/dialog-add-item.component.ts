import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material';
import { Navig } from 'app/_models/navig';
import { NavigUpdate } from 'app/_models/navigUpdate.model';

@Component({
    selector: 'app-dialog-add-item',
    template: `
        <form
            #f="ngForm"
            style="min-width: 300px;"
            fxLayout="column"
            fxLayoutAlign="center center"
        >
            <h2 mat-dialog-title>Добавить к "{{ parentNode.title }}"</h2>
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
                    <mat-select style="width: 180px;" placeholder="Your type" 
                    [(ngModel)]="navigItem.type" name="type" required>
                        <mat-option value="item">Item</mat-option>
                        <mat-option value="group">Group</mat-option>
                        <mat-option value="collapsable">Collapsable</mat-option>
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
                <button
                    [disabled]="f.invalid"
                    mat-raised-button
                    [mat-dialog-close]="navigItem"
                >
                    Yes
                </button>
                <button mat-raised-button [mat-dialog-close]="false">No</button>
            </mat-dialog-actions>
        </form>
    `
})
export class DialogAddItemComponent implements OnInit {
    navigItem = new NavigUpdate();
    parentNode: any;
    constructor(@Inject(MAT_DIALOG_DATA) private passedData: any) {}

    ngOnInit(): void {
        this.parentNode = this.passedData;
    }
}
