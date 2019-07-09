import { Component, Injector } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { CriterionExamine } from 'entities';
import { Router, ActivatedRoute } from '@angular/router';
import { WorkCriterionService } from 'services';
import { ResultDetailComponent } from './result-detail/result-detail.component';
import { ResultFeedbackComponent } from './result-feedback/result-feedback.component';

@Component({
    moduleId: module.id,
    selector: 'examine-detail',
    templateUrl: 'examine-detail.component.html'
})
export class ExamineDetailComponent extends PagedListingComponentBase<any>{
    examineId: string;
    examine: CriterionExamine = new CriterionExamine();
    constructor(injector: Injector
        , private router: Router
        , private actRouter: ActivatedRoute
        , private workCriterionService: WorkCriterionService
    ) {
        super(injector);
        this.examineId = this.actRouter.snapshot.params['id'];
    }

    ngOnInit(): void {
        this.getExamineInfo();
        this.refreshData();
    }
    getExamineInfo() {
        if (this.examineId) {
            let params: any = {};
            params.id = this.examineId;
            this.workCriterionService.getExamineInfo(params).subscribe((result) => {
                this.examine = result;
            });
        }
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
        params.ExamineId = this.examineId;
        params.SkipCount = request.skipCount;
        params.MaxResultCount = request.maxResultCount;
        this.workCriterionService.getExamineDetailByCurrentIdAsync(params)
            .finally(() => {
                finishedCallback();
            })
            .subscribe((result: PagedResultDto) => {
                this.dataList = result.items
                this.totalItems = result.totalCount;
            });
    }

    resultDetail(id: string): void {
        this.modalHelper
            .open(ResultDetailComponent, { id: id }, 950, {
                nzMask: true,
                nzClosable: false,
                nzMaskClosable: false,
            })
            .subscribe(isSave => {
                if (isSave) {
                    this.refreshData();
                }
            });
    }

    resultFeedBack(id: string): void {
        this.modalHelper
            .open(ResultFeedbackComponent, { id: id, type: 1 }, 950, {
                nzMask: true,
                nzClosable: false,
                nzMaskClosable: false,
            })
            .subscribe(isSave => {
                if (isSave) {
                    this.refreshData();
                }
            });
    }

    return() {
        this.router.navigate(['app/criterion/my-examine']);
    }
}