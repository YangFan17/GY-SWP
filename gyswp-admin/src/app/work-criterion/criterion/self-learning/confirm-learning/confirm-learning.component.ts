import { Component, Input, Injector } from '@angular/core';
import { ModalComponentBase } from '@shared/component-base';
import { Clause } from 'entities';
import { WorkCriterionService } from 'services';
import { interval } from 'rxjs';
import { take } from 'rxjs/operators';

@Component({
    moduleId: module.id,
    selector: 'confirm-learning',
    templateUrl: 'confirm-learning.component.html',
    styleUrls: ['confirm-learning.component.less']
})
export class ConfirmLearningComponent extends ModalComponentBase {
    @Input() docId: string;
    @Input() docName: string;
    @Input() id: string;
    title: string = '条款详情';
    disabledClick: boolean = true;
    btnText: string = '确认学习';
    clause: Clause = new Clause();
    txt: string;
    constructor(injector: Injector
        , private workCriterionService: WorkCriterionService
    ) {
        super(injector);
    }
    ngOnInit(): void {
        this.getClauseById();
    }

    countDown() {
        const numbers = interval(1000);
        const takeFourNumbers = numbers.pipe(take(5));
        takeFourNumbers.subscribe(
            x => {
                this.btnText = '确认学习（' + (5 - x) + '）';
                this.disabledClick = true;
            },
            error => { },
            () => {
                this.btnText = '确认学习';
                this.disabledClick = false;
            });
    }

    getClauseById() {
        if (this.id) {
            this.workCriterionService.getClauseByIdAsync(this.id).subscribe(res => {
                this.clause = res;
                this.txt = this.clause.clauseNo + (this.clause.title ? '\t' + this.clause.title : '') + (this.clause.content ? '\r\n' + this.clause.content : '');
                this.countDown();
                // this.pNo = res.
            })
        }
    }

    submit() {
        this.clause.documentId = this.docId;
        this.clause.hasAttchment = false;
        this.workCriterionService.selfCheckedClauseAsync(this.id, this.docId)
            .finally(() => { this.saving = false; })
            .subscribe(res => {
                if (res.code == 0) {
                    this.notify.info('已学习！', '');
                    this.success(true);
                } else {
                    this.notify.info('请重试', '');
                    this.success(true);
                }
            });
    }
}
