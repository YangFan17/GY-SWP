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
    curDeptId: string;
    constructor(injector: Injector
        , private supervisionService: SupervisionService
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.getDeptId();
    }

    getDeptId() {
        this.supervisionService.getDeptId().subscribe((data) => {
            this.curDeptId = data;
            this.getTrees();
        });
    }

    getTrees() {
        // this.supervisionService.getDeptExamineNzTreeNodes().subscribe((data) => {
        //     this.nodes = data;
        //     if (data.length > 0) {
        //         var selectedNode = data[0].children[0];
        //         this.curDeptId = selectedNode.key;
        //         if (selectedNode && selectedNode.isSelected) {
        //             if (this.curDeptId == data[0].children[0].key) {
        //                 this.isCurDept = true;
        //             } else {
        //                 this.isCurDept = false;
        //             }
        //             this.selectedDept = { id: selectedNode.key, name: selectedNode.title, isCurDept: this.isCurDept };
        //             this.empList.dept = this.selectedDept;
        //             this.empList.getEmployeeList();
        //         }
        //     }
        // });
        this.supervisionService.getDeptDocNzTreeNodesNoPermission('考核对象').subscribe((data) => {
            this.nodes = data;
            if (data.length > 0) {
                var selectedNode = data[0].children[0];
                // this.curDeptId = selectedNode.key;
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
            // if (data.node.key == '59549057' || data.node.key == '59646091' || data.node.key == '59552081'
            //     || data.node.key == '59632058' || data.node.key == '59571109' || data.node.key == '59584063'
            //     || data.node.key == '59644078' || data.node.key == '59620071' || data.node.key == '59628060'
            //     || data.node.key == '59538081' || data.node.key == '59490590' || data.node.key == '59591062'
            //     || data.node.key == '59481641' || data.node.key == '59534185' || data.node.key == '59534184'
            //     || data.node.key == '59534183') {
            //     this.empList.isCountryDept = false;
            // } else {
            //     this.empList.isCountryDept = true;
            // }
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
