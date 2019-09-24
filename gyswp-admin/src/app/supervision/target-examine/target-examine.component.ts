import { Component, Injector } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { SupervisionService } from 'services';
import { Router } from '@angular/router';
import { NzModalRef, NzModalService } from 'ng-zorro-antd';
import { PublishConfirmComponent } from './target-examine-detail/target-list/publish-confirm/publish-confirm.component';

@Component({
    moduleId: module.id,
    selector: 'target-examine',
    templateUrl: 'target-examine.component.html'
})
export class TargetExamineComponent extends PagedListingComponentBase<any>{
    search: any = { cycleTime: 0 };
    confirmModal: NzModalRef;

    cycleTimes = [
        { value: 0, text: '全部', selected: true },
        { value: 2, text: '季度', selected: false },
        { value: 1, text: '年度', selected: false },
    ]
    constructor(injector: Injector
        , private router: Router
        , private supervisionService: SupervisionService
        , private modal: NzModalService
    ) {
        super(injector);
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
        this.search = { keyWord: '', cycleTime: 0 };
        this.refresh();
    }

    protected fetchDataList(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
        let params: any = {};
        params.SkipCount = request.skipCount;
        params.MaxResultCount = request.maxResultCount;
        // params.KeyWord = this.search.keyWord;
        if (this.search.cycleTime != 0) {
            params.cycleTime = this.search.cycleTime;
        }
        this.supervisionService.getIndicatorListAsync(params)
            .finally(() => {
                finishedCallback();
            })
            .subscribe((result: PagedResultDto) => {
                this.dataList = result.items
                this.totalItems = result.totalCount;
            });
    }

    publishById(id: string) {
        let indicatorIds: string[] = [];
        indicatorIds.push(id);
        this.saving = true;
        this.modalHelper
            .open(PublishConfirmComponent, { indicatorIds: indicatorIds, title: `发布当前目标指标监督检查` }, 'md', {
                nzMask: true,
                nzClosable: false,
                nzMaskClosable: false,
            })
            .subscribe(isSave => {
                this.saving = false;
                if (isSave) {
                    this.modal.closeAll();
                }
            });
    }

    publish() {
        let num: number = this.dataList.filter(v => v.checked).length;
        if (num == 0) {
            this.notify.info('请至少选择一项需要考核的指标', '');
        } else {
            let indicatorIds: string[] = [];
            this.dataList.filter(v => v.checked).forEach(v => {
                indicatorIds.push(v.id);
            });
            this.saving = true;
            this.modalHelper
                .open(PublishConfirmComponent, { indicatorIds: indicatorIds, num: num, title: `发布当前 ${num} 项目标指标监督检查` }, 'md', {
                    nzMask: true,
                    nzClosable: false,
                    nzMaskClosable: false,
                })
                .subscribe(isSave => {
                    this.saving = false;
                    if (isSave) {
                        this.modal.closeAll();
                    }
                });
        }
    }

    create() {
        this.router.navigate(['app/supervision/indicator-detail']);
    }

    goDetail(id: string) {
        this.router.navigate(['app/supervision/indicator-detail', id]);
    }

    goRecord(id: string) {
        this.router.navigate(['app/supervision/target-record', id]);
    }
}
