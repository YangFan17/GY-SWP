import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { HomeComponent } from '@app/home/home.component';
import { LayoutDefaultComponent } from '../layout/default/layout-default.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutDefaultComponent,
    canActivate: [AppRouteGuard],
    canActivateChild: [AppRouteGuard],
    children: [
      { path: '', redirectTo: 'home', pathMatch: 'full' },
      {
        path: 'home',
        component: HomeComponent,
        canActivate: [AppRouteGuard],
        data: { preload: true }
      },
      {
        path: 'system',
        loadChildren: './system/system.module#SystemModule',
        data: { preload: true },
      },
      {
        path: 'basic',
        loadChildren: './basic-data/basic-data.module#BasicDataModule',
        data: { preload: true },
      },
      {
        path: 'criterion',
        loadChildren: './work-criterion/work-criterion.module#WorkCriterionModule',
        data: { preload: true },
      },
      {
        path: 'config',
        loadChildren: './configs/configs.module#ConfigsModule',
        data: { preload: true },
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
