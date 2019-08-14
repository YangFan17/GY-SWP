import { Component, OnInit, Injector, Input, Output, EventEmitter } from '@angular/core';
import { HomeService } from 'services/home/home.service';
import { PagedRequestDto, PagedResultDto, ModalComponentBase, ModalPagedListingComponentBase } from '@shared/component-base';

@Component({
    moduleId: module.id,
    selector: 'emp-doc-list',
    templateUrl: 'emp-doc-list.component.html',
    providers: [HomeService]
})
export class EmpDocListComponent extends ModalPagedListingComponentBase<any> implements OnInit {
    @Output() modalSelect: EventEmitter<any> = new EventEmitter<any>();
    constructor(injector: Injector
        , private homeService: HomeService
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
        this.homeService.getDocumentListAsync(params)
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
