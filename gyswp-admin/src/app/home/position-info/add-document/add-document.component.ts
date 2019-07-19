import { ChangeDetectionStrategy, OnInit, ViewEncapsulation } from '@angular/core';
import { Component, Input, Injector } from '@angular/core';
import { HomeService } from 'services/home/home.service';
import { AppComponentBase, ModalComponentBase } from '@shared/component-base';
import { MainPointsRecord } from 'entities';

@Component({
    moduleId: module.id,
    selector: 'add-document',
    styleUrls: ['add-document.component.less'],
    templateUrl: 'add-document.component.html',
    providers: [HomeService],
})
export class AddDocumentComponent extends ModalComponentBase implements OnInit {
    @Input() positionInfoId: string;
    inputValue: string;
    mainPointsRecord: MainPointsRecord = new MainPointsRecord();
    mainPoint: string;
    optionGroups: any[];
    docId: any;
    constructor(
        injector: Injector,
        private homeService: HomeService
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.getCategoryDocList();
    }

    getCategoryDocList() {
        this.homeService.getCategoryDocByCurrentUser().subscribe((result) => {
            this.optionGroups = result;
            console.log(this.optionGroups);
        });
    }

    save(): void {
        // console.log(this.docId);
        if (this.docId) {
            this.mainPointsRecord.mainPoint = this.mainPoint;
            this.mainPointsRecord.positionInfoId = this.positionInfoId;
            this.mainPointsRecord.documentId = this.docId;
            this.homeService.createMainPointRecord(this.mainPointsRecord)
                .finally(() => { this.saving = false; })
                .subscribe((data) => {
                    if (data.code == 0) {
                        this.notify.success('保存成功');
                        this.success(true);
                    } else {
                        this.notify.error('保存失败,请重试.');
                        this.success(false);
                    }
                });

        }
    }
}
