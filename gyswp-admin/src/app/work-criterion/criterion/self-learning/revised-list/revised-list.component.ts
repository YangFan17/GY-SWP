import { Component, Injector, Input } from '@angular/core';
import { ModalComponentBase } from '@shared/component-base';
import { WorkCriterionService } from 'services';
import { ClauseRevision } from 'entities';
import { NzModalRef, NzModalService } from 'ng-zorro-antd';
import { RevisedListDetailComponent } from './revised-list-detail/revised-list-detail.component';

@Component({
    moduleId: module.id,
    selector: 'revised-list',
    templateUrl: 'revised-list.component.html',
    styleUrls: ['revised-list.component.less']
})
export class RevisedListComponent extends ModalComponentBase {
    @Input() docId: string;
    @Input() applyId: string;
    dataList: ClauseRevision[] = [];
    dataCount = { total: 0, cnumber: 0, unumber: 0, dnumber: 0 };
    isLoading: boolean = false;
    confirmModal: NzModalRef;

    constructor(injector: Injector
        , private workCriterionService: WorkCriterionService
        , private modal: NzModalService
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.getClauseRevisionListById();
    }

    getClauseRevisionListById() {
        let params: any = {};
        params.ApplyInfoId = this.applyId;
        params.DocumentId = this.docId;
        this.isLoading = true;
        this.workCriterionService.getClauseRevisionListById(params).finally(() => {
            this.isLoading = false;
        }).subscribe((result) => {
            this.dataList = result.list;
            this.dataCount = result.count;
        });
    }

    edit(item: any, type?: string): void {
        if (this.docId) {
            var id;
            var pId = '';
            var pNo = '';
            if (type == 'child') {
                pId = item.id;
                pNo = item.clauseNo;
            }
            if (type == 'detail') {
                id = item.id;
            }
            this.modalHelper
                .open(RevisedListDetailComponent, { docId: this.docId, pId: pId, pNo: pNo, id: id, applyId: this.applyId, revisionType: item.revisionType }, 950, {
                    nzMask: true,
                    nzClosable: false,
                    nzMaskClosable: false,
                })
                .subscribe(isSave => {
                    if (isSave) {
                        this.getClauseRevisionListById();
                    }
                });
        }
    }

    remove(id: string): void {
        if (id) {
            this.confirmModal = this.modal.confirm({
                nzContent: `是否移除当前修项?`,
                nzOnOk: () => {
                    this.workCriterionService.removeRevisionById(id).subscribe(res => {
                        if (res.code == 0) {
                            this.notify.success('移除成功！', '');
                            this.getClauseRevisionListById();
                        }
                        else {
                            this.notify.warn('请确保当前条款下无子项条款后再进行移除！', '');
                        }
                    });
                }
            });
        }
    }

    saveRevised(): void {
        if (this.applyId) {
            this.confirmModal = this.modal.confirm({
                nzContent: `是否提交当前所有操作内容?`,
                nzOnOk: () => {
                    this.workCriterionService.saveRevised(this.applyId, this.docId).finally(() => { this.saving = false; }).subscribe(res => {
                        if (res.code == 0) {
                            this.notify.success('制修订申请提交成功！', '');
                            this.success(true);
                        }
                        else {
                            this.notify.error('制修订申请提交失败，请重试！', '');
                        }
                    });
                }
            });
        }
    }
}
