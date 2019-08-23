import { Component, OnInit, Input, Injector } from '@angular/core';
import { ModalComponentBase } from '@shared/component-base';
import { IndicatorShowDto } from 'entities';
import { SupervisionService } from 'services';
import { IndicatorsDetail } from 'entities/indicators-detail';

@Component({
    moduleId: module.id,
    selector: 'full-in-result',
    templateUrl: 'full-in-result.component.html'
})
export class FullInResultComponent extends ModalComponentBase implements OnInit {
    @Input() id: string;
    indicator: IndicatorShowDto = new IndicatorShowDto();
    indicatorDetail: IndicatorsDetail = new IndicatorsDetail();
    constructor(injector: Injector
        , private supervisionService: SupervisionService
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.getById();
    }

    getById() {
        if (this.id) {
            this.supervisionService.getIndicatorDetailByIdAsync(this.id).subscribe(res => {
                this.indicator = res;
            });
        }
    }

    save(): void {
        this.saving = true;
        let input: any = {};
        input.id = this.indicator.indicatorDetailId;
        input.actualValue = this.indicatorDetail.actualValue;
        if (this.indicator.achieveType === 1) { // 大于 = 1,
            if (this.indicatorDetail.actualValue > this.indicator.expectedValue) {
                input.status = 2;
            }
            else {
                input.status = 3;
            }
        } else if (this.indicator.achieveType === 2) {// 大于等于 = 2,
            if (this.indicatorDetail.actualValue >= this.indicator.expectedValue) {
                input.status = 2;
            }
            else {
                input.status = 3;
            }
        } else if (this.indicator.achieveType === 3) {// 小于 = 3,
            if (this.indicatorDetail.actualValue < this.indicator.expectedValue) {
                input.status = 2;
            }
            else {
                input.status = 3;
            }
        } else if (this.indicator.achieveType === 4) {// 小于等于 = 4
            if (this.indicatorDetail.actualValue <= this.indicator.expectedValue) {
                input.status = 2;
            }
            else {
                input.status = 3;
            }
        } else {
            if (this.indicatorDetail.actualValue === this.indicator.expectedValue) {
                input.status = 2;
            }
            else {
                input.status = 3;
            }
        }

        this.supervisionService.changeIndicatorStatusByIdAsync(input)
            .finally(() => { this.saving = false; })
            .subscribe(res => {
                this.notify.success('保存成功！', '');
                if (res.code == 0) {
                    this.success(true);
                } else {
                    this.success(false);
                }
            });
    }
}
