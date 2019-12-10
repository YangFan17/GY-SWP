import { Component, OnInit, ViewChild, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/component-base';
import { NzTreeComponent, NzFormatEmitEvent } from 'ng-zorro-antd';
import { StandardRevisionService } from 'services';
import { Router } from '@angular/router';

@Component({
  moduleId: module.id,
  selector: 'app-standardrevision',
  templateUrl: './standardrevision.component.html',
  providers: [StandardRevisionService]
})
export class StandardrevisionComponent extends AppComponentBase implements OnInit {
  @ViewChild('detpTree') detpTree: NzTreeComponent;
  nodes: any[];
  selectedDept: any = { id: '', name: '' };
  // month: number;
  year: number;
  date = new Date();
  dataList: any[];
  // search = { month: '', year: '', deptId: 1 };
  search = { year: 2019, deptId: 1 };
  isTableLoading = false;

  constructor(
    injector: Injector
    , private standardRevisionService: StandardRevisionService
    , private router: Router
  ) {
    super(injector);
  }

  ngOnInit() {
    // this.search.month = this.date.getFullYear() + '-' + (this.date.getMonth() + 1) + '-1';
    this.search.year = this.date.getFullYear();
    this.getTrees();
  }

  getTrees() {
    this.standardRevisionService.getDeptDocNzTreeNodes('标准制修订部门').subscribe((data) => {
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

  goTotalCurrent(type: number) {
    // var date = this.dateFormat(this.date);
    var date = this.date.getFullYear();
    if (type == 1 && this.selectedDept.id == '1') {
      this.router.navigate(['app/reports/acitve-statistics']);
    } else {
      this.router.navigate(['app/reports/revision-doc', this.search.deptId, type, date]);
    }
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
    // this.month = this.date.getMonth() + 1;
    this.year = this.date.getFullYear();
    this.isTableLoading = true;
    //alert(this.month)
    this.standardRevisionService.getSearchStandardRevisions(this.search).subscribe((data) => {
      this.dataList = data;
      this.isTableLoading = false;
    });
  }

  refreshData() {
    this.getStandardRevisionData();
  }

  reset() {
    this.date = new Date();
    // this.search.month = this.date.getFullYear() + '-' + (this.date.getMonth() + 1) + '-1';
    this.search.year = this.date.getFullYear();
    this.refreshData();
  }

  onChange(result: Date): void {
    // this.search.month = this.date.getFullYear() + '-' + (this.date.getMonth() + 1) + '-1';
    this.search.year = this.date.getFullYear();
  }
}
