import { Component, Injector } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { Router } from '@angular/router';
import { LogisticService } from 'services';
import { addDays } from 'date-fns';

@Component({
    moduleId: module.id,
    selector: 'in-storage-bill',
    templateUrl: 'in-storage-bill.component.html',
    styleUrls: ['in-storage-bill.component.scss']
})
export class InStorageBillComponent extends PagedListingComponentBase<any>{
    search = { beginTime: '', endTime: '' };
    timeFormat = 'yyyy-MM-dd';
    // dateRange = [addDays(new Date(), -1 * (new Date()).getDay() + 1), new Date()];
    dateRange: Date[] = [];
    constructor(injector: Injector
        , private logisticService: LogisticService
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
        this.search.beginTime = null;
        this.search.endTime = null;
        this.dateRange = [];
        // this.dateRange = [addDays(new Date(), -1 * (new Date()).getDay() + 1), new Date()]
        this.refreshData();
    }

    protected fetchDataList(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
        let params: any = {};
        params.BeginTime = this.search.beginTime;
        params.EndTime = this.search.endTime;
        params.SkipCount = request.skipCount;
        params.MaxResultCount = request.maxResultCount;
        this.isTableLoading = false;
        this.logisticService.getPagedInStorageBillAsync(params)
            .finally(() => {
                finishedCallback();
            })
            .subscribe((result: PagedResultDto) => {
                this.dataList = result.items
                this.totalItems = result.totalCount;
            });
    }

    changeTime(times) {
        if (times.length != 0) {
            this.search.beginTime = this.dateFormat(this.dateRange[0]);
            this.search.endTime = this.dateFormat(this.dateRange[1]);
        } else {
            this.search.beginTime = null;
            this.search.endTime = null;
        }
    }

    setSearchTime() {
        this.search.beginTime = this.dateRange[0].getFullYear() + '-' + (this.dateRange[0].getMonth() + 1) + '-' + this.dateRange[0].getDate();
        this.search.endTime = this.dateRange[1].getFullYear() + '-' + (this.dateRange[1].getMonth() + 1) + '-' + this.dateRange[1].getDate();
    }
}