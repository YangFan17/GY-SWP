import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { HomeComponent } from '@app/home/home.component';
import { LayoutDefaultComponent } from '../layout/default/layout-default.component';
import { PositionInfoComponent } from './home/position-info/position-info.component';
import { CreatePositionInfoComponent } from './home/position-info/create-position-info/create-position-info.component';

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
        path: 'supervision',
        loadChildren: './supervision/supervision.module#SupervisionModule',
        data: { preload: true },
      },
      {
        path: 'advises',
        loadChildren: './advises/advises.module#AdvisesModule',
        data: { preload: true },
      },
      {
        path: 'config',
        loadChildren: './configs/configs.module#ConfigsModule',
        data: { preload: true },
      },
      {
        path: 'reports',
        loadChildren: './reports/reports.module#ReportsModule',
        data: { preload: true },
      },
      {
        path: 'position',
        component: PositionInfoComponent,
        canActivate: [AppRouteGuard],
        data: { preload: true }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
