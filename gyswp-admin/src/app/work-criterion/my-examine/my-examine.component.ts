import { Component, Injector } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { Router } from '@angular/router';
import { WorkCriterionService } from 'services';

@Component({
    moduleId: module.id,
    selector: 'my-examine',
    templateUrl: 'my-examine.component.html'
})
export class MyExamineComponent extends PagedListingComponentBase<any>{
    // listOfParentData: any[] = [];
    // listOfChildrenData: any[] = [];

    constructor(injector: Injector
        , private router: Router
        , private workCriterionService: WorkCriterionService
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
        this.workCriterionService.getPagedExamineByCurrentIdAsync(params)
            .finally(() => {
                finishedCallback();
            })
            .subscribe((result: PagedResultDto) => {
                this.dataList = result.items
                this.totalItems = result.totalCount;
            });
    }

    goDetail(id: string) {
        this.router.navigate(['app/criterion/examine-detail', id]);
    }
}
