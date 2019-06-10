import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { NzModalRef, NzModalService } from 'ng-zorro-antd';
import { ActivatedRoute, Router } from '@angular/router';
import { BasicDataService } from 'services';
import { DocumentDto } from 'entities';
import { NotifyService } from 'abp-ng2-module/dist/src/notify/notify.service';
import { AppConsts } from '@shared/AppConsts';
import { ClauseComponent } from '../../clause/clause.component';
import { AppComponentBase } from '@shared/component-base';
import { DeptUserComponent } from './dept-user/dept-user.component';

@Component({
    moduleId: module.id,
    selector: 'detail',
    templateUrl: 'detail.component.html',
    styleUrls: ['detail.component.less']
})
export class DetailComponent extends AppComponentBase implements OnInit {
    @ViewChild('clause') clause: ClauseComponent;
    isAllUser = '1';
    userTags = [];
    host = AppConsts.remoteServiceBaseUrl;
    actionUrl = this.host + '/GYISMSFile/MeetingRoomPost?fileName=room';
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
    fileList = [
        {
            uid: 1,
            name: 'xxx.png',
            status: 'done',
            response: 'Server Error 500', // custom error message to show
            url: 'http://www.baidu.com/xxx.png'
        },
        {
            uid: 2,
            name: 'yyy.png',
            status: 'done',
            url: 'http://www.baidu.com/yyy.png'
        },
        {
            uid: 3,
            name: 'zzz.png',
            status: 'error',
            response: 'Server Error 500', // custom error message to show
            url: 'http://www.baidu.com/zzz.png'
        }
    ];
    saving: boolean = false;
    document: DocumentDto = new DocumentDto();
    category = { id: '', name: '' };
    dept = { id: '', name: '' };
    isUpdate = false;
    id: any = '';
    codeStyle = 'none';
    attachments = [];

    constructor(injector: Injector
        , private actRouter: ActivatedRoute
        , private router: Router
        , private basicDataService: BasicDataService
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
        this.getAttachments();
    }

    save() {
        if (!this.document.id) {
            this.document.categoryId = parseInt(this.category.id);
            this.document.categoryDesc = this.category.name;
            this.document.deptIds = this.dept.id;
        }
        this.getUserDepts();
        this.document.publishTime = this.dateFormat(this.document.publishTime);
        this.basicDataService.CreateOrUpdateDocumentAsync(this.document)
            .finally(() => { this.saving = false; })
            .subscribe(res => {
                this.notify.info('保存成功！', '');
                if (res.data) {
                    this.document.id = res.data;
                    this.id = this.document.id;
                    this.qrCode.value = this.document.id;
                }
                this.isUpdate = true;
                this.codeStyle = 'block';
                this.clause.doc = { id: res.id, name: res.name };
            });
    }

    getById() {
        if (this.id) {
            this.basicDataService.GetDocumentByIdAsync(this.id).subscribe(res => {
                this.document = res;
                if (res.employeeIds || res.employeeDes) {
                    this.setUserDepts(this.document);
                }
                this.isAllUser = res.isAllUser == true ? '1' : '0';
                this.category.id = res.categoryId.toString();
                this.category.name = res.categoryDesc;
                this.codeStyle = 'block';
                this.qrCode.value = res.id;
                this.codeStyle = 'block';
                this.clause.doc = { id: res.id, name: res.name };
                this.clause.getClauseList();
            });
        }
    }

    getAttachments() {
        // if (this.id) {
        //     var param = { docId: this.id };
        //     this.attachmentService.getListByDocIdAsync(param).subscribe(res => {
        //         this.attachments = res;
        //     });
        // }
    }

    return() {
        this.router.navigate(['app/basic/document']);
    }

    // delete() {
    //     this.confirmModal = this.modal.confirm({
    //         nzContent: '确定是否删除?',
    //         nzOnOk: () => {
    //             this.documentService.delete(this.document.id)
    //                 .finally(() => { this.saving = false; })
    //                 .subscribe(res => {
    //                     this.notify.info('删除成功');
    //                     this.router.navigate(['app/doc/document']);
    //                 });
    //         }
    //     });
    // }

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

    deleteAttachment(itemid) {
        // this.confirmModal = this.modal.confirm({
        //     nzContent: '确定是否删除资料文档?',
        //     nzOnOk: () => {
        //         this.attachmentService.delete(itemid).subscribe(() => {
        //             this.notify.info('删除成功！', '');
        //             this.getAttachments();
        //         });
        //     }
        // });
    }
    uploadDocAttach() {

    }

    //#region 人员选择模块
    allUserRadioChange(ngmodel: string) {
        this.isAllUser = ngmodel;
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

    getUserDepts() {
        if (this.document.isAllUser) {
            this.document.employeeIds = '';
            this.document.employeeDes = '';
            this.document.deptIds = '';
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
        }
    }

    showDeptUserModel() {
        this.modalHelper
            .open(DeptUserComponent, { deptId: this.dept.id, deptName: this.dept.name, selectedUsers: this.userTags }, 'lg', {
                nzMask: true,
                nzClosable: false,
            })
            .subscribe(isconfirm => {
                if (!isconfirm) {
                    this.setUserDepts(this.document);
                }
            });
    }

    setUserDepts(entity: DocumentDto) {
        this.userTags = entity.getUsers();
    }
    //#endregion
}