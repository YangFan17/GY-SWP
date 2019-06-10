import { Component, Injector } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { WorkCriterionService } from 'services';
import { Router } from '@angular/router';
import { SelectGroup } from 'entities';

@Component({
    moduleId: module.id,
    selector: 'criterion',
    templateUrl: 'criterion.component.html',
    styleUrls: ['criterion.component.less']
})
export class CriterionComponent extends PagedListingComponentBase<any>{
    search: any = { keyWord: '', categoryId: '0' };
    categories = [];
    constructor(injector: Injector
        , private router: Router
        , private workCriterionService: WorkCriterionService) {
        super(injector);
    }

    ngOnInit(): void {
        this.getCategoryType();
    }

    getCategoryType() {
        this.workCriterionService.getCategoryTypeAsync().subscribe((result: SelectGroup[]) => {
            this.categories.push(SelectGroup.fromJS({ value: '0', text: '全部' }));
            this.categories.push(...result);
            this.refresh();
        });
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
        this.workCriterionService.getDocumentListAsync(params)
            .finally(() => {
                finishedCallback();
            })
            .subscribe((result: PagedResultDto) => {
                this.dataList = result.items
                this.totalItems = result.totalCount;
            });
    }

    goPre(id: string) {
        this.router.navigate(['app/criterion/pre-doc', id]);
    }

    goDetail(id: string) {
        this.router.navigate(['app/criterion/self-learning', id]);
    }
}
