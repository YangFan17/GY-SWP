import { Component, Output, EventEmitter, Input, Injector, OnInit } from '@angular/core';
import { ModalComponentBase } from '@shared/component-base';
import { Router } from '@angular/router';
import { BasicDataService } from 'services';
import { Employee } from 'entities';
import { NzModalService } from 'ng-zorro-antd';

@Component({
    moduleId: module.id,
    selector: 'dept-user',
    templateUrl: 'dept-user.component.html',
    styleUrls: ['dept-user.component.less']
})
export class DeptUserComponent extends ModalComponentBase implements OnInit {
    @Output() modalCancel: EventEmitter<boolean> = new EventEmitter<boolean>();
    @Input() selectedUsers = [];
    @Input() deptId: string;
    @Input() deptName: string;
    keyWord: string;
    indeterminate = false;
    isTableLoading: boolean = false;
    dataList: Employee[] = [];
    allChecked = false;
    orgCheckedKeys = [];

    constructor(injector: Injector, private router: Router
        , private basicDataService: BasicDataService
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.getEmployeeList();
    }

    getEmployeeList() {
        let params: any = {};
        params.DepartId = this.deptId;
        params.keyWord = this.keyWord;
        this.isTableLoading = true;
        this.basicDataService.GetEmployeeListByDeptIdAsync(params)
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
