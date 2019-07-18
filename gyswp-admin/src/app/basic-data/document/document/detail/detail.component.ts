import { Component, OnInit, Injector, ViewChild, Inject } from '@angular/core';
import { UploadFile, UploadFilter, NzModalService } from 'ng-zorro-antd';
import { ActivatedRoute, Router } from '@angular/router';
import { BasicDataService } from 'services/basic-data/basic-data.service';
import { DocumentDto, DocAttachment, Attachment } from 'entities';
import { AppConsts } from '@shared/AppConsts';
import { ClauseComponent } from '../../clause/clause.component';
import { AppComponentBase } from '@shared/component-base';
import { DeptUserComponent } from './dept-user/dept-user.component';

@Component({
    moduleId: module.id,
    selector: 'detail',
    templateUrl: 'detail.component.html',
    styleUrls: ['detail.component.less'],
})
export class DetailComponent extends AppComponentBase implements OnInit {
    @ViewChild('clause') clause: ClauseComponent;
    isAllUser = '1';
    userTags = [];
    deptTags = [];
    host = AppConsts.remoteServiceBaseUrl;
    postUrl: string = '/GYSWPFile/DocFilesPostsAsync';
    qrCode = {
        value: '',
        background: 'white',
        backgroundAlpha: 1.0,
        foreground: 'black',
        foregroundAlpha: 1.0,
        level: 'M',
        mime: 'image/png',
        padding: 10,
        size: 230
    };
    fileList: Attachment[] = [];
    filters: UploadFilter[] = [
        {
            name: 'type',
            fn: (fileList: UploadFile[]) => {
                const filterFiles = fileList.filter(w => ~['application/pdf'].indexOf(w.type));
                if (filterFiles.length !== fileList.length) {
                    this.notify.error(`包含文件格式不正确，只支持 pdf 格式`);
                    return filterFiles;
                }
                return fileList;
            }
        }
    ];
    document: DocumentDto = new DocumentDto();
    category = { id: '', name: '' };
    dept = { id: '', name: '' };
    isUpdate = false;
    id: any = '';
    codeStyle = 'none';
    attachment: DocAttachment = new DocAttachment();
    isControl: string;
    isValid: string;

    constructor(injector: Injector
        , private actRouter: ActivatedRoute
        , private router: Router
        , private basicDataService: BasicDataService
        , private modal: NzModalService
    ) {
        super(injector);
        var cid = this.actRouter.snapshot.params['cid'];
        if (cid) {
            this.category.id = cid;
            this.category.name = this.actRouter.snapshot.params['cname'];
        }
        this.dept.id = this.actRouter.snapshot.params['deptId'];
        this.dept.name = this.actRouter.snapshot.params['deptName'];
        this.id = this.actRouter.snapshot.params['id'];
    }

    ngOnInit(): void {
        this.getById();
    }

    save() {
        if (!this.document.id) {
            this.document.categoryId = parseInt(this.category.id);
            // this.document.categoryDesc = this.category.name;
            this.document.deptIds = this.dept.id;
            this.document.isAction = true;
        }
        this.document.publishTime = this.dateFormat(this.document.publishTime);
        this.document.isAllUser = this.isAllUser == '1' ? true : false;

        var control = this.isControl == '5' ? '1' : '2';
        this.document.stamps = control + ',' + this.isValid;

        this.getUserDepts();

        this.basicDataService.createOrUpdateDocumentAsync(this.document)
            .finally(() => { this.saving = false; })
            .subscribe(res => {
                this.notify.info('保存成功！', '');
                if (res.data) {
                    this.document = res.data;
                    this.id = this.document.id;
                    this.qrCode.value = this.document.id;
                }
                this.isUpdate = true;
                this.codeStyle = 'block';
                this.clause.doc = { id: res.data.id, name: res.data.name };
            });
    }

