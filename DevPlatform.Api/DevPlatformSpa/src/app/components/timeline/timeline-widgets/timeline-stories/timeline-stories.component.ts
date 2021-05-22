import { Component, Input, OnInit } from '@angular/core';

/*Models */
import { SignedUser } from '../../../../models/user/signedUser';
import { Story } from '../../../../models/story/story';

/*Services */
import { ModalService } from '../../../../services/modal/modal.service';

@Component({
  selector: 'app-timeline-stories',
  templateUrl: './timeline-stories.component.html',
  styleUrls: ['./timeline-stories.component.scss']
})
export class TimelineStoriesComponent implements OnInit {

  constructor(private modalService: ModalService) { }

  @Input() signedUser: SignedUser;
  @Input() stories: Story[];

  ngOnInit() {
  }

  closeModal(id: string) {
    this.modalService.close(id);
  }

  openModal(id: string) {
    this.modalService.open(id);
  }

}
