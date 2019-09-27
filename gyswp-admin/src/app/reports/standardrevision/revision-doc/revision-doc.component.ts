import { Component, Injector } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { WorkCriterionService } from 'services';

@Component({
    moduleId: module.id,
    selector: 'revision-doc',
    templateUrl: 'revision-doc.component.html',
    providers: [WorkCriterionService]

})
export class RevisionDocComponent extends PagedListingComponentBase<any>{
    deptId: string;
    type: string;
    date: string;
    title: string;
    constructor(injector: Injector
        , private router: Router
        , private actRouter: ActivatedRoute
        , private workCriterionService: WorkCriterionService) {
        super(injector)
        {
            this.deptId = this.actRouter.snapshot.params['deptId'];
            this.type = this.actRouter.snapshot.params['type'];
            this.date = this.actRouter.snapshot.params['date'];
        };
    }

    ngOnInit(): void {
        if (this.type == '1') {
            this.title = '现行标准汇总';
        } else if (this.type == '2') {
            this.title = '标准制定汇总';
        } else if (this.type == '4') {
            this.title = '标准修订汇总';
        } else if (this.type == '6') {
            this.title = '标准废止汇总';
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

    protected fetchDataList(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
        let params: any = {};
        params.skipCount = request.skipCount;
        params.maxResultCount = request.maxResultCount;
        params.deptId = this.deptId;
        params.type = this.type;
        params.month = this.date;
        this.workCriterionService.getActionDocumentAsync(params).finally(() => {
            finishedCallback();
        })
            .subscribe((result: PagedResultDto) => {
                this.dataList = result.items
                this.totalItems = result.totalCount;
            });
    }

    goDetail(itemid: string) {
        if (this.type == '1') {
            this.router.navigate(['app/reports/revision-draft', itemid, this.deptId, this.type, this.date]);
        } else {
            this.router.navigate(['app/reports/revision-draft', itemid, this.deptId, this.type, this.date]);
        }
    }

    return() {
        this.router.navigate(['app/reports/standardrevision']);
    }
}
