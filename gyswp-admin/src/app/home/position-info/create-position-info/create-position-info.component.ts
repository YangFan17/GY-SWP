import { Component, OnInit, Injector, Input } from '@angular/core';
import { ModalComponentBase } from '@shared/component-base/modal-component-base';
import { HomeService } from 'services/home/home.service';
import { PositionInfo } from 'entities/position-info';

@Component({
    moduleId: module.id,
    selector: 'create-position-info',
    templateUrl: 'create-position-info.component.html',
    providers: [HomeService]
})
export class CreatePositionInfoComponent extends ModalComponentBase implements OnInit {
    duties: string;//工作职责
    positionInfo: PositionInfo = new PositionInfo();
    saving: boolean;
    notify: any;

    constructor(
        injector: Injector,
        private homeService: HomeService
    ) {
        super(injector);
    }

    ngOnInit() {
    }

    save(): void {
        this.homeService.createPositionInfo(this.positionInfo)
            .finally(() => { this.saving = false; })
            .subscribe((data) => {
                if (data.code == 0) {
                    this.notify.info('保存成功');
                    this.positionInfo.id = data.data;
                    this.close();
                } else {
                    this.notify.error('保存失败,请重试.');
                }
            });
    }


}
