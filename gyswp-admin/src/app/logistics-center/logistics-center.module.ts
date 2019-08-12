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
import { QualityRecordComponent } from './in-storage/quality-record/quality-record.component';
import { InStorageBillComponent } from './in-storage/in-storage-bill/in-storage-bill.component';
import { InStorageScanComponent } from './in-storage/in-storage-scan/in-storage-scan.component';
import { OutStorageComponent } from './out-storage/out-storage.component';
import { OutScanRecordComponent } from './out-storage/out-scan-record/out-scan-record.component';
import { OutStorageClassifyComponent } from './out-storage-classify/out-storage-classify.component';
import { StorageCustodyComponent } from './storage-custody/storage-custody.component';
import { UseOutStorageComponent } from './out-storage-classify/use-out-storage/use-out-storage.component';
import { CigaretExchangeComponent } from './out-storage-classify/cigaret-exchange/cigaret-exchange.component';
import { DailyCheckComponent } from './in-storage/daily-check/daily-check.component';

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
        QualityRecordComponent,
        InStorageBillComponent,
        InStorageScanComponent,
        //在库报管
        StorageCustodyComponent,
        //出库分拣
        OutStorageClassifyComponent,
        UseOutStorageComponent,
        CigaretExchangeComponent,
        //领货出库
        OutStorageComponent,
        OutScanRecordComponent,
        DailyCheckComponent,
    ],
    entryComponents: [
        InStorageComponent,
        InStorageRecordComponent,
        QualityRecordComponent,
        InStorageBillComponent,
        InStorageScanComponent,
        //在库报管
        StorageCustodyComponent,
        //出库分拣
        OutStorageClassifyComponent,
        UseOutStorageComponent,
        CigaretExchangeComponent,
        //领货出库
        OutStorageComponent,
        OutScanRecordComponent,
        DailyCheckComponent,
    ],
    providers: [LogisticService]
})
export class LogisticsCenterModule {
}