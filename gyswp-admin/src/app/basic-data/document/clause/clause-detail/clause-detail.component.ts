import { Clause, Attachment, DocAttachment } from 'entities';
import { ModalComponentBase } from '@shared/component-base';
import { BasicDataService } from 'services';
import { Component, Input, Injector, TemplateRef } from '@angular/core';
import { UploadFile, NzModalService } from 'ng-zorro-antd';
import { AppConsts } from '@shared/AppConsts';

@Component({
    moduleId: module.id,
    selector: 'clause-detail',
    templateUrl: 'clause-detail.component.html',
    styleUrls: ['clause-detail.component.less'],
    providers: [BasicDataService]
})
export class ClauseDetailComponent extends ModalComponentBase {
    @Input() docId: string;
    @Input() docName: string;
    @Input() pId: string;
    @Input() pNo: string;
    @Input() id: string;
    title: string = '条款详情';
    clause: Clause = new Clause();
    postUrl: string = '/GYSWPFile/DocFilesPostsAsync';
    isDelete: boolean;
    deleteFileId: string;//需要删除的文件id
    deleteNotice = false;//是否弹出删除消息对话框
    fileList: Attachment[];
    attachment: DocAttachment = new DocAttachment();
    uploadLoading: boolean;
    isHasAttachments: boolean;
    host = AppConsts.remoteServiceBaseUrl;
    constructor(injector: Injector
        , private basicDataService: BasicDataService
        , private modal: NzModalService
    ) {
        super(injector);
    }
    ngOnInit(): void {
        this.getClauseById();
    }

    getClauseById() {
        if (this.id) {
            this.basicDataService.getClauseByIdAsync(this.id).subscribe(res => {
                this.clause = res;
                // this.pNo = res.
            })
            this.getAttachmentList();
        }
    }

    submit() {
        this.saving = true;
        if (this.pId) {
            this.clause.parentId = this.pId;
        }
        this.clause.documentId = this.docId;
        this.basicDataService.createOrUpdateClauseAsync(this.clause)
            .finally(() => { this.saving = false; })
            .subscribe(res => {
                this.notify.info('保存成功！', '');
                if (res.code == 0) {
                    this.clause.id = res.data;
                    this.success(true);
                }
            });
    }

    getAttachmentList() {
        let params: any = {};
        params.BllId = this.id;

        this.basicDataService.getClauseAttachmentsById(params).subscribe(r => {
            this.fileList = r;
            if (this.fileList.length >= 0) {
                this.isHasAttachments = true;
            }
            this.isHasAttachments = false;
        })
    }

    handleChange = (info: { file: UploadFile }): void => {
        if (info.file.status === 'error') {
            this.notify.error('上传文件异常，请重试');
            this.uploadLoading = false;
        }
        else {
            if (info.file.status === 'done') {
                this.uploadLoading = false;
                var res = info.file.response.result;
                if (res.code == 0) {
                    this.attachment.name = res.data.name;
                    this.attachment.type = 2;
                    this.attachment.fileSize = res.data.size;
                    this.attachment.path = this.host + res.data.url;
                    this.attachment.bLL = this.id;
                    this.saveAttachment();
                } else {
                    this.notify.error(res.msg);
                }
            }
        }
    }

    saveAttachment() {
        this.basicDataService.uploadAttachment(this.attachment).subscribe(res => {
            this.notify.success('上传文件成功');
            this.getAttachmentList();
        })
    }

    deleteAttachment = (file: UploadFile): boolean => {
        this.modal.confirm({
            nzContent: '确定是否删除资料文档?',
            nzOnOk: () => {
                this.basicDataService.deleteAttachmentByIdAsync(file.id).subscribe(() => {
                    this.notify.info('删除成功！', '');
                    this.getAttachmentList();
                    this.isDelete = true;
                });
            },
            nzOnCancel: () => {
                this.isDelete = false;
            }
        });
        return this.isDelete;
    }

    cancel(): void {
        this.isDelete = false;
        this.deleteNotice = true;
    }

    confirm(): void {
        // this.basicDataService.deleteAttachmentByIdAsync(this.deleteFileId).subscribe(res => {
        //     this.notify.success('删除成功');
        //     this.getAttachmentList();
        //     return true;
        // })
        this.isDelete = true;
        this.deleteNotice = true;
    }
}
