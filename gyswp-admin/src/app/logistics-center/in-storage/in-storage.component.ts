import { Component, Injector } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { Router } from '@angular/router';

@Component({
    moduleId: module.id,
    selector: 'in-storage',
    templateUrl: 'in-storage.component.html'
})
export class InStorageComponent extends PagedListingComponentBase<any>{

    menuTypes = [
        { value: 1, text: '日常点检', selected: true },
        { value: 2, text: '到货单证', selected: false },
        { value: 3, text: '入库扫码', selected: false },
        { value: 4, text: '卷烟入库验收质量问题处理登记表', selected: false },
        { value: 5, text: '入库记录', selected: false },
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
