import { Component, OnInit, ViewChild, Injector } from '@angular/core';
import { AppComponentBase, PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { NzTreeComponent, NzFormatEmitEvent } from 'ng-zorro-antd';
import { StandardRevisionService } from 'services';
import { Router } from '@angular/router';

@Component({
    moduleId: module.id,
    selector: 'standard-confirm',
    templateUrl: 'standard-confirm.component.html',
    providers: [StandardRevisionService]
})
export class StandardConfirmComponent extends PagedListingComponentBase<any>{

    @ViewChild('detpTree') detpTree: NzTreeComponent;
    nodes: any[];
    selectedDept: any = { id: '', name: '' };
    // dataList: any[];
    search = { keyWord: '', deptId: 1 };
    // isTableLoading = false;

    constructor(injector: Injector
        , private router: Router
        , private standardRevisionService: StandardRevisionService) {
        super(injector);
    }

    ngOnInit() {
        console.log("2");
        this.getTrees();
    }

    getTrees() {
        this.standardRevisionService.getDeptDocNzTreeNodes('标准归口部门').subscribe((data) => {
            this.nodes = data;
            if (data.length > 0) {
                var selectedNode = data[0].children[0];
                if (selectedNode && selectedNode.isSelected) {
                    this.selectedDept = { id: selectedNode.key, name: selectedNode.title };
                    this.refreshData();
                }
            }
        });
    }

    // 选中节点
    activeNode(data: NzFormatEmitEvent): void {
        if (data.node.key == '0' || data.node.key == '-1') {
            this.selectedDept = { id: '1', name: '' };
        } else {
            this.selectedDept = { id: data.node.key, name: data.node.title };
        }
        this.refreshData();
    }

    getStandardRevisionData() {
        this.search.deptId = this.selectedDept.id;
        this.isTableLoading = true;
        this.standardRevisionService.getDocumentConfirmsList(this.search).subscribe((data) => {
            this.dataList = data.items;
            this.isTableLoading = false;
        });
    }

    reset() {
        this.search.keyWord = '';
        this.refresh();
    }

    refresh(): void {
        this.getDataPage(this.pageNumber);
    }
    refreshData() {
        this.pageNumber = 1;
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
        params.KeyWord = this.search.keyWord;
        params.deptId = this.selectedDept.id;
        this.isTableLoading = true;
        this.standardRevisionService.getDocumentConfirmsList(params)
            .finally(() => {
                finishedCallback();
            })
            .subscribe((result: PagedResultDto) => {
                this.dataList = result.items
                this.totalItems = result.totalCount;
            });
    }

    goDetail(id: string, type: number) {
        this.router.navigate(['app/reports/confirm-detail', id, type]);
    }
}