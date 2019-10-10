import { NgModule } from '@angular/core';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { Routes, RouterModule } from '@angular/router';
import { AdvisesComponent } from './advises/advises.component';
import { MyAdviceComponent } from './advises/my-advice/my-advice.component';

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
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdvisesRoutingModule { }
