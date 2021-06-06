import { Component, OnInit, ViewChild } from '@angular/core';
import { CKEditorComponent } from 'ng2-ckeditor';

@Component({
  selector: 'app-create-question',
  templateUrl: './create-question.component.html',
  styleUrls: ['./create-question.component.css']
})
export class CreateQuestionComponent implements OnInit {

  name = 'ng2-ckeditor';
  ckeConfig: CKEDITOR.config;
  questionCreate: any = {};
  log: string = '';
  tags: '';
  @ViewChild("myckeditor") ckeditor: CKEditorComponent;

  constructor() { 
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
    console.log("onChange");
  }

  onPaste($event: any): void {
    console.log("onPaste");
  }

  saveQuestion(){
    console.log("onSaved");
  }

}
