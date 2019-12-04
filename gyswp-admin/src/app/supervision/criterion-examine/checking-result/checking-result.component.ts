import { Component, OnInit, Input, Injector } from '@angular/core';
import { ModalComponentBase } from '@shared/component-base';
import { ExamineResult, ExamineRecord, DocAttachment, Attachment } from 'entities';
import { SupervisionService } from 'services';
import { FailReasonComponent } from './fail-reason/fail-reason.component';

@Component({
    moduleId: module.id,
    selector: 'checking-result',
    templateUrl: 'checking-result.component.html'
})
export class CheckingResultComponent extends ModalComponentBase implements OnInit {
    @Input() id: string;
    // postUrl: string = '/GYSWPFile/DocFilesPostsAsync';
    uploadLoading = false;
    examineResult: ExamineResult = new ExamineResult();
    examineRecord: ExamineRecord = new ExamineRecord();
    attachment: DocAttachment = new DocAttachment();
    attachmentList: Attachment[] = [];

    constructor(injector: Injector
        , private supervisionService: SupervisionService
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.getExamineDetail();
    }

    getExamineDetail() {
        if (this.id) {
            this.supervisionService.getExamineDetailByIdAsync(this.id).subscribe(res => {
                this.examineRecord = res;
                this.getExamineResult();
                this.getAttachmentList();
            })
        }
    }

    getExamineResult() {
        this.supervisionService.getExamineResult(this.id).subscribe(r => {
            this.examineResult = r;
        })
    }

    getAttachmentList() {
        let params: any = {};
        params.BllId = this.id;
        this.supervisionService.getAttachmentListByIdAsync(params).subscribe(r => {
            this.attachmentList = r;
        })
    }

    submit(result: number) {
        this.saving = true;
        let params: any = {};
        params.Id = this.id;
        params.Result = result;
        this.supervisionService.changeStatusByIdAsync(params)
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

    goFailSubmit(): void {
        this.modalHelper
            .open(FailReasonComponent, { id: this.examineResult.id, examineDetailId: this.id }, 'md', {
                nzMask: true,
                nzClosable: false,
                nzMaskClosable: false,
            })
            .subscribe(isSave => {
                if (isSave) {
                    this.success(true);
                }
            });
    }
}