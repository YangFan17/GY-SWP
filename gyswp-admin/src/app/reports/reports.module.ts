import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutModule } from '@layout/layout.module';
import { SharedModule } from '@shared/shared.module';
import { HttpClientModule } from '@angular/common/http';
import { ReportsRoutingModule } from './reports-routing.module';
import { InspectComponent } from './inspect/inspect.component';
import { BasicDataService } from 'services';
import { StandardrevisionComponent } from './standardrevision/standardrevision.component';

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
        StandardrevisionComponent
    ],
    entryComponents: [

    ],
    providers: [BasicDataService],
})
export class ReportsModule { }
