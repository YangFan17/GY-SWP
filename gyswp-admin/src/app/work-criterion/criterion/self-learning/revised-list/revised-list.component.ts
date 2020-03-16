import { Component, Injector, Input } from '@angular/core';
import { ModalComponentBase } from '@shared/component-base';
import { WorkCriterionService } from 'services';
import { ClauseRevision, DocAttachment, Attachment } from 'entities';
import { NzModalRef, NzModalService, UploadFile } from 'ng-zorro-antd';
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

    //文件上传
    attachment: DocAttachment = new DocAttachment();
    attachmentList: Attachment[] = [];
    postUrl: string = '/GYSWPFile/RevisedFilePostsAsync';
    uploadLoading = false;

    constructor(injector: Injector
        , private workCriterionService: WorkCriterionService
        , private modal: NzModalService
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.getClauseRevisionListById();
        this.getAttachmentList();
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
            this.saving = true;
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
                },
                nzOnCancel: () => {
                    this.saving = false;
                }
            });
        }
    }

    beforeUpload = (file: UploadFile): boolean => {
        if (this.uploadLoading) {
            this.notify.info('正在上传中');
            return false;
        }
        this.uploadLoading = true;
        return true;
    }

    handleChange = (info: { file: UploadFile }): void => {
        if (info.file.status === 'error') {
            this.notify.error('上传文件异常，请重试');
            this.uploadLoading = false;
        }
        if (info.file.status === 'done') {
            this.uploadLoading = false;
            var res = info.file.response.result;
            if (res.code == 0) {
                this.attachment.name = res.data.name + res.data.ext;
                this.attachment.type = 4;
                this.attachment.fileSize = res.data.size;
                this.attachment.path = res.data.url;
                this.attachment.bLL = this.applyId;
                this.saveAttachment();
            } else {
                this.notify.error(res.msg);
            }
        }
    }

    getAttachmentList() {
        let params: any = {};
        params.BllId = this.applyId;
        this.workCriterionService.getRevisedAttachmentByIdAsync(params).subscribe(r => {
            this.attachmentList = r;
        })
    }

    saveAttachment() {
        this.workCriterionService.uploadAttachment(this.attachment).subscribe(res => {
            if (res.code == 0) {
                this.notify.success('上传文件成功');
                this.getAttachmentList();
            } else {
                this.notify.error('上传文件异常，请重试');
            }

        })
    }

    deleteAttachment(id: string): void {
        this.confirmModal = this.modal.confirm({
            nzContent: `是否删除当前附件?`,
            nzOnOk: () => {
                this.workCriterionService.deleteAttachmentByIdAsync(id).subscribe(res => {
                    this.notify.success('删除成功');
                    this.getAttachmentList();
                })
            }
        });
    }
}
