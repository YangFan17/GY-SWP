import { Component, OnInit, Input, Injector } from '@angular/core';
import { ModalComponentBase } from '@shared/component-base';
import { ExamineFeedback } from 'entities';
import { WorkCriterionService } from 'services';
import { NzModalRef, NzModalService } from 'ng-zorro-antd';

@Component({
    moduleId: module.id,
    selector: 'result-feedback',
    templateUrl: 'result-feedback.component.html'
})
export class ResultFeedbackComponent extends ModalComponentBase implements OnInit {
    @Input() id: string; //examineDetailId
    @Input() type: number; //考核类型
    confirmModal: NzModalRef;
    courseTypes = [
        { label: '人', value: '1', checked: false },
        { label: '机', value: '2', checked: false },
        { label: '料', value: '3', checked: false },
        { label: '法', value: '4', checked: false },
        { label: '环', value: '5', checked: false },
    ];
    examineFeedback: ExamineFeedback = new ExamineFeedback();
    splitCodes: string[];

    constructor(injector: Injector
        , private workCriterionService: WorkCriterionService
        , private modal: NzModalService
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.getExamineFeedback();
    }

    getExamineFeedback() {
        if (this.id) {
            this.workCriterionService.getExamineFeedbackByIdAsync(this.id).subscribe(res => {
                this.examineFeedback = res;
                if (res.courseType) {
                    this.splitCodes = res.courseType.split(',');
                    let i: number = 0;
                    this.courseTypes.forEach(v => {
                        if (v.value == this.splitCodes[i]) {
                            v.checked = true;
                            if (i < this.splitCodes.length) {
                                i++;
                            }
                        }
                    }
                    );
                }
            })
        }
    }

    submit(): void {
        this.confirmModal = this.modal.confirm({
            nzContent: `保存后将无法修改，是否提交?`,
            nzOnOk: () => {
                this.saving = true;
                this.examineFeedback.businessId = this.id;
                this.examineFeedback.type = this.type;
                var filter = this.courseTypes.filter(v => v.checked == true);
                this.examineFeedback.courseType = filter.map(v => {
                    return v.value;
                }).join(',');
                this.workCriterionService.createExamineFeedbackAsync(this.examineFeedback)
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
        });
    }
}