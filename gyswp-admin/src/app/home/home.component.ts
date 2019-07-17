import { Component, Injector, AfterViewInit, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/component-base/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { _HttpClient } from '@delon/theme';
import { NzMessageService } from 'ng-zorro-antd';
import { ACLService } from '@delon/acl';
import { NzNotificationService } from 'ng-zorro-antd';
import { Router } from '@angular/router';
import { WorkCriterionService } from 'services';

@Component({
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.less'],
  animations: [appModuleAnimation()],
  providers: [WorkCriterionService]
})
export class HomeComponent extends AppComponentBase implements OnInit {

  // widthConfig = ['50px', '100px', '100px', '100px', '100px', '100px'];
  widthConfig = ['100px', '100px', '100px', '300px'];
  // widthConfig2 = ['50px', '500px'];
  listOfDisplayData: any[] = [];
  listOfData: any[] = [];
  info: any = { a: '杨帆', b: '采购管理员' };
  // info2: any = { a: '采购管理员', b: '管理类', c: '企管科科长', d: '', f: '市局（公司）各部门、县区局（分公司）综合办' };
  listOfMapData = [];
  docId: string = '81165277-8746-436b-b08b-08d6e3fafef2';
  mapOfExpandedData: { [id: string]: TreeNodeInterface[] } = {};
  constructor(
    injector: Injector
    , private http: _HttpClient
    , public msg: NzMessageService
    , private router: Router
    , private workCriterionService: WorkCriterionService

  ) {
    super(injector);
  }
  getClauseList() {
    if (this.docId) {
      let params: any = {};
      params.DocumentId = this.docId;
      this.workCriterionService.getClauseListAsync(params).subscribe((result) => {
        this.listOfMapData = result
        this.listOfMapData.forEach(item => {
          this.mapOfExpandedData[item.id] = this.convertTreeToList(item);
        });
      });
    }
  }
  ngOnInit(): void {
    this.getClauseList();

    this.listOfData.push({
      duties: '采购工作管理',
      docName: '采购管理办法',
      docNo: 'Q/GYYC 206008－2016',
      docContent: `a)组织编制《采购目录》\r\n
      b)组织编制年度采购计划及调整采购计划\r\n
    c）审核项目是否已完整履行申报、审核、计划和预算、审批等手续；\r\n
    d) 组织开展预审会对计划变更项目进行审议；\r\n
    e）抽选招标代理机构；\r\n
    f）组织审定采购文件；\r\n
g）核对发布标书与审核后的标书内容是否有实质性的差别；`
    });
    this.listOfData.push({
      duties: '采购项目档案验收、统一保管',
      docName: '采购项目档案管理办法',
      docNo: 'Q/GYYC 206012－2016',
      docContent: `a）对已实施完成组卷的项目档案进行验收；\r\n
      b)对验收合格的采购项目档案进行登记，建立采购项目档案台账。`
    });
    this.listOfData.push({
      duties: '负责建立和动态管理招标代理机构库，抽取招标代理机构',
      docName: '采购招标代理机构聘用管理办法',
      docNo: 'Q/GYYC 206009－2016',
      docContent: `a)聘用不少于3家的招标代理机构建立招标代理机构库；\r\n
      b)在监督办的监督下在招标代理机构库中随机抽取1家招标代理机构；\r\n
      c) 招标结束后，组织采购实施部门对招标代理机构当次招标代理活动进行评价；\r\n
      d) 对招标代理机构评价结果进行汇总整理，
      将评价结果与招标代理机构进行反馈，并运用评价结果。
      ；`
    });
    this.listOfData.push({
      duties: '',
      docName: '供应商管理办法',
      docNo: 'Q/GYYC 206011－2016',
      docContent: ``
    });
    this.listOfData.push({
      duties: '办负责分类建立内部评标成员库，负责对其实行日常和动态管理',
      docName: '评标成员和评标成员库管理办法',
      docNo: 'Q/GYYC 206010－2016',
      docContent: `a)组织评标成员库的建立；\r\n
      b)评标成员库进行在发布，同时录入物资采购信息系统；\r\n
      c)在监督部门的监督下随时抽取评标人员；\r\n
      d)根据评标成员岗位变动、身体健康及工作态度等，及时对成员库进行调整和补充。
      `
    });
    this.listOfDisplayData = [...this.listOfData];
  }

  collapse(array: TreeNodeInterface[], data: TreeNodeInterface, $event: boolean): void {
    if ($event === false) {
      if (data.children.length > 0) {
        data.children.forEach(d => {
          const target = array.find(a => a.id === d.id)!;
          target.expand = false;
          this.collapse(array, target, false);
        });
      } else {
        return;
      }
    }
  }

  convertTreeToList(root: object): TreeNodeInterface[] {
    const stack: any[] = [];
    const array: any[] = [];
    const hashMap = {};
    stack.push({ ...root, level: 0, expand: true });

    while (stack.length !== 0) {
      const node = stack.pop();
      this.visitNode(node, hashMap, array);
      if (node.children.length > 0) {
        for (let i = node.children.length - 1; i >= 0; i--) {
          stack.push({ ...node.children[i], level: node.level + 1, expand: true, parent: node });
        }
      }
    }
    return array;
  }

  visitNode(node: TreeNodeInterface, hashMap: { [id: string]: any }, array: TreeNodeInterface[]): void {
    if (!hashMap[node.id]) {
      hashMap[node.id] = true;
      array.push(node);
    }
  }


  goPre(id: string) {
    this.router.navigate(['app/criterion/pre-doc', id]);
  }

  goEdit() {
    this.router.navigate(['app/position']);
  }
}
export interface TreeNodeInterface {
  id: string;
  parentId: string;
  content: string;
  clauseNo: string;
  title: string;
  expand: boolean;
  level: number;
  checked: boolean;
  bllId: string;
  isNew: boolean;
  lastModificationTime: Date;
  children?: TreeNodeInterface[];
}
