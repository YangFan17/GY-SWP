import { Component, Injector } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { Router } from '@angular/router';
import { AdviseService } from 'services';
import { DetailAdviseComponent } from '../detail-advise/detail-advise.component';

@Component({
    moduleId: module.id,
    selector: 'my-advice',
    templateUrl: 'my-advice.component.html'
})
export class MyAdviceComponent extends PagedListingComponentBase<any>{
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
        this.adviseService.getMyAdviceList(params)
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

    goDetail(itemId: any) {

        this.modalHelper.open(DetailAdviseComponent, { adviseId: itemId }, 'md', {
            nzMask: true
        }).subscribe(isSave => {
            if (isSave) {
            }
        });

    }
}
