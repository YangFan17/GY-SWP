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
    ],
    entryComponents: [
        CriterionExamineComponent,
        EmpListComponent,
    ],
    providers: [SupervisionService]
})

export class SupervisionModule {

}