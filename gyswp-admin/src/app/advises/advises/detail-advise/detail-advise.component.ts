import { Component, OnInit, Injector, Input } from '@angular/core';
import { AdviseService } from 'services';
import { Advise } from 'entities';
import { ModalComponentBase } from '@shared/component-base';

@Component({
  selector: 'app-detail-advise',
  templateUrl: './detail-advise.component.html'
})
export class DetailAdviseComponent extends ModalComponentBase implements OnInit {
  @Input() adviseId;
  advise: Advise = new Advise();
  constructor(injector: Injector, private adviseService: AdviseService
  ) {
    super(injector);
  }
  ngOnInit(): void {
    this.getAdvise();
  }

  //获取详情
  getAdvise(): void {
    this.adviseService.getById(this.adviseId).subscribe((data) => {
      this.advise = data;
    });
  }
}