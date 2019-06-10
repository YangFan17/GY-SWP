import { Component, Injector, Input } from '@angular/core';
import { Clause } from 'entities';
import { ModalComponentBase } from '@shared/component-base';
import { BasicDataService } from 'services';

@Component({
    moduleId: module.id,
    selector: 'clause-detail',
    templateUrl: 'clause-detail.component.html',
    styleUrls: ['clause-detail.component.less']
})
export class ClauseDetailComponent extends ModalComponentBase {
    @Input() docId: string;
    @Input() docName: string;
    @Input() pId: string;
    @Input() pNo: string;
    @Input() id: string;
    title: string = '条款详情';
    clause: Clause = new Clause();
    constructor(injector: Injector
        , private basicDataService: BasicDataService
    ) {
        super(injector);
    }
    ngOnInit(): void {
        this.getClauseById();
    }

    getClauseById() {
        if (this.id) {
            this.basicDataService.GetClauseByIdAsync(this.id).subscribe(res => {
                this.clause = res;
                // this.pNo = res.
            })
        }
    }

    submit() {
        if (this.pId) {
            this.clause.parentId = this.pId;
        }
        this.clause.documentId = this.docId;
        this.clause.hasAttchment = false;
        this.basicDataService.CreateOrUpdateClauseAsync(this.clause)
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
