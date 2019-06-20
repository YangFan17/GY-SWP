import { Component, Injector, Input, OnInit } from '@angular/core';
import { ModalComponentBase } from '@shared/component-base';
import { ApplyInfo } from 'entities';
import { WorkCriterionService } from 'services';

@Component({
    moduleId: module.id,
    selector: 'doc-application',
    templateUrl: 'doc-application.component.html',
    styleUrls: ['doc-application.component.less']
})
export class DocApplicationComponent extends ModalComponentBase implements OnInit {
    @Input() docId: string;
    @Input() docName: string;
    applyInfo: ApplyInfo = new ApplyInfo();
    operateTypes = [
        // { value: 1, text: '制定标准' },
        { value: 2, text: '修订标准' },
        { value: 3, text: '废止标准' }
    ];

    constructor(injector: Injector
        , private workCriterionService: WorkCriterionService
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.applyInfo.operateType = 2;
    }

    submit() {
        this.applyInfo.documentId = this.docId;
        this.saving = true;
        this.workCriterionService.applyDocAsync(this.applyInfo)
            .finally(() => { this.saving = false; })
            .subscribe(res => {
                if (res.code == 0) {
                    this.notify.info('申请已提交！', '');
                    this.success(true);
                } else {
                    this.notify.error('申请异常，请重试！', '');
                    this.success(false);
                }
            });
    }
}
