import { Component, Injector } from '@angular/core';
import { HomeService } from 'services/home/home.service';
import { AppComponentBase } from '@shared/app-component-base';
import { CreatePositionInfoComponent } from './create-position-info/create-position-info.component';
import { PositionInfo } from 'entities/position-info';
import { AddDocumentComponent } from './add-document/add-document.component';

@Component({
    moduleId: module.id,
    selector: 'position-info',
    templateUrl: 'position-info.component.html',
    providers: [HomeService]
})
export class PositionInfoComponent extends AppComponentBase {
    employee: any = { id: '5896512826844512', name: '赵康辰', position: '科长' };
    listOfMapData: PositionInfo[] = [];

    constructor(
        injector: Injector,
        private homeService: HomeService
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

    showDetail(item?: any, type?: string): void {
        this.modalHelper
            .open(AddDocumentComponent, { positionInfoId: item.id }, 950, {
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

    // deleteClause(item: Clause): void {
    //     this.confirmModal = this.modal.confirm({
    //         nzContent: `是否删除当前条款[条款编号：${item.clauseNo}]?`,
    //         nzOnOk: () => {
    //             this.basicDataService.deleteClauseById(item.id).subscribe(res => {
    //                 if (res.code == 0) {
    //                     this.notify.info('删除成功！', '');
    //                     this.getClauseList();
    //                 } else {
    //                     this.notify.warn('请确保当前条款下无子项条款后再删除！', '');
    //                 }
    //             });
    //         }
    //     });
    // }
}