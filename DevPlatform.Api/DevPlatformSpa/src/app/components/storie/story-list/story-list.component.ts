import { Component, Input, OnInit } from '@angular/core';
import { SignedUser } from 'src/app/models/user/signedUser';
import { ModalService } from 'src/app/services/modal/modal.service';

/* Models */
import { Story } from '../../../models/story/story';

@Component({
  selector: 'app-story-list',
  templateUrl: './story-list.component.html',
  styleUrls: ['./story-list.component.scss']
})
export class StoryListComponent implements OnInit {

  constructor(private modalService: ModalService) { }

  @Input() stories: Story[];
  @Input() newStory: boolean;
  @Input() signedUser: SignedUser;

  ngOnInit() {
  }

  closeModal(id: string) {
    this.modalService.close(id);
  }

  openModal(id: string) {
    this.modalService.open(id);
  }

}
