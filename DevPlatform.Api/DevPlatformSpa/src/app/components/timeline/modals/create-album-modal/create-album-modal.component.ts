import { Component, OnInit, ViewChild } from '@angular/core';
import { DropzoneComponent, DropzoneDirective, DropzoneConfigInterface } from 'ngx-dropzone-wrapper';
import { ModalService } from '../../../../services/modal.service';


@Component({
  selector: 'app-create-album-modal',
  templateUrl: './create-album-modal.component.html',
  styleUrls: ['./create-album-modal.component.scss']
})
export class CreateAlbumModalComponent implements OnInit {

  public type: string = 'component';

  public disabled: boolean = false;

  public config: DropzoneConfigInterface = {
    clickable: true,
    uploadMultiple: true,
    autoReset: null,
    errorReset: null,
    cancelReset: null,
    autoProcessQueue: true,
    autoQueue: true,
    thumbnailWidth: 1024,
    thumbnailHeight: 768,
    createImageThumbnails: true,
    url: 'https://httpbin.org/post',
    addRemoveLinks: true
  };

  @ViewChild(DropzoneComponent, { static: false }) componentRef?: DropzoneComponent;
  @ViewChild(DropzoneDirective, { static: false }) directiveRef?: DropzoneDirective;

  constructor(private modalService: ModalService
  ) { }


  ngOnInit() {
  }

  files: File[] = [];

  public toggleType(): void {
    this.type = (this.type === 'component') ? 'directive' : 'component';
  }

  public toggleDisabled(): void {
    this.disabled = !this.disabled;
  }

  public toggleAutoReset(): void {
    this.config.autoReset = this.config.autoReset ? null : 5000;
    this.config.errorReset = this.config.errorReset ? null : 5000;
    this.config.cancelReset = this.config.cancelReset ? null : 5000;
  }

  public toggleMultiUpload(): void {
    this.config.maxFiles = this.config.maxFiles ? 0 : 1;
  }

  public toggleClickAction(): void {
    this.config.clickable = !this.config.clickable;
  }

  public resetDropzoneUploads(): void {
    if (this.type === 'directive' && this.directiveRef) {
      this.directiveRef.reset();
    } else if (this.type === 'component' && this.componentRef && this.componentRef.directiveRef) {
      this.componentRef.directiveRef.reset();
    }
  }

  public onUploadInit(event: any): void {
    console.log('onUploadInit:', event);
  }

  public onUploadError(event: any): void {
    console.log('onUploadError:', event);
  }

  public onUploadSuccess(event: any): void {
    this.files.push(event[1].files);
  }

  closeModal(id: string) {
    this.modalService.close(id);
  }

}
