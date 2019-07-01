import { Component, ViewChild, Injector, OnInit } from '@angular/core';
import { NzTreeComponent, NzFormatEmitEvent } from 'ng-zorro-antd';
import { BasicDataService } from 'services';
import { AppComponentBase } from '@shared/component-base';
import { ListComponent } from './list/list.component';
import { CategoryComponent } from './category/category.component';

@Component({
    moduleId: module.id,
    selector: 'document',
    templateUrl: 'document.component.html',
    styleUrls: ['document.component.less']
})
export class DocumentComponent extends AppComponentBase implements OnInit {
    @ViewChild('docList') docList: ListComponent;
    @ViewChild('docCategory') docCategory: CategoryComponent;
    @ViewChild('detpTree') detpTree: NzTreeComponent;
    categoryName: string;
    nodes: any[];
    selectedDept: any = { id: '', name: '' };

    constructor(injector: Injector
        , private basicDataService: BasicDataService) {
        super(injector);
    }

    ngOnInit(): void {
        this.getTrees();
    }

    onSelectedCategory(selected: any) {
        this.docList.selectedCategory = selected;
        this.docList.dept = this.selectedDept;
        this.docList.refresh();
    }

    getTrees() {
        this.basicDataService.getDeptDocNzTreeNodes('标准归口部门').subscribe((data) => {
            this.nodes = data;
            if (data.length > 0) {
                var selectedNode = data[0].children[0];
                if (selectedNode && selectedNode.isSelected) {
                    this.selectedDept = { id: selectedNode.key, name: selectedNode.title };
                    this.docCategory.getTreeAsync(selectedNode.key);
                    this.docList.dept = this.selectedDept;
                    this.docList.refresh();
                }
            }
        });
    }

    // 选中节点
    activeNode(data: NzFormatEmitEvent): void {
        if (data.node.key == '0' || data.node.key == '-1') {
            this.selectedDept = { id: '', name: '' };
        } else {
            this.selectedDept = { id: data.node.key, name: data.node.title };
        }
        this.docCategory.getTreeAsync(data.node.key);
        this.docList.dept = this.selectedDept;
        this.docList.refresh();
    }

}
