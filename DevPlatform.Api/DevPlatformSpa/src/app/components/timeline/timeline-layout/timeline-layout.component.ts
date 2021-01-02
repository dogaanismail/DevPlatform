import { Component, OnInit } from '@angular/core';
import { ModalService } from '../../../services/modal/modal.service';

@Component({
  selector: 'app-timeline-layout',
  templateUrl: './timeline-layout.component.html',
  styleUrls: ['./timeline-layout.component.css']
})
export class TimelineLayoutComponent implements OnInit {

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
