import { Component, OnInit } from '@angular/core';
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



@Component({
  selector: 'app-timeline-create-post',
  templateUrl: './timeline-create-post.component.html',
  styleUrls: ['./timeline-create-post.component.scss']
})
export class TimelineCreatePostComponent implements OnInit {

  constructor(private modalService: ModalService,
    private postStore: Store<fromPost.State>,
    private authService: AuthService,
    private spinner: NgxSpinnerService) { }

  postCreate: any = {};
  fileData: File = null;
  previewUrl: any = null;
  isHighlighted: boolean = false;

  ngOnInit() {
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
    this.spinner.show();
    var formData: any = new FormData();
    if (this.fileData != null) {
      let checkedItem = this.checkFileType(this.fileData);
      if (checkedItem === "image") formData.append("photo", this.fileData);
      else if (checkedItem === "video") formData.append("video", this.fileData);
    }

    formData.append("Text", this.postCreate.text);

    setTimeout(() => {
      this.spinner.hide();
    }, 2000);

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
}
