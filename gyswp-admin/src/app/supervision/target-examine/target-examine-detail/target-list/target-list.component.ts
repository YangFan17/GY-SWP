import { Component, OnInit, Input, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/component-base';
import { ActivatedRoute, Router } from '@angular/router';
import { SupervisionService } from 'services';
import { NzModalService } from 'ng-zorro-antd';
import { IndicatorShowDto } from 'entities';
import { FeedbackResultComponent } from '@app/supervision/criterion-examine/feedback-result/feedback-result.component';
import { FullInResultComponent } from './full-in-result/full-in-result.component';

@Component({
    moduleId: module.id,
    selector: 'target-list',
    templateUrl: 'target-list.component.html'
})
export class TargetListComponent extends AppComponentBase implements OnInit {
    @Input() id: string;
    indicatorList: IndicatorShowDto[] = [];
    constructor(injector: Injector
        , private actRouter: ActivatedRoute
        , private router: Router
        , private supervisionService: SupervisionService
        , private modal: NzModalService
    ) {
        super(injector);
        this.id = this.actRouter.snapshot.params['id'];
    }

    ngOnInit(): void {
        if (this.id) {
            this.getIndicatorListById();
        }
    }
    getIndicatorListById() {
        this.supervisionService.getIndicatorListById(this.id).subscribe(res => {
            this.indicatorList = res;
        });
    }

    resultFeedBack(id: string): void {
        this.modalHelper
            .open(FeedbackResultComponent, { id: id }, 950, {
                nzMask: true,
                nzClosable: false,
                nzMaskClosable: false,
            })
            .subscribe(isSave => {
                if (isSave) {
                }
            });
    }

    fullResult(id: string): void {
        this.modalHelper
            .open(FullInResultComponent, { id: id }, 'md', {
                nzMask: true,
                nzClosable: false,
                nzMaskClosable: false,
            })
            .subscribe(isSave => {
                if (isSave) {
                    this.getIndicatorListById();
                }
            });
    }
}
