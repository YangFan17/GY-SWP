import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { LayoutModule } from '@layout/layout.module';
import { SharedModule } from '@shared/shared.module';
import { CommonModule } from '@angular/common';
import { LogisticsCenterRoutingModule } from './logistics-center-routing.module';
import { InStorageComponent } from './in-storage/in-storage.component';
import { InStorageRecordComponent } from './in-storage/in-storage-record/in-storage-record.component';
import { LogisticService } from 'services';

@NgModule({
    imports: [
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        LayoutModule,
        SharedModule,
        CommonModule,
        LogisticsCenterRoutingModule
    ],
    declarations: [
        InStorageComponent,
        InStorageRecordComponent,
    ],
    entryComponents: [
        InStorageComponent,
        InStorageRecordComponent,
    ],
    providers: [LogisticService]
})
export class LogisticsCenterModule {
}