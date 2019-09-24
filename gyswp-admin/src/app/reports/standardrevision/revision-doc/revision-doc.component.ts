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
    type: string;//
    date: string;
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
    protected fetchDataList(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
        let params: any = {};
        params.skipCount = request.skipCount;
        params.maxResultCount = request.maxResultCount;
        params.deptId = this.deptId;
        params.type = this.type;
        params.endTime = this.date;
        params.startTime = new Date().getFullYear() + '-' + (new Date().getMonth() + 1) + '-1';
        this.workCriterionService.getActionDocumentAsync(params).finally(() => {
            finishedCallback();
        })
            .subscribe((result: PagedResultDto) => {
                this.dataList = result.items
                this.totalItems = result.totalCount;
            });
    }

    goDetail(itemid: string) {
        this.router.navigate(['app/reports/revision-draft', itemid, this.deptId, this.type, this.date]);
    }
}
