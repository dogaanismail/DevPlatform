import { AlbumService } from './../../../../services/album/album.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { DropzoneComponent, DropzoneDirective, DropzoneConfigInterface } from 'ngx-dropzone-wrapper';
import { ModalService } from '../../../../services/modal/modal.service';
import { AlbumCreate } from 'src/app/models/album/albumCreate';

@Component({
  selector: 'app-create-album-modal',
  templateUrl: './create-album-modal.component.html',
  styleUrls: ['./create-album-modal.component.scss']
})
export class CreateAlbumModalComponent implements OnInit {

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

  constructor(private modalService: ModalService,
    private albumService: AlbumService
  ) { }

  public disabled: boolean = false;
  public type: string = 'component';
  albumCreate: any = {};
  files: File[] = [];
  album: AlbumCreate;

  ngOnInit() {

  }

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
    this.files.push(event[0] as File);
  }

  closeModal(id: string) {
    this.modalService.close(id);
  }

  saveAlbum() {
    var formData: any = new FormData();
    formData.append('name', this.albumCreate.name);
    formData.append('place', this.albumCreate.place);
    formData.append('date', this.albumCreate.date);
    formData.append('tag', this.albumCreate.tag);

    if (this.files.length > 0) {
      for (let index = 0; index < this.files.length; index++) {
        formData.append('images', this.files[index], this.files[index].name);
      }
    }

    this.albumService.createAlbum(formData).subscribe((data: any) => {
      //TODO: must be implemented
    });
  }

}
