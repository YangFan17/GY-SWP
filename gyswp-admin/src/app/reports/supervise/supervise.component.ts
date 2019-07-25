import { Component, OnInit, ViewChild, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/component-base';
import { NzTreeComponent, NzFormatEmitEvent } from 'ng-zorro-antd';
import { SuperviseService } from 'services';
import { addDays } from 'date-fns';


@Component({
    selector: 'supervise',
    templateUrl: 'supervise.component.html',
    styleUrls: ['supervise.component.less'],
    providers: [SuperviseService]
})
export class SuperviseComponent extends AppComponentBase implements OnInit {

    @ViewChild('detpTree') detpTree: NzTreeComponent;
    nodes: any[];
    selectedDept: any = { id: '', name: '' };
    dateRange = [addDays(new Date(), -1 * (new Date()).getDay() + 1), new Date()];
    dataList: any[];
    search = { beginTime: '', endTime: '', deptId: 0, userName: '' };
    isTableLoading = false;

    constructor(injector: Injector,
        private superviseService: SuperviseService
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.setSearchTime();
        this.getTrees();
    }

    getTrees() {
        this.superviseService.getDeptDocNzTreeNodes('监督查询部门').subscribe((data) => {
            this.nodes = data;
            if (data.length > 0) {
                var selectedNode = data[0].children[0];
                if (selectedNode && selectedNode.isSelected) {
                    this.selectedDept = { id: selectedNode.key, name: selectedNode.title };
                    this.getSuperviseData();
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
        this.getSuperviseData();
    }

    getSuperviseData() {
        this.search.deptId = this.selectedDept.id;
        this.isTableLoading = true;
        this.superviseService.getSuperviseReportData(this.search).subscribe((data) => {
            this.dataList = data;
            this.isTableLoading = false;
        });
    }

    refreshData() {
        this.getSuperviseData();
    }

    reset() {
        this.search.beginTime = '';
        this.search.endTime = '';
        this.search.userName = '';
        this.dateRange = [addDays(new Date(), -1 * (new Date()).getDay() + 1), new Date()]
        this.refreshData();
    }

    onChange(result: Date): void {
        this.setSearchTime();
        //console.log('onChange: ', result);
    }

    setSearchTime() {
        this.search.beginTime = this.dateRange[0].getFullYear() + '-' + (this.dateRange[0].getMonth() + 1) + '-' + this.dateRange[0].getDate();
        this.search.endTime = this.dateRange[1].getFullYear() + '-' + (this.dateRange[1].getMonth() + 1) + '-' + this.dateRange[1].getDate();
    }
}
