import { Component, OnInit, Injector, Input } from '@angular/core';
import { ModalComponentBase } from '@shared/component-base';
import { WorkCriterionService } from 'services';
import { ExamineResult, ExamineRecord, DocAttachment, Attachment } from 'entities';
import { UploadFile, NzModalRef, NzModalService } from 'ng-zorro-antd';

@Component({
    moduleId: module.id,
    selector: 'result-detail',
    templateUrl: 'result-detail.component.html'
})
export class ResultDetailComponent extends ModalComponentBase implements OnInit {
    @Input() id: string;
    postUrl: string = '/GYSWPFile/ExamineFilesPostsAsync';
    uploadLoading = false;
    examineResult: ExamineResult = new ExamineResult();
    examineRecord: ExamineRecord = new ExamineRecord();
    attachment: DocAttachment = new DocAttachment();
    attachmentList: Attachment[] = [];
    confirmModal: NzModalRef;

    constructor(injector: Injector
        , private workCriterionService: WorkCriterionService
        , private modal: NzModalService
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.getExamineDetail();
    }

    getExamineDetail() {
        if (this.id) {
            this.workCriterionService.getExamineDetailByIdAsync(this.id).subscribe(res => {
                this.examineRecord = res;
                this.getExamineResult();
                this.getAttachmentList();
            })
        }
    }

    getExamineResult() {
        this.workCriterionService.getExamineResult(this.id).subscribe(r => {
            this.examineResult = r;
        })
    }

    getAttachmentList() {
        let params: any = {};
        params.BllId = this.id;
        this.workCriterionService.getAttachmentListByIdAsync(params).subscribe(r => {
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

    submit(): void {
        this.confirmModal = this.modal.confirm({
            nzContent: this.attachmentList.length > 0 ? '保存后将无法修改，是否提交?' : '未上传考核证据，极有可能被判定为不合格，是否提交?',
            nzOnOk: () => {
                this.saving = true;
                this.examineResult.examineDetailId = this.id;
                this.workCriterionService.createExamineResultAsync(this.examineResult)
                    .finally(() => { this.saving = false; })
                    .subscribe(res => {
                        if (res.code == 0) {
                            this.examineResult.id = res.data;
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
                this.attachment.type = 3;
                this.attachment.fileSize = res.data.size;
                this.attachment.path = res.data.url;
                this.attachment.bLL = this.id;
                this.saveAttachment();
            } else {
                this.notify.error(res.msg);
            }
        }
    }
}