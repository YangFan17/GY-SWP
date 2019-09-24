import { Component, OnInit, Input, Injector } from '@angular/core';
import { AppComponentBase, PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { ActivatedRoute, Router } from '@angular/router';
import { SupervisionService } from 'services';
import { NzModalService } from 'ng-zorro-antd';
import { IndicatorShowDto } from 'entities';
import { FeedbackResultComponent } from '@app/supervision/criterion-examine/feedback-result/feedback-result.component';
import { FullInResultComponent } from './full-in-result/full-in-result.component';

@Component({
    moduleId: module.id,
    selector: 'target-list',
    templateUrl: 'target-list.component.html'
})
export class TargetListComponent extends PagedListingComponentBase<any>{
    // @Input() id: string;
    id: string;
    indicatorList: IndicatorShowDto[] = [];
    constructor(injector: Injector
        , private actRouter: ActivatedRoute
        , private router: Router
        , private supervisionService: SupervisionService
        , private modal: NzModalService
    ) {
        super(injector);
        this.id = this.actRouter.snapshot.params['id'];
    }

    ngOnInit(): void {
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
        params.SkipCount = request.skipCount;
        params.MaxResultCount = request.maxResultCount;
        params.id = this.id;
        this.supervisionService.getIndicatorRecord(params)
            .finally(() => {
                finishedCallback();
            })
            .subscribe((result: PagedResultDto) => {
                this.dataList = result.items
                this.totalItems = result.totalCount;
            });
    }

    // getIndicatorListById() {
    //     this.supervisionService.getIndicatorListById(this.id).subscribe(res => {
    //         this.indicatorList = res;
    //     });
    // }

    resultFeedBack(id: string): void {
        this.modalHelper
            .open(FeedbackResultComponent, { id: id }, 950, {
                nzMask: true,
                nzClosable: false,
                nzMaskClosable: false,
            })
            .subscribe(isSave => {
                if (isSave) {
                }
            });
    }

    fullResult(id: string): void {
        this.modalHelper
            .open(FullInResultComponent, { id: id }, 'md', {
                nzMask: true,
                nzClosable: false,
                nzMaskClosable: false,
            })
            .subscribe(isSave => {
                if (isSave) {
                    this.refresh();
                }
            });
    }

    return() {
        this.router.navigate(['app/supervision/indicators']);
    }
}
