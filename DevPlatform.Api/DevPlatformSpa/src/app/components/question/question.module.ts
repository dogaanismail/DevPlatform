import { QuestionSettingsComponent } from './question-settings/question-settings.component';
import { QuestionPostsComponent } from './common/question-posts/question-posts.component';
import { QuestionMenuTopComponent } from './common/question-menu-top/question-menu-top.component';
import { QuestionCategoriesComponent } from './common/question-categories/question-categories.component';
import { QuestionMenuFixedComponent } from './common/question-menu-fixed/question-menu-fixed.component';
import { CreateQuestionComponent } from './common/create-question/create-question.component';
import { QuestionLayoutComponent } from './question-layout/question-layout.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { QuestionDetailComponent } from './question-detail/question-detail.component';

const questionRoutes: Routes = [
    { path: "question", component: QuestionLayoutComponent },
    { path: "question/detail", component: QuestionDetailComponent },
    { path: "question/setting", component: QuestionSettingsComponent },
    { path: "question/categories", component: QuestionCategoriesComponent }
    //TODO: must be question/detail/id?= 
];

@NgModule({
    imports: [
        RouterModule.forChild(questionRoutes),
    ],
    declarations: [
        QuestionLayoutComponent,
        CreateQuestionComponent,
        QuestionMenuFixedComponent,
        QuestionCategoriesComponent,
        QuestionMenuTopComponent,
        QuestionPostsComponent,
        QuestionDetailComponent,
        QuestionSettingsComponent,
        QuestionCategoriesComponent
    ]
})
export class QuestionModule { }
