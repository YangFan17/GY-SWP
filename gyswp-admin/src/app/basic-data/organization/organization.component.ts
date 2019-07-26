import { AppComponentBase, PagedResultDto } from "@shared/component-base";
import { BasicDataService } from "services";
import { Component, OnInit, Injector, ViewChild } from "@angular/core";
import { NzDropdownContextComponent, NzTreeNode, NzFormatEmitEvent } from "ng-zorro-antd";
import { EmployeeComponent } from "../employee/employee.component";

@Component({
    moduleId: module.id,
    selector: 'organization',
    templateUrl: 'organization.component.html',
    styleUrls: ['organization.component.less']
})
export class OrganizationComponent extends AppComponentBase implements OnInit {
    @ViewChild('employeeList') employeeList: EmployeeComponent;

    syncDataLoading = false;
    exportLoading = false;
    search: any = {};
    searchValue;
    dropdown: NzDropdownContextComponent;
    activedNode: NzTreeNode;
    dragNodeElement;
    tempNode: string;
    nodes = [];

    constructor(injector: Injector
        , private basicDataService: BasicDataService
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.employeeList.refreshData(null);
        this.getTrees();
    }

    /**
     * important:
     * if u want to custom event/node properties, u need to maintain the selectedNodesList/checkedNodesList yourself
     * @param {} data
     */
    openFolder(data: NzTreeNode | NzFormatEmitEvent): void {
        // do something if u want
        if (data instanceof NzTreeNode) {
            // change node's expand status
            if (!data.isExpanded) {
                // close to open
                data.origin.isLoading = true;
                setTimeout(() => {
                    data.isExpanded = !data.isExpanded;
                    data.origin.isLoading = false;
                }, 500);
            } else {
                data.isExpanded = !data.isExpanded;
            }
        } else {
            // change node's expand status
            if (!data.node.isExpanded) {
                // close to open
                data.node.origin.isLoading = true;
                setTimeout(() => {
                    data.node.isExpanded = !data.node.isExpanded;
                    data.node.origin.isLoading = false;
                }, 500);
            } else {
                data.node.isExpanded = !data.node.isExpanded;
            }
        }
    }

    // 选中节点
    activeNode(data: NzFormatEmitEvent): void {
        if (this.activedNode) {
            this.activedNode = null;
        }
        data.node.isSelected = true;
        this.activedNode = data.node;
        this.tempNode = data.node.key;

        // this.refreshData(data.node.key);
        this.employeeList.search = { name: '', mobile: '' };
        this.employeeList.refreshData(this.tempNode);
    }

    selectDropdown(): void {
        this.dropdown.close();
        // do something
    }

    syncData() {
        this.syncDataLoading = true;
        this.basicDataService.synchronousOrganizationAsync().subscribe(() => {
            this.notify.success('同步成功！', '');
            this.syncDataLoading = false;
            this.getTrees();
            this.employeeList.refreshData(null);
        });
    }

    getTrees() {
        this.basicDataService.getTreesAsync().subscribe((data) => {
            this.nodes = data;
        });
    }
}
