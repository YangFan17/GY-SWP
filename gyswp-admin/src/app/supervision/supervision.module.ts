import { NgModule } from '@angular/core';
import { SupervisionService } from 'services';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { LayoutModule } from '@layout/layout.module';
import { SharedModule } from '@shared/shared.module';
import { SupervisionRoutingModule } from './supervision-routing.mdule';
import { EmpListComponent } from './criterion-examine/emp-list/emp-list.component';
import { CriterionExamineComponent } from './criterion-examine/criterion-examine.component';
import { DeptExamineRecordComponent } from './criterion-examine/dept-examine-record/dept-examine-record.component';
import { RecordDetailComponent } from './criterion-examine/dept-examine-record/record-detail/record-detail.component';
import { CheckingResultComponent } from './criterion-examine/checking-result/checking-result.component';
import { EmpExamineRecordComponent } from './criterion-examine/emp-list/emp-examine-record/emp-examine-record.component';
import { FeedbackResultComponent } from './criterion-examine/feedback-result/feedback-result.component';
import { SuperAdminComponent } from './criterion-examine/super-admin/super-admin.component';
import { TargetExamineComponent } from './target-examine/target-examine.component';
import { TargetExamineDetailComponent } from './target-examine/target-examine-detail/target-examine-detail.component';
import { TargetDeptComponent } from './target-examine/target-examine-detail/target-dept/target-dept.component';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        LayoutModule,
        SharedModule,
        SupervisionRoutingModule,
    ],
    declarations: [
        CriterionExamineComponent,
        EmpListComponent,
        DeptExamineRecordComponent,
        RecordDetailComponent,
        CheckingResultComponent,
        EmpExamineRecordComponent,
        FeedbackResultComponent,
        SuperAdminComponent,
        TargetExamineComponent,
        TargetExamineDetailComponent,
        TargetDeptComponent,
    ],
    entryComponents: [
        CriterionExamineComponent,
        EmpListComponent,
        DeptExamineRecordComponent,
        RecordDetailComponent,
        CheckingResultComponent,
        EmpExamineRecordComponent,
        FeedbackResultComponent,
        SuperAdminComponent,
        TargetExamineComponent,
        TargetExamineDetailComponent,
        TargetDeptComponent,
    ],
    providers: [SupervisionService]
})

export class SupervisionModule {

}