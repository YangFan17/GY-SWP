import { Component, OnInit, ViewChild, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/component-base';
import { NzTreeComponent, NzFormatEmitEvent } from 'ng-zorro-antd';
import { BasicDataService, InspectService } from 'services';


@Component({
    moduleId: module.id,
    selector: 'inspect',
    templateUrl: 'inspect.component.html',
    styleUrls: ['inspect.component.less'],
    providers: [InspectService]
})
export class InspectComponent extends AppComponentBase implements OnInit {

    @ViewChild('detpTree') detpTree: NzTreeComponent;
    nodes: any[];
    selectedDept: any = { id: '', name: '' };
    month: number;
    dataList: any[];
    search = { month: new Date(), deptId: 0, userName: '' };

    constructor(injector: Injector,
        private basicDataService: BasicDataService,
        private inspectService: InspectService
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.getTrees();
    }

    getTrees() {
        this.basicDataService.getDeptDocNzTreeNodes('自查部门').subscribe((data) => {
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
        this.search.deptId = this.selectedDept.id;
        this.month = this.search.month.getMonth() + 1;
        this.inspectService.getSearchInspectReports(this.search).subscribe((data) => {
            this.dataList = data;
        });
    }

    refreshData() {
        this.getInspectData();
    }

    reset() {

    }

}
