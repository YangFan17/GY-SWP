import { Component, Injector } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { Router, ActivatedRoute } from '@angular/router';
import { SupervisionService } from 'services';

@Component({
    moduleId: module.id,
    selector: 'qg-examine-record',
    templateUrl: 'qg-examine-record.component.html'
})
export class QgExamineRecordComponent extends PagedListingComponentBase<any>{
    constructor(injector: Injector
        , private router: Router
        , private actRouter: ActivatedRoute
        , private supervisionService: SupervisionService
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
        this.supervisionService.getPagedCriterionRecordByQGAdmin(params)
            .finally(() => {
                finishedCallback();
            })
            .subscribe((result: PagedResultDto) => {
                this.dataList = result.items
                this.totalItems = result.totalCount;
            });
    }

    goDetail(id: string) {
        this.router.navigate(['app/supervision/record-detail', id]);
    }

    return() {
        this.router.navigate(['app/supervision/supervision']);
    }
}
