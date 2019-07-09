import { Component, Output, EventEmitter, Input, Injector, OnInit } from '@angular/core';
import { ModalComponentBase } from '@shared/component-base';
import { Router } from '@angular/router';
import { BasicDataService } from 'services';
import { Employee } from 'entities';
import { NzModalService, NzFormatEmitEvent } from 'ng-zorro-antd';

@Component({
    moduleId: module.id,
    selector: 'dept-user',
    templateUrl: 'dept-user.component.html',
    styleUrls: ['dept-user.component.less']
})
export class DeptUserComponent extends ModalComponentBase implements OnInit {
    @Output() modalCancel: EventEmitter<boolean> = new EventEmitter<boolean>();
    @Input() selectedUsers = [];
    @Input() selectedDepts = [];
    @Input() deptId: string;
    @Input() deptName: string;
    keyWord: string;
    indeterminate = false;
    isTableLoading: boolean = false;
    dataList: Employee[] = [];
    allChecked = false;
    orgCheckedKeys = [];
    nodes = [];
    checkedDeptKeys = [];

    constructor(injector: Injector, private router: Router
        , private basicDataService: BasicDataService
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.initOrgCheckedKeys();
        this.getEmployeeList();
        this.getTrees();
    }
    getTrees() {
        this.basicDataService.getTreesAsync().subscribe((data) => {
            this.nodes = data;
            this.checkedDeptKeys = this.orgCheckedKeys;
        });
    }
    getEmployeeList() {
        let params: any = {};
        params.DepartId = this.deptId;
        params.keyWord = this.keyWord;
        this.isTableLoading = true;
        this.basicDataService.getEmployeeListByDeptIdAsync(params)
            .finally(() => {
                this.isTableLoading = false;
            })
            .subscribe((result: Employee[]) => {
                result.map(item => {
                    const users = this.selectedUsers.filter(e => e.id == item.id);
                    if (users.length > 0) {
                        item.checked = true;
                    } else {
                        item.checked = false;
                    }
                    return item;
                });
                this.dataList = result;
                this.refreshStatus(null, null);
            });
    }

    //#region 部门选择相关方法
    initOrgCheckedKeys() {
        this.orgCheckedKeys = this.selectedDepts.map(dept => { return dept.id; });
    }

    handleDeptClose(tag: any) {
        var i = 0;
        for (const item of this.selectedDepts) {
            if (item.id == tag.id) {
                let keys = this.orgCheckedKeys.filter(o => o == item.id);
                if (keys.length > 0) {
                    this.orgCheckedKeys.splice(this.orgCheckedKeys.indexOf(keys[0]), 1);
                }
                this.selectedDepts.splice(i, 1);
                // console.log(this.orgCheckedKeys);
                const tempKeys = this.orgCheckedKeys.concat();
                // console.log(tempKeys);
                if (tempKeys.length > 0) {
                    this.checkedDeptKeys = tempKeys;
                } else {
                    this.getTrees();
                }
                break;
            }
            i++;
        }
    }

    refreshDeptTags(item: any) {
        let depts = this.selectedDepts.filter(s => s.id == item.id);
        if (item.isChecked) {
            if (depts.length == 0) {
                this.selectedDepts.push({ id: item.id, name: item.name });
            }
        } else {
            if (depts.length > 0) {
                this.selectedDepts.splice(this.selectedDepts.indexOf(depts[0]), 1);
            }
        }
    }

    checkBoxChange(data: NzFormatEmitEvent) {
        this.orgCheckedKeys = data.keys;
        this.refreshDeptTags({ id: data.node.key, name: data.node.origin.deptName, isChecked: data.node.isChecked });
    }
    //#endregion

    handleUserClose(tag: any) {
        var i = 0;
        for (const item of this.selectedUsers) {
            if (item.id == tag.id) {
                let users = this.dataList.filter(e => e.id == item.id);
                if (users.length > 0) {
                    users[0].checked = false;
                }
                this.selectedUsers.splice(i, 1);
                this.refreshStatus(null, null);
                break;
            }
            i++;
        }
    }

    refreshStatus(eve: any, item: any): void {
        const allChecked = this.dataList.every(value => value.checked === true);
        const allUnChecked = this.dataList.every(value => !value.checked);
        this.allChecked = allChecked;
        this.indeterminate = (!allChecked) && (!allUnChecked);
        if (item) {
            this.refreshUserTags(item);
        }
    }

    refreshUserTags(item: any) {
        let users = this.selectedUsers.filter(s => s.id == item.id);
        if (item.checked) {
            if (users.length == 0) {
                this.selectedUsers.push({ id: item.id, name: item.name });
            }
        } else {
            if (users.length > 0) {
                this.selectedUsers.splice(this.selectedUsers.indexOf(users[0]), 1);
            }
        }
    }

    cancel() {
        this.close();
    }

    ok() {
        this.success(true);
    }
}
