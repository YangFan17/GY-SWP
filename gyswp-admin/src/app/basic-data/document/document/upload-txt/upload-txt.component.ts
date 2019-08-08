import { Component, Input, OnInit, Injector } from '@angular/core';
import { UploadFile } from 'ng-zorro-antd';
import { ModalFormComponentBase } from '@shared/component-base';
import { Category } from 'entities';
import { BasicDataService } from 'services';
import { Validators } from '@angular/forms';

@Component({
    moduleId: module.id,
    selector: 'upload-txt',
    templateUrl: 'upload-txt.component.html'
})
export class UploadTxtComponent extends ModalFormComponentBase<any> implements OnInit {
    @Input() categoryId: any;
    @Input() deptId: any;
    @Input() categoryName: any;
    uploading = false;
    address: string;
    postUrl: string = '';
    constructor(
        injector: Injector
        , private basicDataService: BasicDataService
    ) {
        super(injector);
        this.postUrl = '/GYSWPFile/DocFilesTxTPostsAsync?categoryId=' + this.categoryId;
    }

    ngOnInit() {
        this.postUrl = '/GYSWPFile/DocFilesTxTPostsAsync?categoryId=' + this.categoryId;
        // this.validateForm = this.formBuilder.group({
        //     address: ['', [Validators.required]]
        // });
        // this.setFormValues(this.address);
    }

    handleChange = (info: { file: UploadFile }): void => {
        this.uploading = true;
        if (info.file.status === 'error') {
            this.notify.error('上传文件异常，请重试');
            this.uploading = false;
        }
        if (info.file.status === 'done') {
            this.uploading = false;
            var res = info.file.response.result;
            if (res.code == 0) {
                this.notify.success('上传文件成功');
                // this.success(true);
                this.modalRef.close();
            } else {
                this.notify.error(res.msg);
            }
        }
    }

    protected submitExecute(finisheCallback: Function): void {
        let params: any = {};
        params.path = this.address;
        params.categoryId = this.categoryId;
        // params.deptId = this.deptId;
        // console.log(params);
        this.basicDataService.documentReadAsync(params)
            .finally(() => { this.saving = false; })
            .subscribe(res => {
                this.notify.success('批量上传成功', '');
                this.success(true);
            });
    }

    protected setFormValues(address: string): void {
        this.setControlVal('address', address);
    }

    protected getFormValues(): void {
        this.address = this.getControlVal('address');
    }
}
