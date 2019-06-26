import { Component, OnInit, ViewChild, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/component-base';
import { NzTreeComponent, NzFormatEmitEvent } from 'ng-zorro-antd';
import { SupervisionService } from 'services';
import { EmpListComponent } from './emp-list/emp-list.component';

@Component({
    moduleId: module.id,
    selector: 'criterion-examine',
    templateUrl: 'criterion-examine.component.html',
    styleUrls: ['criterion-examine.component.less']
})
export class CriterionExamineComponent extends AppComponentBase implements OnInit {
    @ViewChild('empList') empList: EmpListComponent;
    @ViewChild('detpTree') detpTree: NzTreeComponent;
    categoryName: string;
    nodes: any[];
    selectedDept: any = { id: '', name: '' };

    constructor(injector: Injector
        , private supervisionService: SupervisionService
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.getTrees();
    }

    getTrees() {
        this.supervisionService.getDeptExamineNzTreeNodes().subscribe((data) => {
            this.nodes = data;
            if (data.length > 0) {
                var selectedNode = data[0].children[0];
                if (selectedNode && selectedNode.isSelected) {
                    this.selectedDept = { id: selectedNode.key, name: selectedNode.title };
                    this.empList.dept = this.selectedDept;
                    this.empList.getEmployeeList();
                }
            }
        });
    }

    // 选中节点
    activeNode(data: NzFormatEmitEvent): void {
        if (data.node.key == '0' || data.node.key == '-1' || data.node.key == null || data.node.isDisabled == true) {
            this.selectedDept = { id: '', name: '' };
        } else {
            this.selectedDept = { id: data.node.key, name: data.node.title };
            this.empList.dept = this.selectedDept;
            this.empList.getEmployeeList();
        }
    }
}
