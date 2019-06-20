import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { BasicDataService } from 'services';
import { Router } from '@angular/router';
import { Component, Injector } from '@angular/core';

@Component({
    moduleId: module.id,
    selector: 'employee',
    templateUrl: 'employee.component.html',
    styleUrls: ['employee.component.less']
})
export class EmployeeComponent extends PagedListingComponentBase<any>{
    search: any = {};
    departId: string;
    constructor(injector: Injector
        , private basicDataService: BasicDataService
        , private router: Router) {
        super(injector);
    }

    refresh(): void {
        this.getDataPage(this.pageNumber);
    }

    refreshData(tempNode: string) {
        this.pageNumber = 1;
        this.departId = tempNode;
        this.refresh();
    }
    /**
     * 重置
     */
    reset() {
        this.pageNumber = 1;
        this.search = { name: '', mobile: '' };
        this.refresh();
    }

    protected fetchDataList(
        request: PagedRequestDto,
        pageNumber: number,
        finishedCallback: Function,
    ): void {
        let params: any = {};
        params.SkipCount = request.skipCount;
        params.MaxResultCount = request.maxResultCount;
        params.departId = this.departId;
        params.Name = this.search.name;
        params.Mobile = this.search.mobile;
        this.basicDataService.getEmployeeListAsync(params)
            .finally(() => {
                finishedCallback();
            })
            .subscribe((result: PagedResultDto) => {
                this.dataList = result.items
                this.totalItems = result.totalCount;
            });
    }
}
