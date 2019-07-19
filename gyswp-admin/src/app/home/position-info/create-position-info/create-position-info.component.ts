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
    @Input() id: string;
    duties: string;//工作职责
    positionInfo: PositionInfo = new PositionInfo();
    constructor(injector: Injector,
        private homeService: HomeService
    ) {
        super(injector);
    }

    ngOnInit() {
        if (this.id) {
            this.positionInfo.id = this.id;
            console.log(this.id);
            this.homeService.getPositionInfoById(this.id).subscribe((data) => {
                this.positionInfo = data;
            });
        }
    }

    save(): void {
        this.homeService.createPositionInfo(this.positionInfo)
            .finally(() => { this.saving = false; })
            .subscribe((data) => {
                if (data.code == 0) {
                    this.notify.success('保存成功');
                    this.positionInfo.id = data.data;
                    this.success(true);
                } else {
                    this.notify.error('保存失败,请重试.');
                    this.success(false);
                }
            });
    }
}
