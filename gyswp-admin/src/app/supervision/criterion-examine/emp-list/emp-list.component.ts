import { Component, Injector, OnInit } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto, AppComponentBase } from '@shared/component-base';
import { SupervisionService } from 'services';
import { Router } from '@angular/router';
import { Employee } from 'entities';
import { NzModalRef, NzModalService } from 'ng-zorro-antd';

@Component({
    moduleId: module.id,
    selector: 'emp-list',
    templateUrl: 'emp-list.component.html',
    styleUrls: ['emp-list.component.less']
})
export class EmpListComponent extends AppComponentBase implements OnInit {
    dept: any = { id: '', name: '' };
    loading = false;
    selectedCategory = { id: '', name: '' };
    empList: Employee[] = [];
    confirmModal: NzModalRef;
    isSelectedAll: boolean = false; // 是否全选
    checkboxCount: number = 0; // 所有Checkbox数量
    checkedLength: number = 0; // 已选中的数量

    constructor(injector: Injector
        , private router: Router
        , private modal: NzModalService
        , private supervisionService: SupervisionService
    ) {
        super(injector);
    }
    ngOnInit(): void {
        // this.getEmployeeList();
    }

    /**
     * 重置
     */
    reset() {
        this.getEmployeeList();
    }

    getEmployeeList() {
        this.resetCheckBox();
        let params: any = {};
        params.DepartId = this.dept.id;
        params.DepartName = this.dept.name;
        this.loading = true;
        this.supervisionService.getEmployeeListByDeptIdAsync(params)
            .finally(() => {
                this.loading = false;
            })
            .subscribe((result: Employee[]) => {
                this.empList = result;
            });
    }

    //#region CheckBox操作
    checkAll(e) {
        var v = this.isSelectedAll;
        this.empList.forEach(u => {
            u.checked = v;
        });
        if (this.isSelectedAll == false) {
            this.checkedLength = 0;
        } else {
            this.checkedLength = this.empList.filter(v => v.checked).length;
        }
    }

    isCancelCheck(x: any) {
        this.checkedLength = this.empList.filter(v => v.checked).length;
        this.checkboxCount = this.empList.length;
        if (this.checkboxCount - this.checkedLength > 0) {
            this.isSelectedAll = false;
        } else {
            this.isSelectedAll = true;
        }
    }
    showRole() {
        // if (this.checkedLength == 0) {
        //     this.notify.info('请选择所要分配的人员！', '');
        // } else {
        //     var checkedEmoloyeeIds = this.empList.filter(v => v.roleSelected == true).map(v => { return v.id; }
        //     ).join(',');
        //     this.docModal.show(checkedEmoloyeeIds);
        // }
    }
    resetCheckBox() {
        this.isSelectedAll = false; // 是否全选
        this.checkboxCount = 0; // 所有Checkbox数量
        this.checkedLength = 0; // 已选中的数量
    }
    //#endregion

    /***
     * 内部考核
     */
    internalExa(): void {
        let num: number = 0;
        num = this.checkedLength;
        if (num == 0) {
            num = this.empList.length;
        }
        this.confirmModal = this.modal.confirm({
            nzContent: `是否为当前 ${num} 人生成考核记录信息?`,
            nzOnOk: () => {
                // this.supervisionService.saveDraftDoc(this.applyId, this.id).finally(() => { this.saving = false; }).subscribe(res => {
                //     if (res.code == 0) {
                //         this.notify.info('制订申请提交成功！', '');
                //         this.modal.closeAll();
                //         this.return();
                //     }
                //     else {
                //         this.notify.error('制订申请提交失败，请重试！', '');
                //     }
                // });
            },
            nzOnCancel: () => {
                this.saving = false;
            }
        });
    }

    create() {
        if (!this.selectedCategory || !this.selectedCategory.id) {
            // this.notify.info('请先选择分类');
            return;
        }
        this.router.navigate(['app/basic/doc-detail', { cid: this.selectedCategory.id, cname: this.selectedCategory.name, deptId: this.dept.id, deptName: this.dept.name }]);
    }

    edit(item) {
        this.router.navigate(['app/basic/doc-detail', { id: item.id, deptId: this.dept.id, deptName: this.dept.name }]);
    }
}
