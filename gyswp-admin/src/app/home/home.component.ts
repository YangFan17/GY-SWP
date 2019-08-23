import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/component-base/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { _HttpClient } from '@delon/theme';
import { Router } from '@angular/router';
import { HomeService } from 'services';
import { ConfirmLearningComponent } from '@app/work-criterion/criterion/self-learning/confirm-learning/confirm-learning.component';

@Component({
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.less'],
  animations: [appModuleAnimation()],
  providers: [HomeService]
})
export class HomeComponent extends AppComponentBase implements OnInit {
  position: string;
  listOfMapData = [];
  constructor(
    injector: Injector
    , private router: Router
    , private homeService: HomeService
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.getPosition();
  }

  getPosition() {
    this.homeService.getCurrentPositionAsync().subscribe((result) => {
      this.position = result;
      this.getClauseList();
    });
  }

  getClauseList() {
    this.homeService.getPositionTreeByIdAsync().subscribe((result) => {
      this.listOfMapData = result
      let i = 1;
      this.listOfMapData.forEach(v => {
        v.duties = i + '、工作职责：' + v.duties;
        // v.children.forEach(item => {
        //   if (item.mainPoint.indexOf('\r\n') != -1)
        //     item.mainPoint = item.mainPoint.replace(/(\r\n)|(\n)/g, '<br/>');
        // });
        i++;
      });
    });
  }


  goDoc(id: string) {
    this.router.navigate(['app/criterion/self-learning', id]);
  }

  goLearn(docId: string, docName: string, clauseId: string) {
    this.modalHelper
      .open(ConfirmLearningComponent, { docId: docId, docName: docName, id: clauseId }, 950, {
        nzMask: true,
        nzClosable: false,
        nzMaskClosable: false,
      })
      .subscribe(isSave => {
        if (isSave) {
          this.getClauseList();
        }
      });
  }

  goEdit() {
    this.router.navigate(['app/position']);
  }
}