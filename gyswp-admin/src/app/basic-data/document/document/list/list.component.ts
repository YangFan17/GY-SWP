import { Component, Input, Injector } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { Router } from '@angular/router';
import { ApiResult } from 'entities';
import { BasicDataService } from 'services';
import { UploadFile } from 'ng-zorro-antd';

@Component({
    moduleId: module.id,
    selector: 'list',
    templateUrl: 'list.component.html',
    styleUrls: ['list.component.less']
})
export class ListComponent extends PagedListingComponentBase<any>{
    @Input() categoryId: any;
    dept: any = { id: '', name: '' };
    keyWord: string;
    // loading = false;
    uploading = false;
    selectedCategory = { id: '', name: '' };

    constructor(injector: Injector, private router: Router
        , private basicDataService: BasicDataService) {
        super(injector);
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
        this.keyWord = '';
        this.refresh();
    }

    protected fetchDataList(
        request: PagedRequestDto,
        pageNumber: number,
        finishedCallback: Function,
    ): void {
        let params: any = {};
        params.SkipCount = request.skipCount;
        params.MaxResultCount = request.maxResultCount;
        params.KeyWord = this.keyWord;
        params.DeptId = this.dept.id;
        params.CategoryId = this.selectedCategory ? this.selectedCategory.id : null;
        this.basicDataService.getDocumentListAsync(params)
            .finally(() => {
                finishedCallback();
            })
            .subscribe((result: PagedResultDto) => {
                this.dataList = result.items
                this.totalItems = result.totalCount;
            });
    }

    create() {
        if (!this.selectedCategory || !this.selectedCategory.id) {
            this.notify.info('请先选择分类');
            return;
        }
        this.router.navigate(['app/basic/doc-detail', { cid: this.selectedCategory.id, cname: this.selectedCategory.name, deptId: this.dept.id, deptName: this.dept.name }]);
    }

    edit(item) {
        this.router.navigate(['app/basic/doc-detail', { id: item.id, deptId: this.dept.id, deptName: this.dept.name }]);
    }
}