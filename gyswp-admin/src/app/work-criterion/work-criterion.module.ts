// Angular Imports
import { NgModule } from '@angular/core';

// This Module's Components
import { WorkCriterionRoutingModule } from './work-criterion-routing.module';
import { CriterionComponent } from './criterion/criterion.component';
import { WorkCriterionService } from 'services';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { LayoutModule } from '@layout/layout.module';
import { SharedModule } from '@shared/shared.module';
import { PreviewDocComponent } from './criterion/preview-doc/preview-doc.component';
import { PdfViewerModule } from 'ng2-pdf-viewer';
import { SelfLearningComponent } from './criterion/self-learning/self-learning.component';
import { ConfirmLearningComponent } from './criterion/self-learning/confirm-learning/confirm-learning.component';
import { DocApplicationComponent } from './criterion/self-learning/doc-application/doc-application.component';
import { RevisedListComponent } from './criterion/self-learning/revised-list/revised-list.component';
import { RevisedClauseDetailComponent } from './criterion/self-learning/revised-clause-detail/revised-clause-detail.component';
import { RevisedListDetailComponent } from './criterion/self-learning/revised-list/revised-list-detail/revised-list-detail.component';
import { DraftApplyInfoComponent } from './criterion/draft-doc/draft-apply-info/draft-apply-info.component';
import { DraftDocComponent } from './criterion/draft-doc/draft-doc/draft-doc.component';
import { DraftClauseComponent } from './criterion/draft-doc/draft-doc/draft-clause/draft-clause.component';
import { MyExamineComponent } from './my-examine/my-examine.component';
import { ExamineDetailComponent } from './my-examine/examine-detail/examine-detail.component';
import { ResultDetailComponent } from './my-examine/examine-detail/result-detail/result-detail.component';
import { ResultFeedbackComponent } from './my-examine/examine-detail/result-feedback/result-feedback.component';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        LayoutModule,
        SharedModule,
        WorkCriterionRoutingModule,
        PdfViewerModule
    ],
    declarations: [
        CriterionComponent,
        PreviewDocComponent,
        SelfLearningComponent,
        ConfirmLearningComponent,
        DocApplicationComponent,
        RevisedClauseDetailComponent,
        RevisedListComponent,
        RevisedListDetailComponent,
        DraftApplyInfoComponent,
        DraftDocComponent,
        DraftClauseComponent,
        MyExamineComponent,
        ExamineDetailComponent,
        ResultDetailComponent,
        ResultFeedbackComponent
    ],
    entryComponents: [
        CriterionComponent,
        PreviewDocComponent,
        SelfLearningComponent,
        ConfirmLearningComponent,
        DocApplicationComponent,
        RevisedClauseDetailComponent,
        RevisedListComponent,
        RevisedListDetailComponent,
        DraftApplyInfoComponent,
        DraftDocComponent,
        DraftClauseComponent,
        MyExamineComponent,
        ExamineDetailComponent,
        ResultDetailComponent,
        ResultFeedbackComponent
    ],
    providers: [WorkCriterionService]
})
export class WorkCriterionModule {

}
