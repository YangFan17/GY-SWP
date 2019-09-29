import { Component, OnInit, ViewChild, Output, EventEmitter, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/component-base';
import { NzTreeComponent, NzDropdownContextComponent, NzTreeNode, NzModalRef, NzDropdownService, NzModalService, NzFormatEmitEvent } from 'ng-zorro-antd';

@Component({
    moduleId: module.id,
    selector: 'active-category',
    templateUrl: 'active-category.component.html',
    styleUrls: ['active-category.component.scss']
})
export class ActiveCategoryComponent extends AppComponentBase implements OnInit {
    @ViewChild('treeCom') treeCom: NzTreeComponent;
    activedNode: NzTreeNode;
    nodes = [];
    searchName;
    @Output() selectedCategory = new EventEmitter<any>();

    constructor(injector: Injector, private nzDropdownService: NzDropdownService
        , private modal: NzModalService) {
        super(injector);
    }

    ngOnInit(): void {
        // this.getTreeAsync();
    }

    openFolder(data: NzTreeNode | NzFormatEmitEvent): void {
        if (data instanceof NzTreeNode) {
            data.isExpanded = !data.isExpanded;
        } else {
            data.node.isExpanded = !data.node.isExpanded;
        }
    }

    activeNode(data: NzFormatEmitEvent): void {
        if (this.activedNode) {
            this.treeCom.nzTreeService.setSelectedNodeList(this.activedNode, false);
        }
        data.node.isSelected = true;
        this.activedNode = data.node;
        if (this.selectedCategory) {
            var catg = { id: this.activedNode.key, name: this.activedNode.title };
            this.selectedCategory.emit(catg);
        }
        this.treeCom.nzTreeService.setSelectedNodeList(this.activedNode, false);
    }
}
