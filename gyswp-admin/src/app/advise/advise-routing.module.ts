import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdviseComponent } from './advise/advise.component';
import { AppRouteGuard } from '@shared/auth';

const routes: Routes = [
  {
    path: 'advise',
    component: AdviseComponent,
    canActivate: [AppRouteGuard],
  }
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdviseRoutingModule { }
