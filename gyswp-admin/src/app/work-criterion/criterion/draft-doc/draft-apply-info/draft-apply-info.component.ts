import { Component, OnInit, Injector } from '@angular/core';
import { ModalComponentBase } from '@shared/component-base';
import { ApplyInfo } from 'entities';
import { WorkCriterionService } from 'services';

@Component({
    moduleId: module.id,
    selector: 'draft-apply-info',
    templateUrl: 'draft-apply-info.component.html',
    styleUrls: ['draft-apply-info.component.less']
})
export class DraftApplyInfoComponent extends ModalComponentBase implements OnInit {
    applyInfo: ApplyInfo = new ApplyInfo();
    operateTypes = [
        { value: 1, text: '制定标准' },
    ];

    constructor(injector: Injector
        , private workCriterionService: WorkCriterionService
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.applyInfo.operateType = 1;
    }

    submit() {
        this.saving = true;
        this.workCriterionService.applyDocAsync(this.applyInfo)
            .finally(() => { this.saving = false; })
            .subscribe(res => {
                if (res.code == 0) {
                    this.notify.success('申请已提交！', '');
                    this.success(true);
                } else {
                    this.notify.error('申请异常，请重试！', '');
                    this.success(false);
                }
            });
    }
}
