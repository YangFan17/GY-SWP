import { Clause, Attachment, DocAttachment } from 'entities';
import { ModalComponentBase } from '@shared/component-base';
import { BasicDataService } from 'services';
import { Component, Input, Injector, TemplateRef } from '@angular/core';
import { UploadFile, NzModalService, NzModalRef } from 'ng-zorro-antd';
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
    // fileList: Attachment[] = [];
    attachmentList: Attachment[] = [];
    attachment: DocAttachment = new DocAttachment();
    newAttachment: DocAttachment[] = [];
    host = AppConsts.remoteServiceBaseUrl;
    confirmModal: NzModalRef;

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
        if (this.id) {
            this.newAttachment = [];
        }
        this.basicDataService.createOrUpdateClauseAsync(this.clause, this.newAttachment)
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
            this.attachmentList = r;
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
                    // this.fileList.forEach(element => {
                    //     if (info.file.uid == element.uid) {
                    //         element.url = this.host + res.data.url;
                    //     }
                    // });
                    this.attachment.name = res.data.name + res.data.ext;
                    this.attachment.type = 2;
                    this.attachment.fileSize = res.data.size;
                    this.attachment.path = res.data.url;
                    this.attachment.bLL = this.id;
                    if (this.id) {
                        this.saveAttachment();
                    } else {
                        const showAttach: Attachment[] = [];
                        showAttach.push(...this.attachmentList);
                        let item: Attachment = new Attachment();
                        item.uid = info.file.uid;
                        item.url = this.host + this.attachment.path;
                        item.name = this.attachment.name;
                        showAttach.push(item);
                        this.attachmentList = showAttach;
                        // console.log(this.attachmentList);
                        let tmpItem: DocAttachment = new DocAttachment();
                        tmpItem.uId = info.file.uid;
                        tmpItem.name = this.attachment.name;
                        tmpItem.type = this.attachment.type;
                        tmpItem.fileSize = this.attachment.fileSize;
                        tmpItem.path = this.attachment.path;
                        this.newAttachment.push(tmpItem);
                        // console.log(this.newAttachment);
                    }
                } else {
                    this.notify.error(res.msg);
                    // this.fileList.pop();
                }
            }
        }
    }

    saveAttachment() {
        this.basicDataService.uploadAttachment(this.attachment).subscribe(res => {
            if (res.code == 0) {
                // this.fileList.pop();
                // const temp: Attachment[] = this.fileList;
                // console.log(this.fileList);
                // console.log(temp);
                // temp.push(Attachment.fromJS(res.data));
                // this.fileList = temp;
                // console.log(this.fileList);
                this.notify.success('上传文件成功');
                this.getAttachmentList();
            } else {
                this.notify.error('上传文件异常，请重试');
            }
        })
    }

    // deleteAttachment = (file: UploadFile): boolean => {
    //     this.modal.confirm({
    //         nzContent: '确定是否删除资料文档?',
    //         nzOnOk: () => {
    //             this.basicDataService.deleteAttachmentByIdAsync(file.id).subscribe(() => {
    //                 let tflist = JSON.parse(JSON.stringify(this.fileList));
    //                 tflist.pop();
    //                 this.fileList = tflist;
    //                 this.notify.success('删除成功！', '');
    //                 // this.getAttachmentList();
    //                 return true;
    //             });
    //         }
    //     });
    //     return false;
    // }
    deleteAttachment(id: string): void {
        this.confirmModal = this.modal.confirm({
            nzContent: `是否删除当前附件?`,
            nzOnOk: () => {
                if (this.id) {
                    this.basicDataService.deleteAttachmentByIdAsync(id).subscribe(res => {
                        this.notify.success('删除成功');
                        this.getAttachmentList();
                    });
                }
                else {
                    let tflist: Attachment[] = Attachment.fromJSArray(JSON.parse(JSON.stringify(this.attachmentList)));
                    let i = 0;
                    tflist.forEach(v => {
                        if (v.uid == id) {
                            tflist.splice(i, 1);
                            let ii = 0;
                            this.newAttachment.forEach(vv => {
                                if (vv.uId == id) {
                                    this.newAttachment.splice(ii, 1);
                                    return;
                                }
                                ii++;
                            });
                            return;
                        }
                        i++;
                    });
                    this.attachmentList = tflist;
                }
            }
        });
    }
}
