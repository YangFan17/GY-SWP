import { Component, OnInit, ViewChild, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/component-base';
import { NzTreeComponent, NzFormatEmitEvent } from 'ng-zorro-antd';
import { InspectService } from 'services';


@Component({
    selector: 'inspect',
    templateUrl: 'inspect.component.html',
    styleUrls: ['inspect.component.less'],
    providers: [InspectService]
})
export class InspectComponent extends AppComponentBase implements OnInit {

    @ViewChild('detpTree') detpTree: NzTreeComponent;
    nodes: any[];
    selectedDept: any = { id: '', name: '' };
    // month: number;
    date = new Date();
    dataList: any[];
    // search = { month: '', deptId: 0, userName: '' };
    search = { year: 2019, deptId: 0, userName: '' };
    isTableLoading = false;

    constructor(injector: Injector
        , private inspectService: InspectService
    ) {
        super(injector);
    }

    ngOnInit(): void {
        // this.search.month = this.date.getFullYear() + '-' + (this.date.getMonth() + 1) + '-1';
        this.search.year = this.date.getFullYear();
        this.getTrees();
    }

    getTrees() {
        this.inspectService.getDeptDocNzTreeNodes('自查部门').subscribe((data) => {
            this.nodes = data;
            if (data.length > 0) {
                var selectedNode = data[0].children[0];
                if (selectedNode && selectedNode.isSelected) {
                    this.selectedDept = { id: selectedNode.key, name: selectedNode.title };
                    this.getInspectData();
                }
            }
        });
    }

    // 选中节点
    activeNode(data: NzFormatEmitEvent): void {
        if (data.node.key == '0' || data.node.key == '-1') {
            this.selectedDept = { id: '', name: '' };
        } else {
            this.selectedDept = { id: data.node.key, name: data.node.title };
        }
        this.getInspectData();
    }

    getInspectData() {
        // this.month = this.date.getMonth() + 1;
        this.search.deptId = this.selectedDept.id;
        this.search.year = this.date.getFullYear();
        this.isTableLoading = true;
        //alert(this.month)
        this.inspectService.getSearchInspectReports(this.search).subscribe((data) => {
            this.dataList = data;
            this.isTableLoading = false;
        });
    }

    refreshData() {
        this.getInspectData();
    }

    reset() {
        // this.search.month = '';
        this.search.userName = '';
        this.date = new Date();
        this.search.year = this.date.getFullYear();
        // this.search.month = this.date.getFullYear() + '-' + (this.date.getMonth() + 1) + '-1';
        this.refreshData();
    }

    onChange(result: Date): void {
        // this.search.month = this.date.getFullYear() + '-' + (this.date.getMonth() + 1) + '-1';
        //console.log('onChange: ', result);
        this.search.year = this.date.getFullYear();
    }
}