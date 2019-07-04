import { Component, OnInit, ViewChild, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/component-base';
import { NzTreeComponent, NzFormatEmitEvent } from 'ng-zorro-antd';
import { SupervisionService } from 'services';
import { EmpListComponent } from './emp-list/emp-list.component';
import { SuperAdminComponent } from './super-admin/super-admin.component';

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
    isCurDept: boolean = false; //是否为本部门
    curDeptId: any;
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
                this.curDeptId = selectedNode.key;
                if (selectedNode && selectedNode.isSelected) {
                    if (this.curDeptId == data[0].children[0].key) {
                        this.isCurDept = true;
                    } else {
                        this.isCurDept = false;
                    }
                    this.selectedDept = { id: selectedNode.key, name: selectedNode.title, isCurDept: this.isCurDept };
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
            if (this.curDeptId == data.node.key) {
                this.isCurDept = true;
            } else {
                this.isCurDept = false;
            }
            this.selectedDept = { id: data.node.key, name: data.node.title, isCurDept: this.isCurDept };
            this.empList.dept = this.selectedDept;
            this.empList.getEmployeeList();
        }
    }

    adminExa(type: number): void {
        this.modalHelper
            .open(SuperAdminComponent, { type: type }, 'md', {
                nzMask: true,
                nzClosable: false,
                nzMaskClosable: false,
            })
            .subscribe(isSave => {
                if (isSave) {
                }
            });
    }
}
