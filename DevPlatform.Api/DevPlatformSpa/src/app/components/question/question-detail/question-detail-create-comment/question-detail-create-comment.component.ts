import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { CKEditorComponent } from 'ng2-ckeditor';

/* Models */
import { QuestionCommentCreate } from '../../../../models/question/questionCommentCreate';

/* NgRx */
import { Store, select } from "@ngrx/store";
import * as fromQuestion from "../../../../core/ngrx/selectors/question.selectors";
import * as questionActions from "../../../../core/ngrx/actions/question.actions";

@Component({
  selector: 'app-question-detail-create-comment',
  templateUrl: './question-detail-create-comment.component.html',
  styleUrls: ['./question-detail-create-comment.component.scss']
})
export class QuestionDetailCreateCommentComponent implements OnInit {

  name = 'comment-create';
  commentConfig: CKEDITOR.config;
  commentCreate: QuestionCommentCreate = new QuestionCommentCreate;
  @ViewChild("commenteditor") ckeditor: CKEditorComponent;
  @Input() errorMessage: string;
  @Input() currentQuestionId: number;

  constructor(private questionStore: Store<fromQuestion.State>) {
  }

  ngOnInit() {
    this.commentConfig = {
      allowedContent: false,
      extraPlugins: 'codesnippet',
      forcePasteAsPlainText: true,
      language: "en",
    };
  }

  onChange($event: any): void {
  }

  onPaste($event: any): void {
  }

  saveComment() {
    console.log(this.commentCreate);
    this.commentCreate.questionId = this.currentQuestionId;
    this.questionStore.dispatch(new questionActions.CreateComment(this.commentCreate));
    if (!this.errorMessage) {
      this.unFillData();
    }
  }

  unFillData() {
    this.commentCreate = new QuestionCommentCreate;
  }
}
