import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutModule } from '@layout/layout.module';
import { SharedModule } from '@shared/shared.module';
import { HttpClientModule } from '@angular/common/http';
import { ReportsRoutingModule } from './reports-routing.module';
import { InspectComponent } from './inspect/inspect.component';
import { SuperviseComponent } from './supervise/supervise.component';
import { StandardrevisionComponent } from './standardrevision/standardrevision.component';
import { AdviseComponent } from './advise/advise.component';
import { IndicatorSuperviseComponent } from './indicator-supervise/indicator-supervise.component';
import { StandardConfirmComponent } from './standard-confirm/standard-confirm.component';
import { ConfirmDetailComponent } from './standard-confirm/confirm-detail/confirm-detail.component';
import { RevisionDocComponent } from './standardrevision/revision-doc/revision-doc.component';
import { RevisionDraftComponent } from './standardrevision/revision-draft/revision-draft.component';
import { RevisionDetailComponent } from './standardrevision/revision-draft/revision-detail/revision-detail.component';
import { DraftDetailComponent } from './standardrevision/revision-draft/draft-detail/draft-detail.component';


@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        ReportsRoutingModule,
        LayoutModule,
        SharedModule,
    ],
    declarations: [
        InspectComponent,
        SuperviseComponent,
        StandardrevisionComponent,
        AdviseComponent,
        IndicatorSuperviseComponent,
        StandardConfirmComponent,
        ConfirmDetailComponent,
        RevisionDocComponent,
        RevisionDraftComponent,
        RevisionDetailComponent,
        DraftDetailComponent
    ],
    entryComponents: [
        InspectComponent,
        SuperviseComponent,
        StandardrevisionComponent,
        AdviseComponent,
        IndicatorSuperviseComponent,
        StandardConfirmComponent,
        ConfirmDetailComponent,
        RevisionDocComponent,
        RevisionDraftComponent,
        RevisionDetailComponent,
        DraftDetailComponent
    ]
})
export class ReportsModule { }
