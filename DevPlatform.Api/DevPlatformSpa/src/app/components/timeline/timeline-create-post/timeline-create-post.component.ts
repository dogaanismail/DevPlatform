import { Component, OnInit, ViewChild } from '@angular/core';
import { FileUploader } from "ng2-file-upload";

/* Services */
import { ModalService } from '../../../services/modal/modal.service';
import { AuthService } from '../../../services/user/auth/auth.service';
import { NgxSpinnerService } from 'ngx-spinner';

/* NgRx */
import { Store, select } from "@ngrx/store";
import * as fromPost from "../../../core/ngrx/selectors/post.selectors";
import * as postActions from "../../../core/ngrx/actions/post.actions";
import { Observable } from "rxjs";
import { DropzoneComponent, DropzoneConfigInterface, DropzoneDirective } from 'ngx-dropzone-wrapper';


@Component({
  selector: 'app-timeline-create-post',
  templateUrl: './timeline-create-post.component.html',
  styleUrls: ['./timeline-create-post.component.css']
})
export class TimelineCreatePostComponent implements OnInit {

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
    addRemoveLinks: true,
  };

  @ViewChild(DropzoneComponent, { static: false }) componentRef?: DropzoneComponent;
  @ViewChild(DropzoneDirective, { static: false }) directiveRef?: DropzoneDirective;

  constructor(private modalService: ModalService,
    private postStore: Store<fromPost.State>,
    private authService: AuthService,
    private spinner: NgxSpinnerService) { }

  postCreate: any = {};
  fileData: File = null;
  previewUrl: any = null;
  isHighlighted: boolean = false;
  public activityFeed = true;
  public storyFeed = false;
  public type: string = 'component';
  public disabled: boolean = false;
  files: File[] = [];

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

  fileProgress(fileInput: any) {
    this.fileData = <File>fileInput.target.files[0];
    this.preview();
  }

  preview() {
    // Show preview
    var mimeType = this.fileData.type;
    if (mimeType.match(/(image|video)\/*/) == null) {
      return;
    }

    if (mimeType.match(/image\/*/)) {
      var reader = new FileReader();
      reader.readAsDataURL(this.fileData);
      reader.onload = _event => {
        this.previewUrl = reader.result;
      };
    }
  }

  checkFileType(data: File) {
    var type = data.type;
    if (type.match(/image\/*/)) return "image";
    else return "video";
  }

  closePreview() {
    this.fileData = null;
    this.previewUrl = null;
  }

  savePost() {
    var formData: any = new FormData();
    if (this.fileData != null) {
      let checkedItem = this.checkFileType(this.fileData);
      if (checkedItem === "image") formData.append("photo", this.fileData);
      else if (checkedItem === "video") formData.append("video", this.fileData);
    }

    formData.append("Text", this.postCreate.text);
    formData.append("IsPost", this.activityFeed.valueOf());
    formData.append("IsStory", this.storyFeed.valueOf());
    this.postStore.dispatch(new postActions.CreatePost(formData));
    this.postCreate.text = null;
    this.closePreview();
    this.makeUnHighlighted();
  }


  closeModal(id: string) {
    this.modalService.close(id);
  }

  openModal(id: string) {
    this.modalService.open(id);
  }

  makeHighlighted() {
    this.isHighlighted = true;
    $('.app-overlay').addClass('is-active');
  }

  makeUnHighlighted() {
    this.isHighlighted = false;
    $('.app-overlay').removeClass('is-active');
    $('#compose-search, #extended-options, .is-suboption').addClass('is-hidden');
    $('#basic-options, #open-compose-search').removeClass('is-hidden');
  }

  changeFeed = (evt: any) => {
    this.activityFeed = evt.target.checked;
  }

  changeStory = (evt: any) => {
    this.storyFeed = evt.target.checked;
  }

}
