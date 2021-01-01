import { GenericModalComponent } from './../components/generic-modal/generic-modal.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxEditorModule } from 'ngx-editor';
import { FileUploadModule } from 'ng2-file-upload';
import { OrderModule } from 'ngx-order-pipe';
import { NgxDropzoneModule } from 'ngx-dropzone';


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
        OrderModule,
        GenericModalComponent,
        NgxDropzoneModule
    ],
    declarations: [
        GenericModalComponent
    ]
})
export class SharedModule { }