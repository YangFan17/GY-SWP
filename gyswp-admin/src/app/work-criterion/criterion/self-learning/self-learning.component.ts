import { Component, OnInit, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/component-base';
import { NzModalRef, NzModalService } from 'ng-zorro-antd';
import { WorkCriterionService, BasicDataService } from 'services';
import { ActivatedRoute, Router } from '@angular/router';
import { DocumentDto, Clause } from 'entities';
import { ConfirmLearningComponent } from './confirm-learning/confirm-learning.component';
import { DocApplicationComponent } from './doc-application/doc-application.component';
import { ClauseDetailComponent } from '@app/basic-data/document/clause/clause-detail/clause-detail.component';
import { RevisedListComponent } from './revised-list/revised-list.component';
import { RevisedClauseDetailComponent } from './revised-clause-detail/revised-clause-detail.component';
import * as moment from 'moment';

@Component({
    moduleId: module.id,
    selector: 'self-learning',
    templateUrl: 'self-learning.component.html',
    styleUrls: ['self-learning.component.less']
})
export class SelfLearningComponent extends AppComponentBase implements OnInit {
    docId: string;
    isConfirm: boolean;
    listOfMapData = [];
    mapOfExpandedData: { [id: string]: TreeNodeInterface[] } = {};
    confirmModal: NzModalRef;
    docInfo: DocumentDto = new DocumentDto();
    selfChecked = []; // 选中的条款Ids
    saving: boolean = false;
    isApply: boolean = false; // 是否可点击申请按钮
    isRevision: boolean = false; // 是否允许制修订
    editMode: boolean = false; //进入编辑模式
    applyId: string; // 申请id
    // isSaveApply: boolean = false //是否可提交保存
    isRevisionWaitTime: boolean = false //是否为审批提交后等待阶段
    docStamps: string;//标准状态
    constructor(injector: Injector
        , private workCriterionService: WorkCriterionService
        , private actRouter: ActivatedRoute
        , private modal: NzModalService
        , private router: Router
    ) {
        super(injector);
        this.docId = this.actRouter.snapshot.params['id'];
    }
    ngOnInit(): void {
        this.getDocInfo();
        this.getClauseList();
    }

    getUserOperate() {
        let params: any = {};
        params.docId = this.docId;
        this.workCriterionService.getUserOperateAsync(params).subscribe((result) => {
            if (result.code == 0) {
                this.isConfirm = result.data.isConfirm;
                this.isApply = result.data.isApply;
                this.isRevision = result.data.isRevision;
                this.editMode = result.data.editModel;
                this.applyId = result.data.applyId;
                // this.isSaveApply = result.data.isSave;
                this.isRevisionWaitTime = result.data.isRevisionWaitTime;
                // console.log(result.data);
            } else {
                this.notify.error('请重试！');
            }
        });
    }

    getDocInfo() {
        if (this.docId) {
            let params: any = {};
            params.id = this.docId;
            this.workCriterionService.getDocInfoAsync(params).subscribe((result) => {
                this.docInfo = result;
                this.docStamps = result.stamps;
                if (result.stamps != null) {
                    // result.stamps.replace('1', "受控");
                    // result.stamps.replace('2', "非受控");
                    // result.stamps.replace('3', "作废");
                    // result.stamps.replace('4', "现行有效");

                    if (result.stamps.indexOf(',')) {
                        var res = result.stamps.split(',');
                        var isControl = res[0] == "1" ? "受控" : "非受控";
                        var isValid = res[1] == "3" ? "作废" : "现行有效";
                        this.docStamps = isControl + ',' + isValid;
                    }
                }
                this.getUserOperate();
            });
        }
    }
    getClauseList() {
        if (this.docId) {
            let params: any = {};
            params.DocumentId = this.docId;
            this.workCriterionService.getClauseListAsync(params).subscribe((result) => {
                this.listOfMapData = result
                this.listOfMapData.forEach(item => {
                    this.mapOfExpandedData[item.id] = this.convertTreeToList(item);
                    // console.log(this.mapOfExpandedData[item.id]);
                    // 初始化checkBox数据
                    this.mapOfExpandedData[item.id].forEach(v => {
                        //判断确认过的条款
                        if (v.checked == true) {
                            this.selfChecked.push(v.id);
                        }
                        if (v.bllId) {
                            let pickDate: moment.Moment;
                            if (v.lastModificationTime) {
                                pickDate = moment(v.lastModificationTime);
                            }
                            else if (v.creationTime) {
                                pickDate = moment(v.creationTime);
                            }
                            let diff = pickDate.diff(moment(), 'days');//相差几天
                            if (diff > 0 - 90) {
                                v.isNew = true;
                            }
                        }
                    })
                });
            });
        }
    }

    collapse(array: TreeNodeInterface[], data: TreeNodeInterface, $event: boolean): void {
        if ($event === false) {
            if (data.children.length > 0) {
                data.children.forEach(d => {
                    const target = array.find(a => a.id === d.id)!;
                    target.expand = false;
                    this.collapse(array, target, false);
                });
            } else {
                return;
            }
        }
    }

    convertTreeToList(root: object): TreeNodeInterface[] {
        const stack: any[] = [];
        const array: any[] = [];
        const hashMap = {};
        stack.push({ ...root, level: 0, expand: true });

        while (stack.length !== 0) {
            const node = stack.pop();
            this.visitNode(node, hashMap, array);
            if (node.children.length > 0) {
                for (let i = node.children.length - 1; i >= 0; i--) {
                    stack.push({ ...node.children[i], level: node.level + 1, expand: true, parent: node });
                }
            }
        }
        return array;
    }

    visitNode(node: TreeNodeInterface, hashMap: { [id: string]: any }, array: TreeNodeInterface[]): void {
        if (!hashMap[node.id]) {
            hashMap[node.id] = true;
            array.push(node);
        }
    }

    showDetail(clauseId: string): void {
        this.modalHelper
            .open(ConfirmLearningComponent, { docId: this.docInfo.id, docName: this.docInfo.name, id: clauseId }, 950, {
                nzMask: true,
                nzClosable: false,
                nzMaskClosable: false,
            })
            .subscribe(isSave => {
                if (isSave) {
                    // this.getClauseList();
                }
            });
    }

    goPre(id: string) {
        this.router.navigate(['app/criterion/pre-doc', id]);
    }

    return() {
        this.router.navigate(['app/criterion/criterion']);
    }

    //#region 确认条款
    isCancelCheck(bool: boolean, id: any) {
        if (bool && !this.existsChecked(id)) {
            this.selfChecked.push(id);
        } else {
            let i = 0;
            this.selfChecked.forEach(v => {
                if (v == id) {
                    this.selfChecked.splice(i, 1);
                    return;
                }
                i++;
            });
        }
    }

    /**
     * 排重
     * @param id 
     */
    existsChecked(id: string): boolean {
        let bo = false;
        this.selfChecked.forEach(v => {
            if (v.id == id) {
                bo = true;
                return;
            }
        });
        return bo;
    }

    confirmClause() {
        this.saving = true;
        this.workCriterionService.confirmClauseAsync(this.selfChecked, this.docInfo.id)
            .finally(() => { this.saving = false; })
            .subscribe(res => {
                if (res.code == 0) {
                    this.notify.success('保存成功！', '');
                    this.isConfirm = true;
                }
            });
    }

    resetChange(): void {
        this.confirmModal = this.modal.confirm({
            nzContent: `是否重新确认适用条款?`,
            nzOnOk: () => {
                this.isConfirm = false;
            }
        });
    }
    //#endregion

    docApply(): void {
        this.modalHelper
            .open(DocApplicationComponent, { docId: this.docInfo.id, docName: this.docInfo.name }, 950, {
                nzMask: true,
                nzClosable: false,
                nzMaskClosable: false,
            })
            .subscribe(isSave => {
                if (isSave) {
                    // this.getIsConfirm();
                    this.isApply = false;
                }
            });
    }

    //#region 制修订操作
    revisedList(): void {
        if (this.docId) {
            this.modalHelper
                .open(RevisedListComponent, { docId: this.docId, applyId: this.applyId }, 1250, {
                    nzMask: true,
                    nzClosable: false,
                    nzMaskClosable: false,
                })
                .subscribe(isSave => {
                    if (isSave) {
                        this.isConfirm = true;
                        this.isApply = false;
                        this.isRevision = false;
                        this.editMode = false;
                        this.applyId = null;
                        this.isRevisionWaitTime = true;
                    }
                });
        }
    }

    editDetail(item?: any, type?: string): void {
        if (this.docId) {
            var id;
            var pId = '';
            var pNo = '';
            var applyId = this.applyId;
            if (type == 'child') {
                pId = item.id;
                pNo = item.clauseNo;
            }
            if (type == 'detail') {
                id = item.id;
            }
            this.modalHelper
                .open(RevisedClauseDetailComponent, { docId: this.docId, docName: this.docInfo.name, pId: pId, pNo: pNo, id: id, applyId: applyId }, 950, {
                    nzMask: true,
                    nzClosable: false,
                    nzMaskClosable: false,
                })
                .subscribe(isSave => {
                    if (isSave) {
                        // this.getClauseList();
                    }
                });
        }
    }

    deleteDetail(item: Clause): void {
        if (this.applyId) {
            this.confirmModal = this.modal.confirm({
                nzContent: `是否申请删除当前条款[条款编号：${item.clauseNo}]?`,
                nzOnOk: () => {
                    this.workCriterionService.deleteClauseById(item.id, this.docId, this.applyId).subscribe(res => {
                        if (res.code == 0) {
                            this.notify.success('删除申请提交成功！', '');
                        } else if (res.code == 2) {
                            this.notify.warn('重复删除，请前往‘已修订列表’进行确认！', '');
                        } else if (res.code == 3) {
                            this.notify.warn('已修订过的条款无法删除，请前往‘已修订列表’进行确认！', '');
                        }
                        else {
                            this.notify.warn('请确保当前条款下无子项条款后再申请删除！', '');
                        }
                    });
                }
            });
        }
    }
    //#endregion
}

export interface TreeNodeInterface {
    id: string;
    parentId: string;
    content: string;
    clauseNo: string;
    title: string;
    expand: boolean;
    level: number;
    checked: boolean;
    bllId: string;
    isNew: boolean;
    lastModificationTime: Date;
    creationTime: Date;
    children?: TreeNodeInterface[];
}
