import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { HomeComponent } from '@app/home/home.component';
import { LayoutDefaultComponent } from '../layout/default/layout-default.component';
import { PositionInfoComponent } from './home/position-info/position-info.component';
import { CreatePositionInfoComponent } from './home/position-info/create-position-info/create-position-info.component';
import { ACLGuard } from '@delon/acl';

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
        // data: { preload: true },
        canActivate: [ACLGuard],
        data: { guard: 'Admin' },
      },
      {
        path: 'basic',
        loadChildren: './basic-data/basic-data.module#BasicDataModule',
        canActivate: [ACLGuard],
        data: { guard: ['QiGuanAdmin', 'Admin', 'StandardAdmin'] },
      },
      {
        path: 'criterion',
        loadChildren: './work-criterion/work-criterion.module#WorkCriterionModule',
        data: { preload: true },
      },
      {
        path: 'supervision',
        loadChildren: './supervision/supervision.module#SupervisionModule',
        canActivate: [AppRouteGuard, ACLGuard],
        data: { guard: ['Admin', 'QiGuanAdmin', 'CountyAdmin'] },
      },
      {
        path: 'advises',
        loadChildren: './advises/advises.module#AdvisesModule',
        data: { preload: true },
      },
      {
        path: 'config',
        loadChildren: './configs/configs.module#ConfigsModule',
        canActivate: [ACLGuard],
        data: { guard: 'Admin' },
      },
      {
        path: 'reports',
        loadChildren: './reports/reports.module#ReportsModule',
        canActivate: [ACLGuard],
        data: { guard: ['Admin', 'QiGuanAdmin'] },
      },
      {
        path: 'logistic',
        loadChildren: './logistics-center/logistics-center.module#LogisticsCenterModule',
        canActivate: [ACLGuard],
        data: { guard: ['Admin', 'LogisticsEmployee'] },
      },
      {
        path: 'position',
        component: PositionInfoComponent,
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
