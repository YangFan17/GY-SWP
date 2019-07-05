import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { Routes, RouterModule } from '@angular/router';
import { AdvisesComponent } from './advises/advises.component';
import { DetailAdviseComponent } from './advises/detail-advise/detail-advise.component'

const routes: Routes = [{
  path: 'advises',
  component: AdvisesComponent,
  canActivate: [AppRouteGuard],
}, {
  path: 'advises-detail',
  component: DetailAdviseComponent,
  canActivate: [AppRouteGuard],
  // data: { title: "项目详情" }
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdvisesRoutingModule { }
