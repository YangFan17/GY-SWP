import { Component, Injector } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { Router, ActivatedRoute } from '@angular/router';
import { SupervisionService } from 'services';
import { CriterionExamine } from 'entities';
import { CheckingResultComponent } from '../../checking-result/checking-result.component';
import { FeedbackResultComponent } from '../../feedback-result/feedback-result.component';

@Component({
    moduleId: module.id,
    selector: 'record-detail',
    templateUrl: 'record-detail.component.html',
    styleUrls: ['record-detail.component.less']
})
export class RecordDetailComponent extends PagedListingComponentBase<any>{
    examineId: string;
    dept: string;
    examine: CriterionExamine = new CriterionExamine();
    constructor(injector: Injector
        , private router: Router
        , private actRouter: ActivatedRoute
        , private supervisionService: SupervisionService
    ) {
        super(injector);
        this.examineId = this.actRouter.snapshot.params['id'];
        this.dept = this.actRouter.snapshot.params['dept'];
    }

    ngOnInit(): void {
        this.getExamineInfo();
        this.refreshData();
    }
    getExamineInfo() {
        if (this.examineId) {
            let params: any = {};
            params.id = this.examineId;
            this.supervisionService.getExamineInfo(params).subscribe((result) => {
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
        this.supervisionService.getExamineRecordByIdAsync(params)
            .finally(() => {
                finishedCallback();
            })
            .subscribe((result: PagedResultDto) => {
                this.dataList = result.items
                this.totalItems = result.totalCount;
            });
    }

    goDetail(id: string) {
        this.router.navigate(['app/supervision/self-learning', id]);
    }

    return() {
        this.router.navigate(['app/supervision/supervision']);
    }

    checkingResult(id: string): void {
        this.modalHelper
            .open(CheckingResultComponent, { id: id }, 950, {
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
            .open(FeedbackResultComponent, { id: id }, 950, {
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
}
