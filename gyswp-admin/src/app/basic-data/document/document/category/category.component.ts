import { Component, Input, ViewChild, Output, EventEmitter, Injector, TemplateRef, OnInit } from '@angular/core';
import { NzTreeComponent, NzDropdownContextComponent, NzTreeNode, NzModalRef, NzDropdownService, NzModalService, NzFormatEmitEvent } from 'ng-zorro-antd';
import { Category } from 'entities';
import { BasicDataService } from 'services';
import { AppComponentBase } from '@shared/component-base';
import { EditCategoryComponent } from '../edit-category/edit-category.component';
import { CreateCategoryComponent } from '../create-category/create-category.component';
import { QrCodeCategoryComponent } from '../qr-code-category/qr-code-category.component';
import { UploadTxtComponent } from '../upload-txt/upload-txt.component';

@Component({
    moduleId: module.id,
    selector: 'category',
    templateUrl: 'category.component.html',
    styleUrls: ['category.component.less']
})
export class CategoryComponent extends AppComponentBase implements OnInit {
    @Input() name: string;
    @Input() deptId: any;
    @Input() deptName: string;

    @ViewChild('treeCom') treeCom: NzTreeComponent;
    dropdown: NzDropdownContextComponent;
    showMenu: boolean = false;
    activedNode: NzTreeNode;
    rkeyNode = { key: '', title: '', origin: { parentId: null } };
    nodes = [];
    searchName;
    @Output() selectedCategory = new EventEmitter<any>();
    confirmModal: NzModalRef;

    constructor(injector: Injector, private nzDropdownService: NzDropdownService
        , private basicDataService: BasicDataService
        , private modal: NzModalService) {
        super(injector);
    }

    ngOnInit(): void {
        this.getTreeAsync();
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

    contextMenu($event: MouseEvent, template: TemplateRef<void>, node): void {
        if (node.title == '技术标准' || node.title == '管理标准' || node.title == '工作标准' || node.title == '外来文件' || node.title == '作废标准库' || node.title == '现行标准库' || node.title == '风险库') {
            // 默认分类无法修改
            this.showMenu = false;
            this.dropdown = this.nzDropdownService.create($event, template);
            this.rkeyNode = node;
        } else {
            this.showMenu = true;
            this.dropdown = this.nzDropdownService.create($event, template);
            this.rkeyNode = node;
        }
    }

    edit(): void {
        if (this.dropdown) {
            this.dropdown.close();
        }
        var category = new Category();
        category.id = parseInt(this.rkeyNode.key);
        category.name = this.rkeyNode.title;
        category.parentId = this.rkeyNode.origin.parentId;
        category.deptId = this.deptId;
        this.modalHelper
            .open(EditCategoryComponent, { category: category }, 'md', {
                nzMask: true,
                nzClosable: false,
                nzMaskClosable: false,
            })
            .subscribe(isSave => {
                if (isSave) {
                    this.getTreeAsync();
                }
            });
    }

    create(key: 'click' | 'r-key'): void {
        if (!this.deptId) {
            this.message.warn('请选择维护部门');
            return;
        }
        if (this.dropdown) {
            this.dropdown.close();
        }
        var pid;
        var pname;
        if (key === 'r-key') {
            pid = this.rkeyNode.key;
            pname = this.rkeyNode.title;
        }
        this.modalHelper
            .open(CreateCategoryComponent, { pid: pid, pname: pname, deptId: this.deptId }, 'md', {
                nzMask: true,
                nzClosable: false,
                nzMaskClosable: false,
            })
            .subscribe(isSave => {
                if (isSave) {
                    this.getTreeAsync();
                }
            });
    }

    qrCodeDetail(key: 'r-key'): void {
        if (this.dropdown) {
            this.dropdown.close();
        }
        var pname;
        var pcode;
        if (key === 'r-key') {
            pname = this.rkeyNode.title;
            pcode = this.rkeyNode.key;
        }
        this.modalHelper
            .open(QrCodeCategoryComponent, { pname: pname, pcode: pcode }, 'md', {
                nzMask: true,
                nzClosable: false,
                nzMaskClosable: false,
            })
            .subscribe(isSave => {
                if (isSave) {
                }
            });
    }

    uploadTxt(): void {
        if (this.dropdown) {
            this.dropdown.close();
        }
        this.modalHelper
            .open(UploadTxtComponent, { deptId: this.deptId, categoryId: this.rkeyNode.key, categoryName: this.rkeyNode.title }, 'md', {
                nzMask: true,
                nzClosable: true,
                nzMaskClosable: false,
            })
            .subscribe(isSave => {
                if (isSave) {
                    // this.getTreeAsync(this.deptId);
                }
            });
    }

    getTreeAsync(deptId?: any) {
        if (!deptId) {
            deptId = this.deptId;
        }
        this.basicDataService.getTreeByCategoryAsync(deptId).subscribe(res => {
            this.nodes = res;
        });
    }

    nzEvent(event: NzFormatEmitEvent): void {
    }

    deleteCate(): void {
        this.confirmModal = this.modal.confirm({
            nzContent: `是否删除当前分类[${this.rkeyNode.title}]?`,
            nzOnOk: () => {
                this.basicDataService.deleteCategoryById(parseInt(this.rkeyNode.key)).subscribe(res => {
                    if (this.dropdown) {
                        this.dropdown.close();
                    }
                    if (res.code == 0) {
                        this.notify.success('删除成功！', '');
                        this.getTreeAsync();
                    } else {
                        this.notify.info('请确保当前分类下无子类或文件后再删除！', '');
                    }
                });
            }
        });
    }
}
