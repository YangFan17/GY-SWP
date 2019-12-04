import { Component, OnInit, Input, Injector } from '@angular/core';
import { ModalComponentBase } from '@shared/component-base';
import { SupervisionService } from 'services';

@Component({
    moduleId: module.id,
    selector: 'fail-reason',
    templateUrl: 'fail-reason.component.html'
})
export class FailReasonComponent extends ModalComponentBase implements OnInit {
    @Input() id: string;
    @Input() examineDetailId: string;
    failReason: string;
    constructor(injector: Injector
        , private supervisionService: SupervisionService
    ) {
        super(injector);
    }

    ngOnInit(): void {
    }

    save(): void {
        this.saving = true;
        let params: any = {};
        params.Id = this.id;
        params.ExamineDetailId = this.examineDetailId;
        params.FailReason = this.failReason;
        this.supervisionService.changeStatusAndReasonByIdAsync(params)
            .finally(() => { this.saving = false; })
            .subscribe(res => {
                if (res.code == 0) {
                    this.notify.success('保存成功！', '');
                    this.success(true);
                } else {
                    this.notify.error('保存失败，请重试！', '');
                    this.success(false);
                }
            });
    }
}
