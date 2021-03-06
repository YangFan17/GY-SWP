import { Component, OnInit, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/component-base';
import { NzModalRef, NzModalService } from 'ng-zorro-antd';
import { WorkCriterionService } from 'services';
import { ActivatedRoute, Router } from '@angular/router';
import { DocumentDto } from 'entities';
import { getDocument } from 'pdfjs-dist';
import { ConfirmLearningComponent } from './confirm-learning/confirm-learning.component';

@Component({
    moduleId: module.id,
    selector: 'self-learning',
    templateUrl: 'self-learning.component.html',
    styleUrls: ['self-learning.component.less']
})
export class SelfLearningComponent extends AppComponentBase implements OnInit {
    docId: string;
    listOfMapData = [];
    mapOfExpandedData: { [id: string]: TreeNodeInterface[] } = {};
    confirmModal: NzModalRef;
    docInfo: DocumentDto = new DocumentDto();
    selfChecked = []; // 选中的条款Ids
    saving: boolean = false;
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

    getDocInfo() {
        if (this.docId) {
            let params: any = {};
            params.id = this.docId;
            this.workCriterionService.getDocInfoAsync(params).subscribe((result) => {
                this.docInfo = result;
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
                    this.getClauseList();
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
                    this.notify.info('保存成功！', '');
                }
            });
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
    children?: TreeNodeInterface[];
}
