import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { AppComponentBase, PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { SupervisionService } from 'services';
import { NzFormatEmitEvent, NzTreeComponent } from 'ng-zorro-antd';
import { Router } from '@angular/router';
import { SelectGroup } from 'entities';
import { DraftApplyInfoComponent } from '@app/work-criterion/criterion/draft-doc/draft-apply-info/draft-apply-info.component';

@Component({
    moduleId: module.id,
    selector: 'target-examine',
    templateUrl: 'target-examine.component.html'
})
export class TargetExamineComponent extends PagedListingComponentBase<any>{
    search: any = {};
    constructor(injector: Injector
        , private router: Router
        , private supervisionService: SupervisionService
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
        this.search = { keyWord: '', categoryId: '0' };
        this.refresh();
    }

    protected fetchDataList(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
        let params: any = {};
        params.SkipCount = request.skipCount;
        params.MaxResultCount = request.maxResultCount;
        params.KeyWord = this.search.keyWord;
        if (this.search.categoryId != '0') {
            params.CategoryId = this.search.categoryId;
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

    create() {
        this.router.navigate(['app/supervision/indicator-detail']);
    }

    goDetail(id: string) {
        this.router.navigate(['app/supervision/indicator-detail', id]);
    }
}
