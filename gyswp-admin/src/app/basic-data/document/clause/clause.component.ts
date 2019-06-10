import { Component, OnInit, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/component-base';
import { ClauseDetailComponent } from './clause-detail/clause-detail.component';
import { BasicDataService } from 'services';
import { ClauseTree, Clause } from 'entities';
import { NzModalRef, NzModalService } from 'ng-zorro-antd';

@Component({
    moduleId: module.id,
    selector: 'clause',
    templateUrl: 'clause.component.html',
    styleUrls: ['clause.component.less']
})
export class ClauseComponent extends AppComponentBase implements OnInit {
    doc: any = { id: '', name: '' };
    listOfMapData = [];
    mapOfExpandedData: { [id: string]: TreeNodeInterface[] } = {};
    confirmModal: NzModalRef;

    constructor(injector: Injector
        , private basicDataService: BasicDataService
        , private modal: NzModalService
    ) {
        super(injector);
    }
    ngOnInit(): void {
        // this.getClauseList();

    }

    getClauseList() {
        let params: any = {};
        params.DocumentId = this.doc.id;
        this.basicDataService.GetClauseListAsync(params).subscribe((result) => {
            this.listOfMapData = result
            this.listOfMapData.forEach(item => {
                this.mapOfExpandedData[item.id] = this.convertTreeToList(item);
            });
        });
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

    showDetail(item?: any, type?: string): void {
        var id;
        var pId = '';
        var pNo = '';
        if (type == 'child') {
            pId = item.id;
            pNo = item.clauseNo;
        }
        if (type == 'detail') {
            id = item.id;
        }
        this.modalHelper
            .open(ClauseDetailComponent, { docId: this.doc.id, docName: this.doc.name, pId: pId, pNo: pNo, id: id }, 950, {
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

    deleteClause(item: Clause): void {
        this.confirmModal = this.modal.confirm({
            nzContent: `是否删除当前条款[条款编号：${item.clauseNo}]?`,
            nzOnOk: () => {
                this.basicDataService.DeleteClauseById(item.id).subscribe(res => {
                    if (res.code == 0) {
                        this.notify.info('删除成功！', '');
                        this.getClauseList();
                    } else {
                        this.notify.info('请确保当前条款下无子项条款后再删除！', '');
                    }
                });
            }
        });
    }
}
export interface TreeNodeInterface {
    // key: number;
    // name: string;
    // age: number;
    // level: number;
    // expand: boolean;
    // address: string;
    // children?: TreeNodeInterface[];
    id: string;
    parentId: string;
    content: string;
    clauseNo: string;
    title: string;
    expand: boolean;
    level: number;
    children?: TreeNodeInterface[];
}