import { Component, Injector } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { Router } from '@angular/router';
import { addDays } from 'date-fns';
import { LogisticService } from 'services';
import { AppConsts } from '@shared/AppConsts';

@Component({
    moduleId: module.id,
    selector: 'entry-exit-registration',
    templateUrl: 'entry-exit-registration.component.html',
    styleUrls: ['entry-exit-registration.component.scss']
})
export class EntryExitRegistrationComponent extends PagedListingComponentBase<any>{
    exportLoading = false;
    search = { beginTime: '', endTime: '' };
    timeFormat = 'yyyy-MM-dd';
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
        this.refreshData();
    }

    protected fetchDataList(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
        let params: any = {};
        //console.log(this.search);

        params.BeginTime = this.search.beginTime;
        params.EndTime = this.search.endTime;
        params.SkipCount = request.skipCount;
        params.MaxResultCount = request.maxResultCount;
        this.isTableLoading = false;
        this.logisticService.getPagedEntryExitRegistrationAsync(params)
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

    export() {
        this.exportLoading = true;
        let params: any = {};
        params.BeginTime = this.search.beginTime;
        params.EndTime = this.search.endTime;
        this.logisticService.exportEntryExitRegistration(params).subscribe((data => {
            if (data.code == 0) {
                var url = AppConsts.remoteServiceBaseUrl + data.data;
                document.getElementById('exportUrl').setAttribute('href', url);
                document.getElementById('btnExportHref').click();
            } else {
                this.notify.error(data.msg);
            }
            this.exportLoading = false;
        }));
    }
}