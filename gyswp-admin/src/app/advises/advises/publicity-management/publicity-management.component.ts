import { Component, Injector } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { Router } from '@angular/router';
import { AdviseService } from 'services';
import { DetailAdviseComponent } from '../detail-advise/detail-advise.component';

@Component({
    moduleId: module.id,
    selector: 'publicity-management',
    templateUrl: 'publicity-management.component.html'
})
export class PublicityManagementComponent extends PagedListingComponentBase<any>{
    constructor(injector: Injector
        , private router: Router
        , private adviseService: AdviseService
    ) {
        super(injector);
    }

    refresh(): void {
        this.getDataPage(this.pageNumber);
    }
    refreshData() {
        this.pageNumber = 1;
        this.refresh();
    }
    /**
     * 重置
     */
    reset() {
        this.refresh();
    }

    protected fetchDataList(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
        let params: any = {};
        params.SkipCount = request.skipCount;
        params.MaxResultCount = request.maxResultCount;
        this.adviseService.getPublicityManagmentList(params)
            .finally(() => {
                finishedCallback();
            })
            .subscribe((result: PagedResultDto) => {
                this.dataList = result.items
                this.totalItems = result.totalCount;
            });
    }

    return() {
        this.router.navigate(['app/advises/advises']);
    }

    //更改状态方法
    changePubStatus(itemId: string, isPublicity: boolean) {
        let msg = "";
        isPublicity == true ? msg = "是否取消公示本条建议?" : msg = "是否公示本条建议?";
        this.message.confirm(
            msg,
            '信息确认',
            (result: boolean) => {
                if (result) {
                    this.adviseService.changePubStatus(itemId).subscribe(() => {
                        this.notify.success('成功完成该操作!');
                        this.refresh();
                    });
                }
            },
        );

    }

    goDetail(itemId: any) {
        this.modalHelper.open(DetailAdviseComponent, { adviseId: itemId }, 'md', {
            nzMask: true
        }).subscribe(isSave => {
            if (isSave) {
            }
        });
    }
}
