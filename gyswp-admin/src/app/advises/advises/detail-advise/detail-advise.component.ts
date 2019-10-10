import { Component, OnInit, Injector, Input } from '@angular/core';
import { AdviseService } from 'services';
import { Advise } from 'entities';
import { ActivatedRoute } from '@angular/router';
import { FormGroup } from '@angular/forms';
import { ModalComponentBase } from '@shared/component-base';

@Component({
  selector: 'app-detail-advise',
  templateUrl: './detail-advise.component.html',
  styles: []
})
export class DetailAdviseComponent extends ModalComponentBase implements OnInit {
  @Input() adviseId;
  loading: boolean = false;
  form: FormGroup;
  id: '';
  advise: Advise = new Advise();
  constructor(injector: Injector, private adviseService: AdviseService
  ) {
    super(injector);
  }
  ngOnInit(): void {
    this.getAdvise();
    this.loading = false;
  }

  //获取详情
  getAdvise(): void {
    console.log("详情api")
    this.loading = true;
    this.adviseService.getById(this.id).subscribe((data) => {
      this.loading = true;
      this.advise = data;
    });
  }
  close() {
    this.success(true);
  }
}