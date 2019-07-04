import { Component, OnInit, ViewChild, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/component-base';
import { NzTreeComponent, NzFormatEmitEvent } from 'ng-zorro-antd';
import { BasicDataService, StandardRevisionService } from 'services';

@Component({
  moduleId: module.id,
  selector: 'app-standardrevision',
  templateUrl: './standardrevision.component.html',
  styles: [],
  providers: [StandardRevisionService]
})
export class StandardrevisionComponent extends AppComponentBase implements OnInit {

  @ViewChild('detpTree') detpTree: NzTreeComponent;
  nodes: any[];
  selectedDept: any = { id: '', name: '' };
  month: number;
  date = new Date();
  dataList: any[];
  search = { month: '', deptId: 1 };
  isTableLoading = false;

  constructor(injector: Injector,
    private basicDataService: BasicDataService, private standardRevisionService: StandardRevisionService) {
    super(injector);
  }

  ngOnInit() {
    this.search.month = this.date.getFullYear() + '-' + (this.date.getMonth() + 1) + '-1';
    this.getTrees();
  }

  getTrees() {
    this.basicDataService.getDeptDocNzTreeNodes('标准制修订部门').subscribe((data) => {
      this.nodes = data;
      if (data.length > 0) {
        var selectedNode = data[0].children[0];
        if (selectedNode && selectedNode.isSelected) {
          this.selectedDept = { id: selectedNode.key, name: selectedNode.title };
          this.getStandardRevisionData();
        }
      }
    });
  }

  // 选中节点
  activeNode(data: NzFormatEmitEvent): void {
    if (data.node.key == '0' || data.node.key == '-1') {
      this.selectedDept = { id: '1', name: '' };
    } else {
      this.selectedDept = { id: data.node.key, name: data.node.title };
    }
    this.getStandardRevisionData();
  }

  getStandardRevisionData() {
    this.search.deptId = this.selectedDept.id;
    this.month = this.date.getMonth() + 1;
    this.isTableLoading = true;
    this.isTableLoading = true;
    //alert(this.month)
    this.standardRevisionService.GetSearchStandardRevisions(this.search).subscribe((data) => {
      this.dataList = data;
      this.isTableLoading = false;
    });
  }

  refreshData() {
    this.getStandardRevisionData();
  }

  onChange(result: Date): void {
    this.search.month = this.date.getFullYear() + '-' + (this.date.getMonth() + 1) + '-1';
    //console.log('onChange: ', result);
  }
}
