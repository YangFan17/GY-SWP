import { Component, OnInit, Output, EventEmitter, Input, Injector } from '@angular/core';
import { ModalComponentBase } from '@shared/component-base';
import { Router } from '@angular/router';
import { SupervisionService } from 'services';
import { NzFormatEmitEvent } from 'ng-zorro-antd';

@Component({
    moduleId: module.id,
    selector: 'target-dept',
    templateUrl: 'target-dept.component.html'
})
export class TargetDeptComponent extends ModalComponentBase implements OnInit {
    @Output() modalCancel: EventEmitter<boolean> = new EventEmitter<boolean>();
    @Input() selectedDepts = [];
    isTableLoading: boolean = false;
    orgCheckedKeys = [];
    nodes = [];
    checkedDeptKeys = [];

    constructor(injector: Injector, private router: Router
        , private supervisionService: SupervisionService
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.initOrgCheckedKeys();
        this.getTrees();
    }
    getTrees() {
        this.supervisionService.getTreesAsync().subscribe((data) => {
            this.nodes = data;
            this.checkedDeptKeys = this.orgCheckedKeys;
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


    cancel() {
        this.close();
    }

    ok() {
        this.success(true);
    }
}
