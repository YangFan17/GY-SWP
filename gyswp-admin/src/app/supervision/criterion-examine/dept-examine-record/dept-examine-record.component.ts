import { Component, Injector } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { Router, ActivatedRoute } from '@angular/router';
import { SupervisionService } from 'services';

@Component({
    moduleId: module.id,
    selector: 'dept-examine-record',
    templateUrl: 'dept-examine-record.component.html',
    styleUrls: ['dept-examine-record.component.less']
})
export class DeptExamineRecordComponent extends PagedListingComponentBase<any>{
    deptId: number;
    deptName: string;
    constructor(injector: Injector
        , private router: Router
        , private actRouter: ActivatedRoute
        , private supervisionService: SupervisionService
    ) {
        super(injector);
        this.deptId = this.actRouter.snapshot.params['id'];
        this.deptName = this.actRouter.snapshot.params['dept'];
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
        params.DeptId = this.deptId;
        params.SkipCount = request.skipCount;
        params.MaxResultCount = request.maxResultCount;
        this.supervisionService.getDeptExamineRecorAsync(params)
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
