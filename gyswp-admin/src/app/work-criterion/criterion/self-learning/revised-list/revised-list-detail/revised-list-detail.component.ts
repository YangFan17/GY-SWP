import { Component, Input, Injector } from '@angular/core';
import { ModalComponentBase } from '@shared/component-base';
import { ClauseRevision } from 'entities';
import { WorkCriterionService } from 'services';

@Component({
    moduleId: module.id,
    selector: 'revised-list-detail',
    templateUrl: 'revised-list-detail.component.html',
    styleUrls: ['revised-list-detail.component.less']
})
export class RevisedListDetailComponent extends ModalComponentBase {
    @Input() docId: string;
    @Input() pId: string;
    @Input() pNo: string;
    @Input() id: string;
    @Input() applyId: string;
    title: string = '条款详情';
    clause: ClauseRevision = new ClauseRevision();
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
            this.workCriterionService.getClauseRevisionByIdAsync(this.id).subscribe(res => {
                this.clause = res;
            })
        }
    }

    submit() {
        this.saving = true;
        if (this.pId) {
            this.clause.parentId = this.pId;
        }
        this.clause.documentId = this.docId;
        this.clause.hasAttchment = false;
        this.clause.applyInfoId = this.applyId;
        this.workCriterionService.createOrUpdateRevisionAsync(this.clause)
            .finally(() => { this.saving = false; })
            .subscribe(res => {
                this.notify.info('保存成功！', '');
                if (res.code == 0) {
                    this.clause.id = res.data;
                    this.success(true);
                }
            });
    }
}
