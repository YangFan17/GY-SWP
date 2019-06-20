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
import { ClauseDetailComponent } from '@app/basic-data/document/clause/clause-detail/clause-detail.component';
import { RevisedListComponent } from './criterion/self-learning/revised-list/revised-list.component';
import { RevisedClauseDetailComponent } from './criterion/self-learning/revised-clause-detail/revised-clause-detail.component';
import { RevisedListDetailComponent } from './criterion/self-learning/revised-list/revised-list-detail/revised-list-detail.component';

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
        RevisedListDetailComponent
    ],
    entryComponents: [
        CriterionComponent,
        PreviewDocComponent,
        SelfLearningComponent,
        ConfirmLearningComponent,
        DocApplicationComponent,
        RevisedClauseDetailComponent,
        RevisedListComponent,
        RevisedListDetailComponent
    ],
    providers: [WorkCriterionService]
})
export class WorkCriterionModule {

}
