import { Component, Input, OnInit } from '@angular/core';

/* Models */
import { SignedUser } from '../../../models/user/signedUser';

/* NgRx */
import { Store, select } from "@ngrx/store";
import * as fromStory from "../../../core/ngrx/selectors/story.selectors";
import * as storyActions from "../../../core/ngrx/actions/story.actions";
import { Observable } from "rxjs";

/* Services */
import { ModalService } from '../../../services/modal/modal.service';

@Component({
  selector: 'app-create-story-modal',
  templateUrl: './create-story-modal.component.html',
  styleUrls: ['./create-story-modal.component.scss']
})
export class CreateStoryModalComponent implements OnInit {

  constructor(
    private storyStore: Store<fromStory.State>,
    private modalService: ModalService) { }

  @Input() signedUser: SignedUser;
  storyCreate: any = {};
  storyImage: File = null;
  imgPreviewUrl: any = null;

  ngOnInit() {
  }

  saveStory() {
    var formData: any = new FormData();
    formData.append('title', this.storyCreate.title);
    formData.append('description', this.storyCreate.description);

    if (this.storyImage != null) {
      let checkedItem = this.checkFileType(this.storyImage);
      if (checkedItem === "image") formData.append("photo", this.storyImage);
      else if (checkedItem === "video") formData.append("video", this.storyImage);
    }

    this.storyStore.dispatch(new storyActions.CreateStory(formData));
    this.storyCreate.title = null;
    this.storyCreate.description = null;
    this.storyCreate.photo = null;
    this.imgPreviewUrl = null;
    this.closeModal("image-story-modal");
  }

  checkFileType(data: File) {
    var type = data.type;
    if (type.match(/image\/*/)) return "image";
    else return "video";
  }

  fileProgress(fileInput: any) {
    this.storyImage = <File>fileInput.target.files[0];
    this.previewImg();
  }

  previewImg() {
    var mimeType = this.storyImage.type;

    if (mimeType.match(/image\/*/)) {
      var reader = new FileReader();
      reader.readAsDataURL(this.storyImage);
      reader.onload = _event => {
        this.imgPreviewUrl = reader.result;
      };
    }
  }

  closeModal(id: string) {
    this.modalService.close(id);
  }

}
