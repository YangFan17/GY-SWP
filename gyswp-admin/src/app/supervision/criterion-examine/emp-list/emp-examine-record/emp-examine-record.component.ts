import { Component, Injector } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { Router, ActivatedRoute } from '@angular/router';
import { SupervisionService } from 'services';
import { CheckingResultComponent } from '../../checking-result/checking-result.component';
import { FeedbackResultComponent } from '../../feedback-result/feedback-result.component';

@Component({
    moduleId: module.id,
    selector: 'emp-examine-record',
    templateUrl: 'emp-examine-record.component.html'
})
export class EmpExamineRecordComponent extends PagedListingComponentBase<any>{
    empId: number;
    constructor(injector: Injector
        , private router: Router
        , private actRouter: ActivatedRoute
        , private supervisionService: SupervisionService
    ) {
        super(injector);
        this.empId = this.actRouter.snapshot.params['id'];
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
        params.EmployeeId = this.empId;
        params.SkipCount = request.skipCount;
        params.MaxResultCount = request.maxResultCount;
        this.supervisionService.getExamineDetailByEmpIdAsync(params)
            .finally(() => {
                finishedCallback();
            })
            .subscribe((result: PagedResultDto) => {
                this.dataList = result.items
                this.totalItems = result.totalCount;
            });
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

    return() {
        this.router.navigate(['app/supervision/supervision']);
    }
}
