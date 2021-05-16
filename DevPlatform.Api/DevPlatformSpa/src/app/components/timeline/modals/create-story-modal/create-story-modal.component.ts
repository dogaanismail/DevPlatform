import { Component, Input, OnInit } from '@angular/core';

/* Models */
import { SignedUser } from '../../../../models/user/signedUser';

/* NgRx */
import { Store, select } from "@ngrx/store";
import * as fromStory from "../../../../core/ngrx/selectors/story.selectors";
import * as storyActions from "../../../../core/ngrx/actions/story.actions";
import { Observable } from "rxjs";

@Component({
  selector: 'app-create-story-modal',
  templateUrl: './create-story-modal.component.html',
  styleUrls: ['./create-story-modal.component.scss']
})
export class CreateStoryModalComponent implements OnInit {

  constructor(private storyStore: Store<fromStory.State>) { }

  @Input() signedUser: SignedUser;
  storyCreate: any = {};
  fileData: File = null;
  previewUrl: any = null;

  ngOnInit() {
  }

  saveStory() {
    var formData: any = new FormData();
    formData.append('title', this.storyCreate.title);
    formData.append('place', this.storyCreate.description);

    if (this.fileData != null) {
      let checkedItem = this.checkFileType(this.fileData);
      if (checkedItem === "image") formData.append("photo", this.fileData);
      else if (checkedItem === "video") formData.append("video", this.fileData);
    }

    this.storyStore.dispatch(new storyActions.CreateStory(formData));
  }

  checkFileType(data: File) {
    var type = data.type;
    if (type.match(/image\/*/)) return "image";
    else return "video";
  }

  fileProgress(fileInput: any) {
    this.fileData = <File>fileInput.target.files[0];
    this.preview();
  }

  preview() {
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

  closePreview() {
    this.fileData = null;
    this.previewUrl = null;
  }

}
