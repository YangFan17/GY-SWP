import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/component-base/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { _HttpClient } from '@delon/theme';
import { Router } from '@angular/router';
import { WorkCriterionService } from 'services';

@Component({
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.less'],
  animations: [appModuleAnimation()],
  providers: [WorkCriterionService]
})
export class HomeComponent extends AppComponentBase implements OnInit {
  position: string;
  listOfMapData = [];
  constructor(
    injector: Injector
    , private router: Router
    , private workCriterionService: WorkCriterionService

  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.getPosition();
  }

  getPosition() {
    this.workCriterionService.getCurrentPositionAsync().subscribe((result) => {
      this.position = result;
      this.getClauseList();
    });
  }

  getClauseList() {
    this.workCriterionService.getPositionTreeByIdAsync().subscribe((result) => {
      this.listOfMapData = result
      let i = 1;
      this.listOfMapData.forEach(v => {
        v.duties = i + '、工作职责：' + v.duties;
        i++;
      });
    });
  }


  goLearn(id: string) {
    this.router.navigate(['app/criterion/self-learning', id]);
  }

  goEdit() {
    this.router.navigate(['app/criterion/pre-doc']);
  }
}