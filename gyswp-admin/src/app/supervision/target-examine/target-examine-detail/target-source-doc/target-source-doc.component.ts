import { Component, OnInit, Output, EventEmitter, Injector } from '@angular/core';
import { ModalPagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { SupervisionService } from 'services';

@Component({
    moduleId: module.id,
    selector: 'target-source-doc',
    templateUrl: 'target-source-doc.component.html'
})

export class TargetSourceDocComponent extends ModalPagedListingComponentBase<any> implements OnInit {
    @Output() modalSelect: EventEmitter<any> = new EventEmitter<any>();
    constructor(injector: Injector
        , private supervisionService: SupervisionService
    ) {
        super(injector);
    }

    // ngOnInit() {

    // }

    refresh(): void {
        this.getDataPage(this.pageNumber);
    }
    refreshData() {
        this.pageNumber = 1;
        this.refresh();
    }

    protected getDataList(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
        let params: any = {};
        params.SkipCount = request.skipCount;
        params.MaxResultCount = request.maxResultCount;
        this.supervisionService.getCurDeptDocListAsync(params)
            .finally(() => {
                finishedCallback();
            })
            .subscribe((result: PagedResultDto) => {
                this.dataList = result.items
                this.totalItems = result.totalCount;
            });
    }

    selectDoc(doc: any) {
        if (doc.id) {
            this.modalSelect.emit(doc);
            this.success(doc);
        }
    }
}
