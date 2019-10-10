import { NgModule } from '@angular/core';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { Routes, RouterModule } from '@angular/router';
import { AdvisesComponent } from './advises/advises.component';
import { MyAdviceComponent } from './advises/my-advice/my-advice.component';
import { PublicityManagementComponent } from './advises/publicity-management/publicity-management.component';
import { ACLGuard } from '@delon/acl';

const routes: Routes = [
  {
    path: 'advises',
    component: AdvisesComponent,
    canActivate: [AppRouteGuard],
  },
  {
    path: 'my-advice',
    component: MyAdviceComponent,
    canActivate: [AppRouteGuard],
  },
  {
    path: 'advice-management',
    component: PublicityManagementComponent,
    canActivate: [AppRouteGuard, ACLGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdvisesRoutingModule { }
