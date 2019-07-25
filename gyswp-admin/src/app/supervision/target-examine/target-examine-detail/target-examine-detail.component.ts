import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/component-base';
import { ActivatedRoute, Router } from '@angular/router';
import { SupervisionService } from 'services';
import { Indicators } from 'entities';
import { TargetDeptComponent } from './target-dept/target-dept.component';
import { NzModalRef, NzModalService } from 'ng-zorro-antd';
import { TargetListComponent } from './target-list/target-list.component';

@Component({
    moduleId: module.id,
    selector: 'target-examine-detail',
    templateUrl: 'target-examine-detail.component.html'
})
export class TargetExamineDetailComponent extends AppComponentBase implements OnInit {
    @ViewChild('targetList') targetList: TargetListComponent;

    id: string;
    cycleTime = '1';
    deptTags = [];
    deptInfo: any[] = []; //选中的部门info
    indicator: Indicators = new Indicators();
    confirmModal: NzModalRef;

    constructor(injector: Injector
        , private actRouter: ActivatedRoute
        , private router: Router
        , private supervisionService: SupervisionService
        , private modal: NzModalService
    ) {
        super(injector);
        this.id = this.actRouter.snapshot.params['id'];
    }

    ngOnInit(): void {
        if (this.id) {
            this.getById();
        }
    }

    cycleTimeRadioChange(ngmodel: string) {
        this.cycleTime = ngmodel;
    }

    save(): void {
        this.confirmModal = this.modal.confirm({
            nzContent: '发布后将无法修改，是否发布?',
            nzOnOk: () => {
                this.indicator.cycleTime = this.cycleTime == '1' ? 1 : 2;
                this.getDepts();
                this.supervisionService.createOrUpdateIndicatorAsync(this.indicator, this.deptInfo)
                    .finally(() => { this.saving = false; })
                    .subscribe(res => {
                        this.notify.info('发布成功！', '');
                        if (res.code == 0) {
                            this.indicator.id = res.data;
                            this.id = this.indicator.id;
                            this.targetList.id = this.id;
                            this.targetList.getIndicatorListById();
                        }
                    });
            }
        });
    }

    getById() {
        if (this.id) {
            this.supervisionService.getIndicatorByIdAsync(this.id).subscribe(res => {
                this.indicator = res;
                if (res.deptIds) {
                    this.setDepts(this.indicator);
                }
                this.targetList.id = res.id;
                this.targetList.getIndicatorListById();
            });
        }
    }

    showDeptModel() {
        this.modalHelper
            .open(TargetDeptComponent, { selectedDepts: this.deptTags }, 'md', {
                nzMask: true,
                nzClosable: false,
                nzMaskClosable: false,
            })
            .subscribe(isconfirm => {
                if (isconfirm) {
                    this.deptTags.forEach(v => {
                        this.deptInfo.push({ deptId: v.id, deptName: v.name });
                    });
                    // this.setUserDepts(this.document);
                }
            });
    }

    handleDeptClose(tag: any) {
        var i = 0;
        for (const item of this.deptTags) {
            if (item.id == tag.id) {
                this.deptTags.splice(i, 1);
                break;
            }
            i++;
        }
        this.deptInfo = [];
        this.deptTags.forEach(v => {
            this.deptInfo.push({ deptId: v.id, deptName: v.name });
        });
    }

    setDepts(entity: Indicators) {
        this.deptTags = entity.getDepts();
    }

    getDepts() {
        let deptIds = '';
        let deptNames = '';
        for (let u of this.deptTags) {
            deptIds += u.id + ',';
            deptNames += u.name + ',';
        }
        this.indicator.deptIds = (deptIds == '' ? '' : deptIds.substr(0, deptIds.length - 1));
        this.indicator.deptNames = (deptNames == '' ? '' : deptNames.substr(0, deptNames.length - 1));
    }

    return() {
        this.router.navigate(['app/supervision/indicators']);
    }
}