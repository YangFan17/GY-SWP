import { Component, Input, Injector } from '@angular/core';
import { ModalComponentBase } from '@shared/component-base';
import { Clause } from 'entities';
import { SupervisionService } from 'services';

@Component({
    moduleId: module.id,
    selector: 'question-bank-detail',
    templateUrl: 'question-bank-detail.component.html'
})
export class QuestionBankDetailComponent extends ModalComponentBase {
    @Input() id: string;
    title: string = '条款详情';
    clause: Clause = new Clause();
    txt: string;//简化显示内容

    constructor(injector: Injector
        , private supervisionService: SupervisionService
    ) {
        super(injector);
    }
    ngOnInit(): void {
        this.getClauseById();
    }

    getClauseById() {
        if (this.id) {
            this.supervisionService.getClauseByIdAsync(this.id).subscribe(res => {
                this.clause = res;
                this.txt = this.clause.clauseNo + (this.clause.title ? '\t' + this.clause.title : '') + (this.clause.content ? '\r\n' + this.clause.content : '');
            })
        }
    }
}