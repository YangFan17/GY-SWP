import { Component, OnInit, ViewChild, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/component-base';
import { NzTreeComponent, NzFormatEmitEvent } from 'ng-zorro-antd';
import { BasicDataService } from 'services';
import { addDays } from 'date-fns';
import { IndicatorSuperviseService } from 'services/reports/indicator-supervise.service';


@Component({
    selector: 'indicator-supervise',
    templateUrl: 'indicator-supervise.component.html',
    providers: [IndicatorSuperviseService]
})
export class IndicatorSuperviseComponent extends AppComponentBase implements OnInit {

    @ViewChild('detpTree') detpTree: NzTreeComponent;
    nodes: any[];
    selectedDept: any = { id: '', name: '' };
    dateRange = [addDays(new Date(), -1 * (new Date()).getDay() + 1), new Date()];
    dataList: any[];
    search = { beginTime: '', endTime: '', deptId: 0, userName: '' };
    isTableLoading = false;

    constructor(injector: Injector
        , private superviseService: IndicatorSuperviseService
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.setSearchTime();
        this.getTrees();
    }

    getTrees() {
        this.superviseService.getDeptDocNzTreeNodes('指标统计部门').subscribe((data) => {
            this.nodes = data;
            if (data.length > 0) {
                var selectedNode = data[0].children[0];
                if (selectedNode && selectedNode.isSelected) {
                    this.selectedDept = { id: selectedNode.key, name: selectedNode.title };
                    this.getIndicatorSuperviseData();
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
        this.getIndicatorSuperviseData();
    }

    getIndicatorSuperviseData() {
        this.search.deptId = this.selectedDept.id;
        this.isTableLoading = true;
        this.superviseService.getIndicatorSuperviseReportData(this.search).subscribe((data) => {
            this.dataList = data;
            this.isTableLoading = false;
        });
    }

    refreshData() {
        this.getIndicatorSuperviseData();
    }

    reset() {
        this.search.beginTime = '';
        this.search.endTime = '';
        this.search.userName = '';
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
