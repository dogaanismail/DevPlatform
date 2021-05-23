import { Component, Input, OnInit } from '@angular/core';

/* Models */
import { Story } from '../../../models/story/story';

@Component({
  selector: 'app-story-feed',
  templateUrl: './story-feed.component.html',
  styleUrls: ['./story-feed.component.scss']
})
export class StoryFeedComponent implements OnInit {

  constructor() { }

  @Input() stories: Story[];

  ngOnInit() {
  }

}
