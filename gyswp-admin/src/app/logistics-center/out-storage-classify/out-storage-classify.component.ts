import { Component, Injector } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { Router } from '@angular/router';

@Component({
    moduleId: module.id,
    selector: 'out-storage-classify',
    templateUrl: 'out-storage-classify.component.html'
})
export class OutStorageClassifyComponent extends PagedListingComponentBase<any>{
    menuTypes = [
        { value: 1, text: '卷烟分拣领用出库单', selected: true },
        { value: 2, text: '班组安全活动', selected: false },
        { value: 3, text: '分拣车间残损卷烟调换表', selected: false }
    ];
    constructor(injector: Injector
        , private router: Router
    ) {
        super(injector);
    }

    ngOnInit() {
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
        // let params: any = {};
        // params.SkipCount = request.skipCount;
        // params.MaxResultCount = request.maxResultCount;
        // this.workCriterionService.getPagedCurrentIndicatorAsync(params)
        //     .finally(() => {
        //         finishedCallback();
        //     })
        //     .subscribe((result: PagedResultDto) => {
        //         this.dataList = result.items
        //         this.totalItems = result.totalCount;
        //     });
    }
}
