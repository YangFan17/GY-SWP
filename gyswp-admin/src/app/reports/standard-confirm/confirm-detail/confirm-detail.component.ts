import { Component, Injector } from '@angular/core';
import { StandardRevisionService } from 'services';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    moduleId: module.id,
    selector: 'confirm-detail',
    templateUrl: 'confirm-detail.component.html',
    providers: [StandardRevisionService]
})
export class ConfirmDetailComponent extends PagedListingComponentBase<any>{
    id: string;
    type: string;
    title = '';
    constructor(injector: Injector
        , private actRouter: ActivatedRoute
        , private router: Router
        , private standardRevisionService: StandardRevisionService
    ) {
        super(injector);
        this.id = this.actRouter.snapshot.params['id'];
        this.type = this.actRouter.snapshot.params['status'];
    }

    ngOnInit(): void {
        if (this.type === '1') {
            this.title = '已认领员工名单';
        } else {
            this.title = '未认领员工名单';
        }
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
        this.refresh();
    }

    protected fetchDataList(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
        let params: any = {};
        params.skipCount = request.skipCount;
        params.maxResultCount = request.maxResultCount;
        if (this.type === '1') {
            params.type = 1;
        } else {
            params.type = 2;
        }
        params.docId = this.id;
        this.standardRevisionService.GetEmpConfirmListById(params)
            .finally(() => {
                finishedCallback();
            })
            .subscribe((result: PagedResultDto) => {
                this.dataList = result.items
                this.totalItems = result.totalCount;
            });
    }

    return() {
        this.router.navigate(['app/reports/standard-confirm']);
    }
}
