import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/component-base';
import { Router, ActivatedRoute } from '@angular/router';
import { WorkCriterionService } from 'services';
import { SelectGroup, DocRevision } from 'entities';
import { DraftClauseComponent } from './draft-clause/draft-clause.component';
import { NzModalRef, NzModalService } from 'ng-zorro-antd';

@Component({
    moduleId: module.id,
    selector: 'draft-doc',
    templateUrl: 'draft-doc.component.html',
    styleUrls: ['draft-doc.component.less']
})
export class DraftDocComponent extends AppComponentBase implements OnInit {
    @ViewChild('draftClause') draftClause: DraftClauseComponent;
    confirmModal: NzModalRef;
    categories = [];
    document: DocRevision = new DocRevision();
    applyId: string;
    id: string;
    isRevisionWaitTime: boolean = false //是否为审批提交后等待阶段
    deptName: string;

    constructor(injector: Injector
        , private router: Router
        , private actRouter: ActivatedRoute
        , private modal: NzModalService
        , private workCriterionService: WorkCriterionService) {
        super(injector);
        this.applyId = this.actRouter.snapshot.params['id'];
    }
    ngOnInit(): void {
        this.getCategoryType();
        this.getDraftOperateDraftAsync();
    }
    getDraftOperateDraftAsync() {
        this.workCriterionService.getDraftOperateDraftAsync().subscribe((result) => {
            if (result.code == 0) {
                this.isRevisionWaitTime = result.data.isRevisionWaitTime;
                this.deptName = result.data.deptName;
            } else {
                this.notify.error('请重试！');
            }
        });
    }
    getCategoryType() {
        this.workCriterionService.getCategoryTypeAsync().subscribe((result: SelectGroup[]) => {
            result = result.filter(v => v.text != '作废标准库');
            this.categories.push(...result);
            this.document.categoryId = this.categories[0].value;
            this.getById();
        });
    }

    getById() {
        if (this.applyId) {
            this.workCriterionService.getDocRevisionByIdAsync(this.applyId).subscribe(res => {
                if (res.id) {
                    this.document = res;
                    this.document.categoryId = res.categoryId.toString();
                    this.id = this.document.id;
                    // this.category.id = res.categoryId.toString();
                    // this.category.name = res.categoryDesc;
                    this.draftClause.doc = { id: res.id, name: res.name, applyId: res.applyInfoId, isWait: this.isRevisionWaitTime };
                    this.draftClause.getClauseList();
                }
            });
        }
    }

    save() {
        if (!this.document.id) {
            this.document.applyInfoId = this.applyId;
        }
        this.workCriterionService.createOrUpdateDocRevisionAsync(this.document)
            .finally(() => { this.saving = false; })
            .subscribe(res => {
                this.notify.info('保存成功！', '');
                if (res.data) {
                    this.document = res.data;
                    this.document.categoryId = res.data.categoryId.toString();
                    this.id = this.document.id;
                }
                this.draftClause.doc = { id: res.data.id, name: res.data.name, applyId: this.applyId, isWait: false };
            });
    }

    saveDraftDoc(): void {
        if (this.applyId && this.id) {
            this.saving = true;
            this.confirmModal = this.modal.confirm({
                nzContent: `是否提交当前所制定的标准?`,
                nzOnOk: () => {
                    this.workCriterionService.saveDraftDoc(this.applyId, this.id).finally(() => { this.saving = false; }).subscribe(res => {
                        if (res.code == 0) {
                            this.notify.info('制订申请提交成功！', '');
                            this.modal.closeAll();
                            this.return();
                        }
                        else {
                            this.notify.error('制订申请提交失败，请重试！', '');
                        }
                    });
                },
                nzOnCancel: () => {
                    this.saving = false;
                }
            });
        }
    }

    return() {
        this.router.navigate(['app/criterion/criterion']);
    }
}
