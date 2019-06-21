import { Component, OnInit, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/component-base';
import { NzModalRef, NzModalService } from 'ng-zorro-antd';
import { WorkCriterionService } from 'services';
import { RevisedListDetailComponent } from '@app/work-criterion/criterion/self-learning/revised-list/revised-list-detail/revised-list-detail.component';
import { ClauseRevision } from 'entities';

@Component({
    moduleId: module.id,
    selector: 'draft-clause',
    templateUrl: 'draft-clause.component.html',
    styleUrls: ['draft-clause.component.less']
})
export class DraftClauseComponent extends AppComponentBase implements OnInit {
    doc: any = { id: '', name: '', isWait: true, applyId: '' };
    listOfMapData = [];
    mapOfExpandedData: { [id: string]: TreeNodeInterface[] } = {};
    confirmModal: NzModalRef;

    constructor(injector: Injector
        , private workCriterionService: WorkCriterionService
        , private modal: NzModalService
    ) {
        super(injector);
        // this.docId = this.actRouter.snapshot.params['id'];
    }
    ngOnInit(): void {
        // this.getClauseList();
    }

    getClauseList() {
        if (this.doc.id) {
            this.workCriterionService.getClauseRevisionListAsync(this.doc.id).subscribe((result) => {
                this.listOfMapData = result
                this.listOfMapData.forEach(item => {
                    this.mapOfExpandedData[item.id] = this.convertTreeToList(item);
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

    editDetail(item?: any, type?: string): void {
        if (this.doc.id) {
            var id;
            var pId = '';
            var pNo = '';
            var applyId = this.doc.applyId;
            if (type == 'child') {
                pId = item.id;
                pNo = item.clauseNo;
            }
            if (type == 'detail') {
                id = item.id;
            }
            this.modalHelper
                .open(RevisedListDetailComponent, { docId: this.doc.id, docName: this.doc.name, pId: pId, pNo: pNo, id: id, applyId: applyId, isDraft: true }, 950, {
                    nzMask: true,
                    nzClosable: false,
                    nzMaskClosable: false,
                })
                .subscribe(isSave => {
                    if (isSave) {
                        this.getClauseList();
                    }
                });
        }
    }

    deleteClause(item: ClauseRevision): void {
        this.confirmModal = this.modal.confirm({
            nzContent: `是否删除当前条款[条款编号：${item.clauseNo}]?`,
            nzOnOk: () => {
                this.workCriterionService.removeDraftRevisionById(item.id).subscribe(res => {
                    if (res.code == 0) {
                        this.notify.info('删除成功！', '');
                        this.getClauseList();
                    } else {
                        this.notify.warn('请确保当前条款下无子项条款后再删除！', '');
                    }
                });
            }
        });
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
    children?: TreeNodeInterface[];
}