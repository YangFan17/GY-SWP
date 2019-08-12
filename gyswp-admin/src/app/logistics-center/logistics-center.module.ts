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
import { EntryExitRegistrationComponent } from './storage-custody/entry-exit-registration/entry-exit-registration.component';
import { InventoryRecordComponent } from './storage-custody/inventory-record/inventory-record.component';
import { InspectionRecordComponent } from './storage-custody/inspection-record/inspection-record.component';
import { MildewSummerComponent } from './storage-custody/mildew-summer/mildew-summer.component';
import { ConveyorCheckComponent } from './in-storage/conveyor-check/conveyor-check.component';
import { ForkliftCheckComponent } from './in-storage/forklift-check/forklift-check.component';
import { SortingEquipCheckComponent } from './out-storage-classify/sorting-equip-check/sorting-equip-check.component';
import { TeamSafetyActivityComponent } from './out-storage-classify/team-safety-activity/team-safety-activity.component';

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
        ConveyorCheckComponent,
        ForkliftCheckComponent,
        //在库报管
        StorageCustodyComponent,
        EntryExitRegistrationComponent,
        InventoryRecordComponent,
        InspectionRecordComponent,
        MildewSummerComponent,
        //出库分拣
        OutStorageClassifyComponent,
        UseOutStorageComponent,
        CigaretExchangeComponent,
        SortingEquipCheckComponent,
        TeamSafetyActivityComponent,
        //领货出库
        OutStorageComponent,
        OutScanRecordComponent,
    ],
    entryComponents: [
        InStorageComponent,
        InStorageRecordComponent,
        QualityRecordComponent,
        InStorageBillComponent,
        InStorageScanComponent,
        ConveyorCheckComponent,
        ForkliftCheckComponent,
        //在库报管
        StorageCustodyComponent,
        EntryExitRegistrationComponent,
        InventoryRecordComponent,
        InspectionRecordComponent,
        MildewSummerComponent,
        //出库分拣
        OutStorageClassifyComponent,
        UseOutStorageComponent,
        CigaretExchangeComponent,
        SortingEquipCheckComponent,
        TeamSafetyActivityComponent,
        //领货出库
        OutStorageComponent,
        OutScanRecordComponent,

    ],
    providers: [LogisticService]
})
export class LogisticsCenterModule {
}