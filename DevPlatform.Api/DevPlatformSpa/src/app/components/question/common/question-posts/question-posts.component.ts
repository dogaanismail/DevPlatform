import { Component, Input, OnInit } from '@angular/core';

/* Models */
import { Question } from '../../../../models/question/question';

@Component({
  selector: 'app-question-posts',
  templateUrl: './question-posts.component.html',
  styleUrls: ['./question-posts.component.css']
})
export class QuestionPostsComponent implements OnInit {

  constructor() { }

  pageTitle = 'Developer Questions';
  @Input() questions: Question[];
  @Input() newQuestion: boolean;
  @Input() newComment: boolean;

  ngOnInit() {
  }

}
