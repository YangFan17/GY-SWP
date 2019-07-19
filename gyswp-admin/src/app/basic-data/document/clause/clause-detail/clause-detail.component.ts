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
    fileList: Attachment[];
    attachment: DocAttachment = new DocAttachment();
    host = AppConsts.remoteServiceBaseUrl;
    constructor(injector: Injector
        , private basicDataService: BasicDataService
        , private modal: NzModalService
    ) {
        super(injector);
    }
    ngOnInit(): void {
        this.getClauseById();
        this.getAttachmentList();
    }

    getClauseById() {
        if (this.id) {
            this.basicDataService.getClauseByIdAsync(this.id).subscribe(res => {
                this.clause = res;
            })
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
        })
    }

    handleChange = (info: { file: UploadFile }): void => {
        if (info.file.status === 'error') {
            this.notify.error('上传文件异常，请重试');
        }
        else {
            if (info.file.status === 'done') {
                var res = info.file.response.result;
                if (res.code == 0) {
                    this.attachment.name = res.data.name + res.data.ext;
                    this.attachment.type = 2;
                    this.attachment.fileSize = res.data.size;
                    this.attachment.path = this.host + res.data.url;
                    this.attachment.bLL = this.id;
                    this.saveAttachment();
                } else {
                    this.notify.error(res.msg);
                    this.fileList.pop();
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
                    this.notify.success('删除成功！', '');
                    this.getAttachmentList();
                    return true;
                });
            }
        });
        return false;
    }
}
