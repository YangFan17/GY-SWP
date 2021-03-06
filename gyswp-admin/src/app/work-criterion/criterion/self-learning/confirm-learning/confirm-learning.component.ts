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
            this.workCriterionService.GetClauseByIdAsync(this.id).subscribe(res => {
                this.clause = res;
                this.countDown();
                // this.pNo = res.
            })
        }
    }

    submit() {
        // if (this.pId) {
        //     this.clause.parentId = this.pId;
        // }
        // this.clause.documentId = this.docId;
        // this.clause.hasAttchment = false;
        // this.basicDataService.CreateOrUpdateClauseAsync(this.clause)
        //     .finally(() => { this.saving = false; })
        //     .subscribe(res => {
        //         this.notify.info('保存成功！', '');
        //         if (res.code == 0) {
        //             this.clause.id = res.data;
        //             this.success(true);
        //         }
        //     });
    }
}
