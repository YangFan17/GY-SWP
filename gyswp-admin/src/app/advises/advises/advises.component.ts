import { Component, Injector } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { Router } from '@angular/router';
import { AdviseService } from 'services';

@Component({
  selector: 'app-advises',
  templateUrl: './advises.component.html',
  styles: []
})
export class AdvisesComponent extends PagedListingComponentBase<any>{
  isTableLoading: boolean = false;
  constructor(
    injector: Injector
    , private router: Router
    , private adviseService: AdviseService
  ) {
    super(injector);
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
    this.refresh();
  }

  protected fetchDataList(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    let params: any = {};
    params.SkipCount = request.skipCount;
    params.MaxResultCount = request.maxResultCount;
    this.adviseService.getPublicityAdviceList(params)
      .finally(() => {
        finishedCallback();
      })
      .subscribe((result: PagedResultDto) => {
        this.dataList = result.items
        this.totalItems = result.totalCount;
      });
  }

  //详情
  goDetail(id: any): void {
    this.router.navigate(['/app/advises/advises-detail', { id: id }]);
  }

  myAdvice(): void {
    this.router.navigate(['/app/advises/my-advice']);
  }
}
