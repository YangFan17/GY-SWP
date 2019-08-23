import { Component, OnInit, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/component-base';
import { Indicators, IndicatorShowDto } from 'entities';
import { NzModalRef, NzModalService } from 'ng-zorro-antd';
import { ActivatedRoute, Router } from '@angular/router';
import { WorkCriterionService } from 'services';
import { IndicatorsDetail } from 'entities/indicators-detail';

@Component({
    moduleId: module.id,
    selector: 'indicator-detail',
    templateUrl: 'indicator-detail.component.html'
})
export class IndicatorDetailComponent extends AppComponentBase implements OnInit {
    id: string;
    indicator: IndicatorShowDto = new IndicatorShowDto();
    indicatorDetail: IndicatorsDetail = new IndicatorsDetail();
    confirmModal: NzModalRef;
    constructor(injector: Injector
        , private actRouter: ActivatedRoute
        , private router: Router
        , private workCriterionService: WorkCriterionService
        , private modal: NzModalService
    ) {
        super(injector);
        this.id = this.actRouter.snapshot.params['id'];
    }

    ngOnInit(): void {
        if (this.id) {
            this.getById();
        }
    }

    save(): void {
        this.confirmModal = this.modal.confirm({
            nzContent: '保存后将无法修改，是否提交?',
            nzOnOk: () => {
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

                this.workCriterionService.changeStatusByIdAsync(input)
                    .finally(() => { this.saving = false; })
                    .subscribe(res => {
                        this.notify.success('保存成功！', '');
                        if (res.code == 0) {
                            this.return();
                        }
                    });
            }
        });
    }

    getById() {
        if (this.id) {
            this.workCriterionService.getIndicatorByIdAsync(this.id).subscribe(res => {
                this.indicator = res;
            });
        }
    }

    return() {
        this.router.navigate(['app/criterion/my-indicator']);
    }
}