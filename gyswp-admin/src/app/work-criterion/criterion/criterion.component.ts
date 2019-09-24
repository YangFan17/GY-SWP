import { Component, Injector } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { WorkCriterionService } from 'services';
import { Router } from '@angular/router';
import { SelectGroup } from 'entities';
import { DraftApplyInfoComponent } from './draft-doc/draft-apply-info/draft-apply-info.component';

@Component({
    moduleId: module.id,
    selector: 'criterion',
    templateUrl: 'criterion.component.html',
    styleUrls: ['criterion.component.less']
})
export class CriterionComponent extends PagedListingComponentBase<any>{
    search: any = { keyWord: '', categoryId: '0' };
    categories = [];
    isApply: boolean = false; // 是否可点击申请按钮
    isRevision: boolean = false; // 是否允许制修订
    editMode: boolean = false; //进入编辑模式
    applyId: string; // 申请id
    isRevisionWaitTime: boolean = false //是否为审批提交后等待阶段

    constructor(injector: Injector
        , private router: Router
        , private workCriterionService: WorkCriterionService) {
        super(injector);
    }

    ngOnInit(): void {
        this.getCategoryType();
        this.getUserOperateDraftAsync();
    }

    getCategoryType() {
        this.workCriterionService.getCategoryTypeAsync().subscribe((result: SelectGroup[]) => {
            this.categories.push(SelectGroup.fromJS({ value: '0', text: '全部' }));
            result = result.filter(v => v.text != '作废标准库');
            this.categories.push(...result);
            this.refresh();
        });
    }

    getUserOperateDraftAsync() {
        this.workCriterionService.getUserOperateDraftAsync().subscribe((result) => {
            if (result.code == 0) {
                this.isApply = result.data.isApply;
                this.isRevision = result.data.isRevision;
                this.editMode = result.data.editModel;
                this.applyId = result.data.applyId;
                this.isRevisionWaitTime = result.data.isRevisionWaitTime;
            } else {
                this.notify.error('请重试！');
            }
        });
    }
    refresh(): void {
        this.getDataPage(this.pageNumber);
    }
    refreshData() {
        this.pageNumber = 1;
        this.refresh();
    }
    /**
     * 重置
     */
    reset() {
        this.pageNumber = 1;
        this.search = { keyWord: '', categoryId: '0' };
        this.refresh();
    }

    protected fetchDataList(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
        let params: any = {};
        params.SkipCount = request.skipCount;
        params.MaxResultCount = request.maxResultCount;
        params.KeyWord = this.search.keyWord;
        console.log(this.search.keyWord);

        if (this.search.categoryId != '0') {
            params.CategoryId = this.search.categoryId;
        }
        this.workCriterionService.getDocumentListAsync(params)
            .finally(() => {
                finishedCallback();
            })
            .subscribe((result: PagedResultDto) => {
                this.dataList = result.items
                this.totalItems = result.totalCount;
            });
    }

    goPre(id: string) {
        this.router.navigate(['app/criterion/pre-doc', id]);
    }

    goDetail(id: string) {
        this.router.navigate(['app/criterion/self-learning', id]);
    }

    draftApply(): void {
        this.modalHelper
            .open(DraftApplyInfoComponent, {}, 950, {
                nzMask: true,
                nzClosable: false,
                nzMaskClosable: false,
            })
            .subscribe(isSave => {
                if (isSave) {
                    this.isApply = false;
                    this.editMode = false;
                    this.isRevision = false;
                    this.isRevisionWaitTime = false;
                }
            });
    }

    draftDoc() {
        if (this.applyId) {
            this.router.navigate(['app/criterion/draft-doc', this.applyId]);
        }
    }
}
