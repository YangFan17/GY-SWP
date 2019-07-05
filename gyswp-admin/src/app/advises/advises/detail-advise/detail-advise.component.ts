import { Component, OnInit, Injector, Input } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { AdviseService } from 'services';
import { Advise } from 'entities';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-detail-advise',
  templateUrl: './detail-advise.component.html',
  styles: []
})
export class DetailAdviseComponent extends AppComponentBase implements OnInit {
  @Input() adviseId;
  loading: boolean = false;
  advise: Advise = new Advise();
  constructor(injector: Injector, private adviseService: AdviseService
    , private actRouter: ActivatedRoute) {
    super(injector);
    this.adviseId = this.actRouter.snapshot.params['id'];
  }

  ngOnInit() {
    this.getAdvise();
  }

  //获取详情
  getAdvise(): void {
    this.loading = true;
    this.adviseService.getById(this.adviseId).subscribe((data) => {
      this.loading = false;
      this.advise = data;
    });
  }

  //返回
  return(): void {
    history.back();
  }

}
