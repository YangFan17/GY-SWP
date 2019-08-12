import { Component, OnInit, Injector, Input } from '@angular/core';
import { ModalComponentBase } from '@shared/component-base';
import { SystemData } from 'entities';
import { DataConfigServiceProxy } from 'services'
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-modify-config',
  templateUrl: './modify-config.component.html'
})
export class ModifyConfigComponent extends ModalComponentBase implements OnInit {
  @Input() id: number;
  @Input() modelId: number;
  systemData: SystemData = new SystemData();
  title: string;
  form: FormGroup;
  configType = [
    { value: 1, text: '钉钉配置', selected: true },
    { value: 2, text: '企业标准库管理系统', selected: false },
    { value: 3, text: '设备管理', selected: false }
  ]
  constructor(injector: Injector, private dataConfigServiceProxy: DataConfigServiceProxy
    , private fb: FormBuilder) { super(injector); }

  ngOnInit() {
    this.form = this.fb.group({
      type: [null],
      code: [null, Validators.compose([Validators.required])],
      desc: [null, Validators.compose([Validators.maxLength(500)])],
      remark: [null, Validators.compose([Validators.maxLength(500)])],
      seq: [null, Validators.compose([Validators.pattern('^[0-9]*$')])],
    });
    if (!this.id) {
      this.title = "创建基础信息";
    }
    else {
      this.title = "编辑基础信息";
      this.getSystemData();
    }
  }

  //获取编辑数据
  getSystemData() {
    this.dataConfigServiceProxy.getById(this.id).subscribe((result) => {
      this.systemData = result;
    });
  }

  //保存
  save() {
    this.systemData.modelId = this.modelId;
    if (this.systemData.modelId == 3 && !this.systemData.type)
      return this.notify.warn("请选择配置类型");
    if (!this.systemData.type)
      this.systemData.type = 0;
    this.dataConfigServiceProxy.createOrUpdate(this.systemData).finally(() => {
      this.saving = false;
    }).subscribe(() => {
      this.notify.success('保存成功！');
      this.success();
    });
  }

}
