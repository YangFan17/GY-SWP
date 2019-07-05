import { Component, OnInit, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { AdviseService } from 'services'

@Component({
  selector: 'app-advise',
  templateUrl: './advise.component.html',
  styles: []
})
export class AdviseComponent extends AppComponentBase implements OnInit {
  isTableLoading: boolean = false;
  constructor(injector: Injector, private adviseService: AdviseService) {
    super(injector);
  }

  ngOnInit() {
    this.getAdvises();
  }

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
