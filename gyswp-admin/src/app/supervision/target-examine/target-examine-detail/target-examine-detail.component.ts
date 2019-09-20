import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/component-base';
import { ActivatedRoute, Router } from '@angular/router';
import { SupervisionService } from 'services';
import { Indicators } from 'entities';
import { TargetDeptComponent } from './target-dept/target-dept.component';
import { NzModalRef, NzModalService } from 'ng-zorro-antd';
import { TargetListComponent } from './target-list/target-list.component';
import { TargetSourceDocComponent } from './target-source-doc/target-source-doc.component';

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
    achieveTypes = [
        { value: 1, text: '大于 >' },
        { value: 2, text: '大于等于 >=' },
        { value: 3, text: '小于 <' },
        { value: 4, text: '小于等于 <=' },
        { value: 5, text: '等于 =' }
    ];
    doc: any = {};

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
        } else {
            this.indicator.isAction = true;
        }
    }

    cycleTimeRadioChange(ngmodel: string) {
        this.cycleTime = ngmodel;
    }

    save(): void {
        // this.confirmModal = this.modal.confirm({
        //     nzContent: '发布后将无法修改，是否发布?',
        //     nzOnOk: () => {
        //         this.indicator.cycleTime = this.cycleTime == '1' ? 1 : 2;
        //         this.getDepts();
        //         this.indicator.sourceDocId = this.doc.id;
        //         this.supervisionService.createOrUpdateIndicatorAsync(this.indicator, this.deptInfo)
        //             .finally(() => { this.saving = false; })
        //             .subscribe(res => {
        //                 this.notify.success('发布成功！', '');
        //                 if (res.code == 0) {
        //                     this.indicator.id = res.data;
        //                     this.id = this.indicator.id;
        //                     this.targetList.id = this.id;
        //                     this.targetList.getIndicatorListById();
        //                 }
        //             });
        //     }
        // });
        this.saving = true;
        this.indicator.cycleTime = this.cycleTime == '1' ? 1 : 2;
        this.getDepts();
        this.indicator.sourceDocId = this.doc.id;
        this.supervisionService.createOrUpdateIndicatorAsync(this.indicator)
            .finally(() => { this.saving = false; })
            .subscribe(res => {
                if (res.code == 0) {
                    this.id = res.data;
                    this.notify.success('保存成功！', '');
                    this.getById();
                } else {
                    this.notify.error('保存失败，请重试！', '');
                }
            });
    }

    action() {
        this.confirmModal = this.modal.confirm({
            nzContent: `是否${this.indicator.isAction == true ? '废止' : '启用'}当前指标?`,
            nzOnOk: () => {
                this.saving = true;
                let input: any = {};
                input.id = this.indicator.id;
                input.isAction = !this.indicator.isAction;
                this.supervisionService.changeActionStatus(input)
                    .finally(() => { this.saving = false; })
                    .subscribe(res => {
                        if (res.code == 0) {
                            this.notify.success('保存成功！', '');
                            this.getById();
                        } else {
                            this.notify.error('保存失败，请重试！', '');
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
                this.doc.name = res.sourceDocName;
                this.doc.id = res.sourceDocId;
                // this.targetList.id = res.id;
                // this.targetList.getIndicatorListById();
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


    chooseDoc() {
        this.modalHelper
            .open(TargetSourceDocComponent, {}, 'lg', {
                nzMask: true,
                nzClosable: false,
                nzMaskClosable: false,
            })
            .subscribe(doc => {
                if (doc) {
                    this.doc.id = doc.id;
                    this.doc.name = doc.name;
                }
            });
    }

    return() {
        this.router.navigate(['app/supervision/indicators']);
    }
}