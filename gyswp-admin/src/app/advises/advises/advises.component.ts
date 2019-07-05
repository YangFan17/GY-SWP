import { Component, OnInit, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { AdviseService } from 'services';
import { CreateAdviseComponent } from './create-advise/create-advise.component'
import { Router } from '@angular/router';

@Component({
  selector: 'app-advises',
  templateUrl: './advises.component.html',
  styles: []
})
export class AdvisesComponent extends AppComponentBase implements OnInit {
  isTableLoading: boolean = false;
  constructor(injector: Injector, private router: Router, private adviseService: AdviseService) {
    super(injector);
  }

  ngOnInit() {
    this.getAdvises();
  }

  //新增
  create(): void {
    this.modalHelper.open(CreateAdviseComponent, {}, 'md', {
      nzMask: true
    }).subscribe(isSave => {
      if (isSave) {
        this.getAdvises();
      }
    });
  }

  //详情
  goDetail(id: any): void {
    this.router.navigate(['/app/advises/advises-detail', { id: id }]);
  }

  //获取Advise数据
  getAdvises() {
    this.isTableLoading = true;
    let params: any = {};
    params.SkipCount = this.query.skipCount()
    params.MaxResultCount = this.query.pageSize;
    //alert(this.month)
    this.adviseService.getAll(params).subscribe((data) => {
      this.isTableLoading = false;
      this.query.data = data.items;
      this.query.total = data.totalCount;
    });
  }

}