    getById() {
        if (this.id) {
            this.basicDataService.getDocumentByIdAsync(this.id).subscribe(res => {
                this.document = res;

                if (res.stamps && res.stamps.indexOf(',') != -1) {
                    this.isControl = res.stamps.split(',')[0] == '1' ? '5' : '6';
                    this.isValid = res.stamps.split(',')[1];
                }

                if (res.employeeIds || res.employeeDes) {
                    this.setUserDepts(this.document);
                }
                this.isUpdate = true;
                this.isAllUser = res.isAllUser == true ? '1' : '0';
                this.category.id = res.categoryId.toString();
                this.category.name = res.categoryDesc;
                this.qrCode.value = res.id;
                this.codeStyle = 'block';
                this.clause.doc = { id: res.id, name: res.name };
                this.clause.getClauseList();
            });
            this.getAttachmentList();
        }
    }

    return() {
        this.router.navigate(['app/basic/document']);
    }

    uploadFile() {
        // let att = { docId: this.document.id };
        // this.modalHelper
        //     .open(UploadFileComponent, { attachment: att }, 'md', {
        //         nzMask: true,
        //         nzClosable: false,
        //     })
        //     .subscribe(isSave => {
        //         if (isSave) {
        //             this.getAttachments();
        //         }
        //     });
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

    handleChange = (info: { file: UploadFile }): void => {
        if (info.file.status === 'error') {
            this.notify.error('上传文件异常，请重试');
            this.fileList.pop();
        }
        if (this.fileList.length > 1) {
            this.notify.error('只能上传一个附件,请先删除原有附件');
            this.fileList.pop();
        }
        else {
            if (info.file.status === 'done') {
                var res = info.file.response.result;
                if (res.code == 0) {
                    this.attachment.name = res.data.name + res.data.ext;
                    this.attachment.type = 1;
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



    getAttachmentList() {
        let params: any = {};
        params.BllId = this.id;
        this.basicDataService.getCriterionAttachmentById(params).subscribe(r => {
            this.fileList = r;
        })
    }

    uploadDocAttach() {

    }
    controlRadioChange(ngmodel: string) {
        this.isControl = ngmodel;
    }
    validRadioChange(ngmodel: string) {
        this.isValid = ngmodel;
    }
    //#region 人员选择模块
    allUserRadioChange(ngmodel: string) {
        this.isAllUser = ngmodel;
    }

    getUserDepts() {
        if (this.document.isAllUser) {
            this.document.employeeIds = '';
            this.document.employeeDes = '';
            this.document.deptIds = '';
            this.document.deptDesc = '';
        }
        else {
            let userIds = '';
            let userNames = '';
            for (let u of this.userTags) {
                userIds += u.id + ',';
                userNames += u.name + ',';
            }
            this.document.employeeIds = (userIds == '' ? '' : userIds.substr(0, userIds.length - 1));
            this.document.employeeDes = (userNames == '' ? '' : userNames.substr(0, userNames.length - 1));
            let deptIds = '';
            let deptNames = '';
            for (let u of this.deptTags) {
                deptIds += u.id + ',';
                deptNames += u.name + ',';
            }
            this.document.deptIds = (deptIds == '' ? '' : deptIds.substr(0, deptIds.length - 1));
            this.document.deptDesc = (deptNames == '' ? '' : deptNames.substr(0, deptNames.length - 1));
        }
    }

    showDeptUserModel() {
        this.modalHelper
            .open(DeptUserComponent, { deptId: this.dept.id, deptName: this.dept.name, selectedUsers: this.userTags, selectedDepts: this.deptTags }, 'lg', {
                nzMask: true,
                nzClosable: false,
                nzMaskClosable: false,
            })
            .subscribe(isconfirm => {
                if (!isconfirm) {
                    this.setUserDepts(this.document);
                }
            });
    }

    setUserDepts(entity: DocumentDto) {
        this.userTags = entity.getUsers();
        this.deptTags = entity.getDepts();
    }

    handleUserClose(tag: any) {
        var i = 0;
        for (const item of this.userTags) {
            if (item.id == tag.id) {
                this.userTags.splice(i, 1);
                break;
            }
            i++;
        }
    }

    handleDeptClose(tag: any) {
        var i = 0;
        for (const item of this.deptTags) {
            if (item.id == tag.id) {
                this.deptTags.splice(i, 1);
                break;
            }
            i++;
        }
    }
    //#endregion
}