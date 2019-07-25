import { Component, Input, Injector } from '@angular/core';
import { ModalComponentBase, PagedResultDto } from '@shared/component-base';
import { NzModalRef, NzModalService } from 'ng-zorro-antd';
import { SupervisionService } from 'services';
import { RevisedListDetailComponent } from '@app/work-criterion/criterion/self-learning/revised-list/revised-list-detail/revised-list-detail.component';

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
    dataList: any[] = [];
    totalItems: number = 0;
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
            .subscribe((result: PagedResultDto) => {
                this.dataList = result.items
                this.totalItems = result.totalCount;
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
                    this.supervisionService.publishCriterionExamineAsync(this.examineId).finally(() => { this.isLoading = false; }).subscribe(res => {
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
