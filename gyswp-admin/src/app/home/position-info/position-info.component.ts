import { Component, Injector } from '@angular/core';
import { HomeService } from 'services/home/home.service';
import { AppComponentBase } from '@shared/app-component-base';
import { CreatePositionInfoComponent } from './create-position-info/create-position-info.component';
import { PositionInfo } from 'entities/position-info';

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
        let params: any = {};
        this.homeService.getPositionListByCurrentUser().subscribe((result) => {
            this.listOfMapData = result;
        });
    }

    create(): void {
        this.modalHelper
            .open(CreatePositionInfoComponent, { position: this.employee.position, employeeId: this.employee.id, employeeName: this.employee.name }, 'md', {
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
        var id;
        var pId = '';
        var pNo = '';
        if (type == 'child') {
            pId = item.id;
            pNo = item.clauseNo;
        }
        if (type == 'detail') {
            id = item.id;
        }
        // this.modalHelper
        //     .open(ClauseDetailComponent, { docId: this.doc.id, docName: this.doc.name, pId: pId, pNo: pNo, id: id }, 950, {
        //         nzMask: true,
        //         nzClosable: false,
        //         nzMaskClosable: false,
        //     })
        //     .subscribe(isSave => {
        //         if (isSave) {
        //             this.getClauseList();
        //         }
        //     });
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