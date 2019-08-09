import { Component, Injector } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { Router } from '@angular/router';

@Component({
    moduleId: module.id,
    selector: 'out-storage',
    templateUrl: 'out-storage.component.html'
})
export class OutStorageComponent extends PagedListingComponentBase<any>{
    menuTypes = [
        { value: 1, text: '卷烟出库扫码记录', selected: true },
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
        console.log("hello");

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
