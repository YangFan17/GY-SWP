import { ChangeDetectionStrategy, OnInit, ViewEncapsulation } from '@angular/core';
import { Component, Input, Injector } from '@angular/core';
import { HomeService } from 'services/home/home.service';
import { AppComponentBase, ModalComponentBase } from '@shared/component-base';
import { MainPointsRecord } from 'entities';
import { EmpDocListComponent } from '../emp-doc-list/emp-doc-list.component';

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
    docId: string;
    doc: any = {};
    constructor(
        injector: Injector,
        private homeService: HomeService
    ) {
        super(injector);
    }

    ngOnInit(): void {
        // this.getCategoryDocList();
    }

    getCategoryDocList() {
        this.homeService.getCategoryDocByCurrentUser().subscribe((result) => {
            this.optionGroups = result;
            // console.log(this.optionGroups);
        });
    }

    chooseDoc() {
        this.modalHelper
            .open(EmpDocListComponent, { doc: this.doc }, 'lg', {
                nzMask: true,
                nzClosable: false,
                nzMaskClosable: false,
            })
            .subscribe(doc => {
                if (doc.id) {
                    this.doc.id = doc.id;
                    this.doc.name = doc.name;
                }
            });
    }

    save(): void {
        if (this.doc.id) {
            this.mainPointsRecord.mainPoint = this.mainPoint;
            this.mainPointsRecord.positionInfoId = this.positionInfoId;
            this.mainPointsRecord.documentId = this.doc.id;
            this.saving = true;
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
