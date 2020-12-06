import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxEditorModule } from 'ngx-editor';
import { FileUploadModule } from 'ng2-file-upload';
import { OrderModule } from 'ngx-order-pipe';


@NgModule({
    imports: [
        CommonModule
    ],
    exports: [
        CommonModule,
        ReactiveFormsModule,
        FormsModule,
        NgxEditorModule,
        FileUploadModule,
        OrderModule
    ],
    declarations: [
    ]
})
export class SharedModule { }