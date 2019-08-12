import { Component, Injector } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { Router } from '@angular/router';

@Component({
    moduleId: module.id,
    selector: 'storage-custody',
    templateUrl: 'storage-custody.component.html',
    styleUrls: ['storage-custody.component.scss']
})
export class StorageCustodyComponent extends PagedListingComponentBase<any>{
    menuTypes = [
        { value: 1, text: '卷烟仓库人员出入登记', selected: true },
        { value: 2, text: '巡查记录', selected: false },
        { value: 3, text: '防霉度夏', selected: false },
        { value: 4, text: '库存卷烟抽查盘点记录', selected: false }
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
