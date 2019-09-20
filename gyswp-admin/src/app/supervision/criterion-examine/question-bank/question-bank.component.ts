import { Component, Input, Injector } from '@angular/core';
import { ModalComponentBase } from '@shared/component-base';
import { NzModalRef, NzModalService } from 'ng-zorro-antd';
import { SupervisionService } from 'services';
import { ExamineRecord } from 'entities';

@Component({
    moduleId: module.id,
    selector: 'question-bank',
    templateUrl: 'question-bank.component.html'
})
export class QuestionBankComponent extends ModalComponentBase {
    @Input() examineId: string;
    @Input() title: string = '检查表';
    isLoading: boolean = false;
    confirmModal: NzModalRef;
    dataList: ExamineRecord[] = [];
    endDate: any;
    deletButtonIShow: boolean = false;

    constructor(injector: Injector
        , private supervisionService: SupervisionService
        , private modal: NzModalService
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.getExamineDetailListById();
    }

    getExamineDetailListById() {
        this.isLoading = true;
        let params: any = {};
        params.ExamineId = this.examineId;
        this.supervisionService.getExamineRecordNoPagedByIdAsync(params)
            .finally(() => {
                this.isLoading = false;
            })
            .subscribe((result: ExamineRecord[]) => {
                this.dataList = result
            });
    }

    ISomeOneChecked() {
        if (this.dataList.filter(v => v.checked).length > 0) {
            this.deletButtonIShow = true;
        } else {
            this.deletButtonIShow = false;
        }
    }
    deleteBatch() {
        let deleteNum: number = this.dataList.filter(v => v.checked).length;
        this.confirmModal = this.modal.confirm({
            nzContent: `是否删除当前 ${deleteNum} 项考核记录信息?`,
            nzOnOk: () => {
                let deleteIds: any[] = [];
                this.dataList.filter(v => v.checked).forEach(v => {
                    deleteIds.push(v.id);
                });
                this.supervisionService.deleteBatchExamineAsyne(deleteIds).subscribe(res => {
                    this.notify.success('删除检查项成功！', '');
                    this.getExamineDetailListById();
                    this.deletButtonIShow = false;
                })
            }
        });
    }

    remove(id: string): void {
        if (id) {
            this.confirmModal = this.modal.confirm({
                nzContent: `是否删除当前检查项?`,
                nzOnOk: () => {
                    this.supervisionService.deleteExamineDetailByIdAsync(id).subscribe(res => {
                        this.notify.success('删除检查项成功！', '');
                        this.getExamineDetailListById();
                    });
                }
            });
        }
    }

    publish(): void {
        if (this.examineId) {
            this.isLoading = true;
            this.confirmModal = this.modal.confirm({
                nzContent: `是否发布当前监督检查任务?`,
                nzOnOk: () => {
                    let input: any = {};
                    input.examineId = this.examineId;
                    input.endTime = this.endDate;
                    this.supervisionService.publishCriterionExamineAsync(input).finally(() => { this.isLoading = false; }).subscribe(res => {
                        if (res.code == 0) {
                            this.notify.success('监督检查发布成功！', '');
                            this.success(true);
                        }
                        else {
                            this.notify.error('监督检查发布失败，请重试！', '');
                        }
                    });
                },
                nzOnCancel: () => {
                    this.isLoading = false;
                }
            });
        }
    }
}
