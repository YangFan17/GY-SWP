import { Component, Input, Injector } from '@angular/core';
import { WorkCriterionService } from 'services';
import { ModalComponentBase } from '@shared/component-base';
import { Clause, Attachment } from 'entities';

@Component({
    moduleId: module.id,
    selector: 'draft-detail',
    templateUrl: 'draft-detail.component.html',
    providers: [WorkCriterionService]
})
export class DraftDetailComponent extends ModalComponentBase {
    @Input() docId: string;
    @Input() docName: string;
    @Input() id: string;
    title: string = '条款详情';
    disabledClick: boolean = true;
    clause: Clause = new Clause();
    txt: string;
    attachmentList: Attachment[] = [];

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
            this.workCriterionService.getClauseByIdAsync(this.id).subscribe(res => {
                this.clause = res;
                this.txt = this.clause.clauseNo + (this.clause.title ? '\t' + this.clause.title : '') + (this.clause.content ? '\r\n' + this.clause.content : '');
                this.getAttachmentList();
            })
        }
    }

    getAttachmentList() {
        let params: any = {};
        params.BllId = this.id;
        this.workCriterionService.getClauseAttachmentsById(params).subscribe(r => {
            this.attachmentList = r;
        })
    }
}
