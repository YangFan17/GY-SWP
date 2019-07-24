import { Component, Injector } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { Router } from '@angular/router';
import { WorkCriterionService } from 'services';
import { ResultFeedbackComponent } from '../my-examine/examine-detail/result-feedback/result-feedback.component';

@Component({
    moduleId: module.id,
    selector: 'my-indicator',
    templateUrl: 'my-indicator.component.html'

})
export class MyIndicatorComponent extends PagedListingComponentBase<any>{
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
        let params: any = {};
        params.SkipCount = request.skipCount;
        params.MaxResultCount = request.maxResultCount;
        this.workCriterionService.getPagedCurrentIndicatorAsync(params)
            .finally(() => {
                finishedCallback();
            })
            .subscribe((result: PagedResultDto) => {
                this.dataList = result.items
                this.totalItems = result.totalCount;
            });
    }

    goDetail(id: string) {
        this.router.navigate(['app/criterion/indicator-detail', id]);
    }

    resultFeedBack(id: string): void {
        this.modalHelper
            .open(ResultFeedbackComponent, { id: id, type: 2 }, 950, {
                nzMask: true,
                nzClosable: false,
                nzMaskClosable: false,
            })
            .subscribe(isSave => {
                if (isSave) {
                    this.refreshData();
                }
            });
    }
}
