import { Component, Input, Injector } from '@angular/core';
import { ModalComponentBase } from '@shared/component-base';
import { NzModalRef, NzModalService } from 'ng-zorro-antd';
import { SupervisionService } from 'services';

@Component({
    moduleId: module.id,
    selector: 'publish-confirm',
    templateUrl: 'publish-confirm.component.html',
})
export class PublishConfirmComponent extends ModalComponentBase {
    @Input() indicatorIds: string[];
    @Input() num: number;
    @Input() title: string = '检查表';
    isLoading: boolean = false;
    confirmModal: NzModalRef;
    endDate: any;

    constructor(injector: Injector
        , private supervisionService: SupervisionService
        , private modal: NzModalService
    ) {
        super(injector);
    }

    publish(): void {
        this.isLoading = true;
        let input: any = {};
        input.indicatorList = this.indicatorIds;
        input.endTime = this.endDate;
        this.supervisionService.publishIndicatorById(input)
            .finally(() => { this.isLoading = false; })
            .subscribe(res => {
                if (res.code == 0) {
                    this.notify.success('发布成功，请点击‘记录’查看详情', '');
                    this.success(true);
                }
                else if (res.code == 999) {
                    this.notify.error(res.msg, '');
                }
                else {
                    this.notify.error('发布失败，请重试！', '');
                }
            });
    }
}
