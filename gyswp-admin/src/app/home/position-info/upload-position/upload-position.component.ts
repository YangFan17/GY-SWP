import { Component, OnInit, Input, Injector } from '@angular/core';
import { ModalFormComponentBase } from '@shared/component-base';
import { BasicDataService } from 'services';
import { UploadFile } from 'ng-zorro-antd';

@Component({
    moduleId: module.id,
    selector: 'upload-position',
    templateUrl: 'upload-position.component.html'
})
export class UploadPositionComponent extends ModalFormComponentBase<any> implements OnInit {
    uploading = false;
    address: string;
    postUrl: string = '/GYSWPFile/PositionFilesTxTPostsAsync';
    constructor(
        injector: Injector
    ) {
        super(injector);
    }

    ngOnInit() {
    }

    handleChange = (info: { file: UploadFile }): void => {
        this.uploading = true;
        if (info.file.status === 'error') {
            this.notify.error('导入工作职责异常，请重试');
            this.uploading = false;
        }
        if (info.file.status === 'done') {
            this.uploading = false;
            var res = info.file.response.result;
            if (res.code == 0) {
                this.notify.success('导入工作职责成功');
                // this.success(true);
                this.modalRef.close();
            } else {
                this.notify.error(res.msg);
            }
        }
    }

    protected submitExecute(finisheCallback: Function): void {
        // let params: any = {};
        // params.path = this.address;
        // // params.deptId = this.deptId;
        // // console.log(params);
        // this.basicDataService.documentReadAsync(params)
        //     .finally(() => { this.saving = false; })
        //     .subscribe(res => {
        //         console.log(res);
        //         if (res.code == 0) {
        //             this.notify.success('批量上传成功', '');
        //             this.success(true);
        //         }
        //         else {
        //             this.notify.error(res.msg, '');
        //         }
        //     });
    }

    protected setFormValues(address: string): void {
        this.setControlVal('address', address);
    }

    protected getFormValues(): void {
        this.address = this.getControlVal('address');
    }
}
