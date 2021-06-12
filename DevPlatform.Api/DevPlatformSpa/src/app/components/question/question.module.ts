/* Modules */
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from '../../shared/modules/shared.module';
import { CKEditorModule } from 'ng2-ckeditor';
import { TagInputModule } from 'ngx-chips';
import { PipesModule } from '../../shared/modules/pipes.modules';

/* Components */
import { QuestionSettingsComponent } from './question-settings/question-settings.component';
import { QuestionPostsComponent } from './common/question-posts/question-posts.component';
import { QuestionMenuTopComponent } from './common/question-menu-top/question-menu-top.component';
import { QuestionCategoriesComponent } from './common/question-categories/question-categories.component';
import { QuestionMenuFixedComponent } from './common/question-menu-fixed/question-menu-fixed.component';
import { CreateQuestionComponent } from './common/create-question/create-question.component';
import { QuestionLayoutComponent } from './question-layout/question-layout.component';
import { QuestionDetailComponent } from './question-detail/question-detail.component';
import { DevPlatformMaterialModule } from '../../shared/modules/material.module';

/*Ngrx and Store infrastructure implementations */
import { storageMetaReducer } from '../../core/store-infrastructure/storage-metareducer';
import * as fromReducer from '../../core/ngrx/reducers/question.reducer';
import { StoreModule } from '@ngrx/store';
import { QUESTIONS_CONFIG_TOKEN, QUESTIONS_LOCAL_STORAGE_KEY, QUESTIONS_STORAGE_KEYS } from './question.tokens';
import { StoreLocalStorageService } from '../../core/store-infrastructure/store-local-storage.service';

export function getQuestionsConfig(saveKeys: string[], localStorageKey: string, storageService: StoreLocalStorageService) {
    return { metaReducers: [storageMetaReducer(saveKeys, localStorageKey, storageService)] };
}

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
        SharedModule,
        CKEditorModule,
        TagInputModule,
        DevPlatformMaterialModule,
        PipesModule,
        StoreModule.forFeature('questions', fromReducer.questionReducer, QUESTIONS_CONFIG_TOKEN),
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
    ],
    providers: [
        StoreLocalStorageService,
        { provide: QUESTIONS_LOCAL_STORAGE_KEY, useValue: '__questions_storage__' },
        { provide: QUESTIONS_STORAGE_KEYS, useValue: ['questions', 'viewMode'] },
        {
            provide: QUESTIONS_CONFIG_TOKEN,
            deps: [QUESTIONS_STORAGE_KEYS, QUESTIONS_LOCAL_STORAGE_KEY, StoreLocalStorageService],
            useFactory: getQuestionsConfig
        },
    ]
})
export class QuestionModule { }
