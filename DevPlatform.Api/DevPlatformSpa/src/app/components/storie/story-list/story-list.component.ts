import { Component, Input, OnInit } from '@angular/core';

/* Models */
import { Story } from '../../../models/story/story';

@Component({
  selector: 'app-story-list',
  templateUrl: './story-list.component.html',
  styleUrls: ['./story-list.component.scss']
})
export class StoryListComponent implements OnInit {

  constructor() { }

  @Input() stories: Story[];

  ngOnInit() {
  }

}
