import { Component, Input, OnInit } from '@angular/core';

/* Models */
import { QuestionComment } from '../../../../models/question/questionComment';

@Component({
  selector: 'app-question-detail-comments',
  templateUrl: './question-detail-comments.component.html',
  styleUrls: ['./question-detail-comments.component.css']
})
export class QuestionDetailCommentsComponent implements OnInit {

  constructor() { }

  @Input() commentList: QuestionComment[];

  ngOnInit() {
  }

}
