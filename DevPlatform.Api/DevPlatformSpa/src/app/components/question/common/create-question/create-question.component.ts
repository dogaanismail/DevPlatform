import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { CKEditorComponent } from 'ng2-ckeditor';

/* NgRx */
import { Store, select } from "@ngrx/store";
import * as fromQuestion from "../../../../core/ngrx/selectors/question.selectors";
import * as questionActions from "../../../../core/ngrx/actions/question.actions";
import { QuestionCreate } from 'src/app/models/question/questionCreate';

@Component({
  selector: 'app-create-question',
  templateUrl: './create-question.component.html',
  styleUrls: ['./create-question.component.css']
})
export class CreateQuestionComponent implements OnInit {

  name = 'ng2-ckeditor';
  ckeConfig: CKEDITOR.config;
  questionCreate: QuestionCreate = new QuestionCreate;
  @ViewChild("myckeditor") ckeditor: CKEditorComponent;
  @Input() errorMessage: string;

  constructor(private questionStore: Store<fromQuestion.State>) {
  }

  ngOnInit() {
    this.ckeConfig = {
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

  saveQuestion() {
    this.questionStore.dispatch(new questionActions.CreateQuestion(this.questionCreate));
    if (!this.errorMessage) {
      this.unFillData();
    }
  }

  unFillData() {
    this.questionCreate = new QuestionCreate;
  }
}
