import { Component, OnInit, ViewChild, Output, EventEmitter, Injector } from '@angular/core';
import { AppComponentBase, PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { NzTreeComponent, NzDropdownContextComponent, NzTreeNode, NzModalRef, NzDropdownService, NzModalService, NzFormatEmitEvent } from 'ng-zorro-antd';
import { StandardRevisionService } from 'services';
import { Router } from '@angular/router';
import { AppConsts } from '@shared/AppConsts';

@Component({
    moduleId: module.id,
    selector: 'active-category',
    templateUrl: 'active-category.component.html',
    providers: [StandardRevisionService]
})
export class ActiveCategoryComponent extends PagedListingComponentBase<any>{
    @ViewChild('treeCom') treeCom: NzTreeComponent;
    nodes = [];
    selectedCategory: any = { id: '', name: '' };
    constructor(injector: Injector
        , private router: Router
        , private standardRevisionService: StandardRevisionService
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.getTrees();
    }

    activeNode(data: NzFormatEmitEvent): void {
        this.selectedCategory = { id: data.node.key, name: data.node.title };
        this.refreshData();
    }

    getTrees() {
        this.standardRevisionService.getActiveCategoryTree().subscribe((data) => {
            this.nodes = data;
            this.refreshData()
        });
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
        params.SkipCount = request.skipCount;
        params.MaxResultCount = request.maxResultCount;
        if (this.selectedCategory.id) {
            params.CategoryId = this.selectedCategory.id;
        }
        this.standardRevisionService.getActionDocumentList(params)
            .finally(() => {
                finishedCallback();
            })
            .subscribe((result: PagedResultDto) => {
                this.dataList = result.items
                this.totalItems = result.totalCount;
            });
    }

    export() {
        this.isTableLoading = true;
        let params: any = {};
        if (this.selectedCategory.id) {
            params.CategoryId = this.selectedCategory.id;
        }
        this.standardRevisionService.exportActionDocument(params).finally(() => {
            this.isTableLoading = false;
        }).subscribe((data => {
            if (data.code == 0) {
                var url = AppConsts.remoteServiceBaseUrl + data.data;
                document.getElementById('exportUrl').setAttribute('href', url);
                document.getElementById('btnExportHref').click();
            } else {
                this.notify.error(data.msg);
            }
        }));
    }

    return() {
        this.router.navigate(['app/reports/standardrevision']);
    }
}
