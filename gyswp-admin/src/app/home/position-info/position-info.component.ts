import { Component, Injector } from '@angular/core';
import { HomeService } from 'services/home/home.service';
import { AppComponentBase } from '@shared/app-component-base';
import { CreatePositionInfoComponent } from './create-position-info/create-position-info.component';
import { PositionInfo } from 'entities/position-info';
import { AddDocumentComponent } from './add-document/add-document.component';
import { Router } from '@angular/router';
import { NzModalRef, NzModalService } from 'ng-zorro-antd';

@Component({
    moduleId: module.id,
    selector: 'position-info',
    templateUrl: 'position-info.component.html',
    providers: [HomeService]
})
export class PositionInfoComponent extends AppComponentBase {
    listOfMapData: PositionInfo[] = [];
    confirmModal: NzModalRef;

    constructor(
        injector: Injector
        , private homeService: HomeService
        , private router: Router
        , private modal: NzModalService
    ) {
        super(injector);
    }
    ngOnInit(): void {
        this.getPositionInfoList();
    }

    getPositionInfoList() {
        this.homeService.getPositionListByCurrentUser().subscribe((result) => {
            this.listOfMapData = result;
        });
    }

    create(): void {
        this.modalHelper
            .open(CreatePositionInfoComponent, {}, 'md', {
                nzMask: true,
                nzClosable: false,
                nzMaskClosable: false,
            })
            .subscribe(isSave => {
                if (isSave) {
                    this.getPositionInfoList();
                }
            });
    }

    editPosition(id: string): void {
        this.modalHelper
            .open(CreatePositionInfoComponent, { id: id }, 'md', {
                nzMask: true,
                nzClosable: false,
                nzMaskClosable: false,
            })
            .subscribe(isSave => {
                if (isSave) {
                    this.getPositionInfoList();
                }
            });
    }

    showDetail(id: string): void {
        this.modalHelper
            .open(AddDocumentComponent, { positionInfoId: id }, 'md', {
                nzMask: true,
                nzClosable: false,
                nzMaskClosable: false,
            })
            .subscribe(isSave => {
                if (isSave) {
                    this.getPositionInfoList();
                }
            });
    }

    delete(id: string): void {
        if (id) {
            this.confirmModal = this.modal.confirm({
                nzContent: `是否删除当前工作职责?`,
                nzOnOk: () => {
                    this.homeService.deletePositionById(id).subscribe(res => {
                        this.notify.success('删除成功！', '');
                        this.getPositionInfoList();
                    });
                }
            });
        }
    }

    return() {
        this.router.navigate(['app/home']);
    }
}