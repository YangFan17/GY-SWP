import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutModule } from '@layout/layout.module';
import { SharedModule } from '@shared/shared.module';
import { HttpClientModule } from '@angular/common/http';
import { ReportsRoutingModule } from './reports-routing.module';
import { InspectComponent } from './inspect/inspect.component';
import { BasicDataService } from 'services';
import { SuperviseComponent } from './supervise/supervise.component';
import { StandardrevisionComponent } from './standardrevision/standardrevision.component';
import { AdviseComponent } from './advise/advise.component';
import { IndicatorSuperviseComponent } from './indicator-supervise/indicator-supervise.component';

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
        IndicatorSuperviseComponent
    ],
    entryComponents: [
        InspectComponent,
        SuperviseComponent,
        StandardrevisionComponent,
        AdviseComponent,
        IndicatorSuperviseComponent
    ],
    providers: [BasicDataService],
})
export class ReportsModule { }
