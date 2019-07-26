import { Component, Input, Injector } from '@angular/core';
import { ModalComponentBase } from '@shared/component-base';
import { Clause } from 'entities';
import { WorkCriterionService } from 'services';

@Component({
    moduleId: module.id,
    selector: 'revised-clause-detail',
    templateUrl: 'revised-clause-detail.component.html',
    styleUrls: ['revised-clause-detail.component.less']
})
export class RevisedClauseDetailComponent extends ModalComponentBase {
    @Input() docId: string;
    @Input() docName: string;
    @Input() pId: string;
    @Input() pNo: string;
    @Input() id: string;
    @Input() applyId: string;
    title: string = '新增条款';
    clause: Clause = new Clause();
    type: number;
    constructor(injector: Injector
        , private workCriterionService: WorkCriterionService
    ) {
        super(injector);
    }
    ngOnInit(): void {
        this.getClauseById();
    }

    getClauseById() {
        if (this.id) {
            this.title = '修订条款';
            this.workCriterionService.getClauseByIdAsync(this.id).subscribe(res => {
                this.clause = res;
                // this.pNo = res.
            })
        }
    }

    submit() {
        this.saving = true;
        if (this.pId) {
            this.clause.parentId = this.pId;
        }
        this.clause.documentId = this.docId;
        if (this.id) {
            this.type = 2; // 修订
        } else {
            this.type = 1; // 新增
        }
        this.workCriterionService.createRevisionAsync(this.clause, this.type, this.applyId)
            .finally(() => { this.saving = false; })
            .subscribe(res => {
                this.notify.success('修订申请提交成功！', '');
                if (res.code == 0) {
                    this.clause.id = res.data;
                    this.success(true);
                }
            });
    }
}
