import { Component, OnInit, Input, Injector } from '@angular/core';
import { ModalComponentBase } from '@shared/component-base';
import { ExamineFeedback } from 'entities';
import { SupervisionService } from 'services';

@Component({
    moduleId: module.id,
    selector: 'feedback-result',
    templateUrl: 'feedback-result.component.html'
})
export class FeedbackResultComponent extends ModalComponentBase implements OnInit {
    @Input() id: string; //examineDetailId
    courseTypes = [
        { label: '人', value: '1', checked: false },
        { label: '机', value: '2', checked: false },
        { label: '料', value: '3', checked: false },
        { label: '法', value: '4', checked: false },
        { label: '环', value: '5', checked: false },
    ];
    examineFeedback: ExamineFeedback = new ExamineFeedback();
    splitCodes: string[];

    constructor(injector: Injector
        , private supervisionService: SupervisionService
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.getExamineFeedback();
    }

    getExamineFeedback() {
        if (this.id) {
            this.supervisionService.getExamineFeedbackByIdAsync(this.id).subscribe(res => {
                this.examineFeedback = res;
                if (res.courseType) {
                    this.splitCodes = res.courseType.split(',');
                    let i: number = 0;
                    this.courseTypes.forEach(v => {
                        if (v.value == this.splitCodes[i]) {
                            v.checked = true;
                            if (i < this.splitCodes.length) {
                                i++;
                            }
                        }
                    }
                    );
                }
            })
        }
    }
}
