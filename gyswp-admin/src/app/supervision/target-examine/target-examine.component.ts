import { Component, Injector } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { SupervisionService } from 'services';
import { Router } from '@angular/router';

@Component({
    moduleId: module.id,
    selector: 'target-examine',
    templateUrl: 'target-examine.component.html'
})
export class TargetExamineComponent extends PagedListingComponentBase<any>{
    search: any = { cycleTime: 0 };
    cycleTimes = [
        { value: 0, text: '全部', selected: true },
        { value: 2, text: '季度', selected: false },
        { value: 1, text: '年度', selected: false },
    ]
    constructor(injector: Injector
        , private router: Router
        , private supervisionService: SupervisionService
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.refreshData();
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
        this.search = { keyWord: '', cycleTime: 0 };
        this.refresh();
    }

    protected fetchDataList(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
        let params: any = {};
        params.SkipCount = request.skipCount;
        params.MaxResultCount = request.maxResultCount;
        // params.KeyWord = this.search.keyWord;
        if (this.search.cycleTime != 0) {
            params.cycleTime = this.search.cycleTime;
        }
        this.supervisionService.getIndicatorListAsync(params)
            .finally(() => {
                finishedCallback();
            })
            .subscribe((result: PagedResultDto) => {
                this.dataList = result.items
                this.totalItems = result.totalCount;
            });
    }

    create() {
        this.router.navigate(['app/supervision/indicator-detail']);
    }

    goDetail(id: string) {
        this.router.navigate(['app/supervision/indicator-detail', id]);
    }
}
