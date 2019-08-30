import { Component, OnInit, Injector, Input } from '@angular/core';
import { ModalComponentBase } from '@shared/component-base';
import { SupervisionService } from 'services';
import { NzFormatEmitEvent, NzModalRef, NzModalService } from 'ng-zorro-antd';
import { QuestionBankComponent } from '../question-bank/question-bank.component';

@Component({
    moduleId: module.id,
    selector: 'super-admin',
    templateUrl: 'super-admin.component.html'
})

export class SuperAdminComponent extends ModalComponentBase implements OnInit {
    @Input() type: number;
    title: string;
    nodes: any[];
    selectedDept: any = { id: '', name: '' };
    curDeptId: any;
    confirmModal: NzModalRef;

    constructor(injector: Injector
        , private supervisionService: SupervisionService
        , private modal: NzModalService
    ) {
        super(injector);
    }
    ngOnInit(): void {
        if (this.type) {
            if (this.type == 1) {
                this.title = "标准化办公室考核";
                this.getTrees();
            } else {
                this.title = "县局考核";
                this.getCountryTrees();
            }
        }
    }

    getTrees() {
        this.supervisionService.getDeptTreeByQGAdminAsync().subscribe((data) => {
            this.nodes = data;
            if (data.length > 0) {
                var selectedNode = data[0].children[0];
                this.curDeptId = selectedNode.key;
                if (selectedNode && selectedNode.isSelected) {
                }
            }
        });
    }

    getCountryTrees() {
        this.supervisionService.getDeptTreeByCountyAdminAsync().subscribe((data) => {
            this.nodes = data;
            if (data.length > 0) {
                var selectedNode = data[0].children[0];
                this.curDeptId = selectedNode.key;
                if (selectedNode && selectedNode.isSelected) {
                }
            }
        });
    }

    // 选中节点
    activeNode(data: NzFormatEmitEvent): void {
        if (data.node.key == '0' || data.node.key == '-1' || data.node.key == null || data.node.isDisabled == true) {
            this.selectedDept = { id: '', name: '' };
        } else {
            this.confirmModal = this.modal.confirm({
                nzContent: `是否为[${data.node.title}]生成考核记录信息?`,
                nzOnOk: () => {
                    let params: any = {};
                    params.Type = 2;
                    params.DeptId = data.node.key;
                    params.DeptName = data.node.title;
                    if (this.type == 1)//企管超级管理员考核
                    {
                        this.supervisionService.createExamineByQiGuanAsync(params).subscribe(res => {
                            if (res.code == 0) {
                                this.notify.success('考核表创建成功', '');
                                // this.modal.closeAll();
                                this.showExamineList(res.data.id, res.data.title);
                            }
                            else {
                                this.notify.error('考核表创建失败，请重试！', '');
                            }
                        });
                    } else {//县局超管考核
                        // this.supervisionService.createInternalExamineAsync(params).subscribe(res => {
                        //     if (res.code == 0) {
                        //         this.notify.success('考核表创建成功', '');
                        //         // this.modal.closeAll();
                        //         this.showExamineList(res.data.id, res.data.title);
                        //     }
                        //     else {
                        //         this.notify.error('考核表创建失败，请重试！', '');
                        //     }
                        // });
                        this.supervisionService.createCountryDeptInternalExamineAsync(params).subscribe(res => {
                            if (res.code == 0) {
                                this.notify.success('考核表创建成功', '');
                                // this.modal.closeAll();
                                this.showExamineList(res.data.id, res.data.title);
                            }
                            else {
                                this.notify.error('考核表创建失败，请重试！', '');
                            }
                        });
                    }
                }
            });
        }
    }

    showExamineList(id: string, title: string): void {
        this.modalHelper
            .open(QuestionBankComponent, { examineId: id, title: title }, 1250, {
                nzMask: true,
                nzClosable: false,
                nzMaskClosable: false,
            })
            .subscribe(isSave => {
                if (isSave) {
                    this.success(true);
                }
            });
    }
}
