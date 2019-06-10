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
        ConfirmLearningComponent
    ],
    entryComponents: [
        CriterionComponent,
        PreviewDocComponent,
        SelfLearningComponent,
        ConfirmLearningComponent
    ],
    providers: [WorkCriterionService]
})
export class WorkCriterionModule {

}
