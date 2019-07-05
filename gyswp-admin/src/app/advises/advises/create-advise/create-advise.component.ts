import { Component, OnInit, Injector } from '@angular/core';
import { ModalComponentBase } from '@shared/component-base';
import { Advise } from 'entities';
import { AdviseService } from 'services'
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-create-advise',
  templateUrl: './create-advise.component.html',
  styles: []
})
export class CreateAdviseComponent extends ModalComponentBase implements OnInit {
  form: FormGroup;
  advise: Advise = new Advise();
  constructor(injector: Injector, private adviseService: AdviseService
    , private fb: FormBuilder) { super(injector); }

  ngOnInit() {
    this.form = this.fb.group({
      adviseName: [null, Validators.compose([Validators.required, Validators.maxLength(50)])],
      currentSituation: [null, Validators.compose([Validators.required, Validators.maxLength(1000)])],
      solution: [null, Validators.compose([Validators.required, Validators.maxLength(1000)])]
    });
  }

  //保存
  save() {
    console.log(this.advise)
    this.adviseService.createOrUpdate(this.advise).finally(() => {
      this.saving = false;
    }).subscribe(() => {
      this.notify.success('保存成功！');
      this.success();
    });
  }

}
