import { Component, OnInit, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/component-base';
import { NzModalRef, NzModalService } from 'ng-zorro-antd';
import { WorkCriterionService, BasicDataService } from 'services';
import { ActivatedRoute, Router } from '@angular/router';
import { DocumentDto, Clause } from 'entities';
import * as moment from 'moment';
import { RevisionDetailComponent } from './revision-detail/revision-detail.component';
@Component({
    moduleId: module.id,
    selector: 'revision-draft',
    templateUrl: 'revision-draft.component.html',
    providers: [WorkCriterionService]

})
export class RevisionDraftComponent extends AppComponentBase implements OnInit {
    docId: string;
    isConfirm: boolean;
    listOfMapData = [];
    mapOfExpandedData: { [id: string]: TreeNodeInterface[] } = {};
    docInfo: DocumentDto = new DocumentDto();
    selfChecked = []; // 选中的条款Ids
    isRevisionWaitTime: boolean = false //是否为审批提交后等待阶段
    docStamps: string;//标准状态

    deptId: string;
    type: string;
    date: string;
    constructor(injector: Injector
        , private workCriterionService: WorkCriterionService
        , private actRouter: ActivatedRoute
        , private modal: NzModalService
        , private router: Router
    ) {
        super(injector);
        this.docId = this.actRouter.snapshot.params['id'];
        this.deptId = this.actRouter.snapshot.params['deptId'];
        this.type = this.actRouter.snapshot.params['type'];
        this.date = this.actRouter.snapshot.params['date'];
    }
    ngOnInit(): void {
        this.getDocInfo();
        this.getClauseList();
    }

    getDocInfo() {
        if (this.docId) {
            let params: any = {};
            params.id = this.docId;
            this.workCriterionService.getDetailDocumentTitleAsync(params).subscribe((result) => {
                this.docInfo = result;
                this.docStamps = result.stamps;
                if (result.stamps != null) {
                    if (result.stamps.indexOf(',')) {
                        var res = result.stamps.split(',');
                        var isControl = res[0] == "1" ? "受控" : "非受控";
                        var isValid = res[1] == "3" ? "作废" : "现行有效";
                        this.docStamps = isControl + ',' + isValid;
                    }
                }
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
            .open(RevisionDetailComponent, { docId: this.docInfo.id, docName: this.docInfo.name, id: clauseId }, 950, {
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
    return() {
        this.router.navigate(['app/reports/revision-doc', this.deptId, this.type, this.date]);
    }

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
