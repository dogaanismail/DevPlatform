import { Component, OnInit } from '@angular/core';
import { ModalService } from '../../../services/modal/modal.service';

@Component({
  selector: 'app-timeline-create-post',
  templateUrl: './timeline-create-post.component.html',
  styleUrls: ['./timeline-create-post.component.scss']
})
export class TimelineCreatePostComponent implements OnInit {

  constructor(private modalService: ModalService) { }

  ngOnInit() {
  }

  closeModal(id: string) {
    this.modalService.close(id);
  }

  openModal(id: string) {
    this.modalService.open(id);
  }

}
