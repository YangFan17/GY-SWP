import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/component-base';
import { SuperviseService } from 'services';
import { addDays } from 'date-fns';
import { AppConsts } from '@shared/AppConsts';

@Component({
    moduleId: module.id,
    selector: 'supervise-summary',
    templateUrl: 'supervise-summary.component.html',
    providers: [SuperviseService]
})
export class SuperviseSummaryComponent extends AppComponentBase implements OnInit {
    exportLoading = false;
    dateRange = [addDays(new Date(), -1 * (new Date()).getDay() + 1), new Date()];
    dataList: any[];
    search = { beginTime: '', endTime: '' };
    isTableLoading = false;

    constructor(injector: Injector,
        private superviseService: SuperviseService
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.setSearchTime();
    }

    getQGSuperviseSummary() {
        this.isTableLoading = true;
        this.superviseService.getQGSuperviseSummaryAsync(this.search).subscribe((data) => {
            this.dataList = data;
            this.isTableLoading = false;
        });
    }

    refreshData() {
        this.getQGSuperviseSummary();
    }

    reset() {
        this.search.beginTime = '';
        this.search.endTime = '';
        this.dateRange = [];
        this.refreshData();
    }

    onChange(result: Date): void {
        this.setSearchTime();
    }

    setSearchTime() {
        this.search.beginTime = this.dateRange[0].getFullYear() + '-' + (this.dateRange[0].getMonth() + 1) + '-' + this.dateRange[0].getDate();
        this.search.endTime = this.dateRange[1].getFullYear() + '-' + (this.dateRange[1].getMonth() + 1) + '-' + this.dateRange[1].getDate();
    }

    export() {
        this.exportLoading = true;
        this.superviseService.exportQGSuperviseSummary(this.search).subscribe((data => {
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
