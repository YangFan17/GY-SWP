import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { InspectComponent } from './inspect/inspect.component';
import { SuperviseComponent } from './supervise/supervise.component';
import { StandardrevisionComponent } from './standardrevision/standardrevision.component';
import { AdviseComponent } from './advise/advise.component'
import { IndicatorSuperviseComponent } from './indicator-supervise/indicator-supervise.component';
import { StandardConfirmComponent } from './standard-confirm/standard-confirm.component';
import { ConfirmDetailComponent } from './standard-confirm/confirm-detail/confirm-detail.component';


const routes: Routes = [
    {
        path: 'inspect',
        component: InspectComponent,
        canActivate: [AppRouteGuard],
    },
    {
        path: 'supervise',
        component: SuperviseComponent,
    },
    {
        path: 'standardrevision',
        component: StandardrevisionComponent,
        canActivate: [AppRouteGuard],
    },
    {
        path: 'indicator-supervise',
        component: IndicatorSuperviseComponent,
        canActivate: [AppRouteGuard],
    },
    {
        path: 'standard-confirm',
        component: StandardConfirmComponent,
        canActivate: [AppRouteGuard],
    },
    {
        path: 'adviseReport',
        component: AdviseComponent,
        canActivate: [AppRouteGuard],
    },
    {
        path: 'confirm-detail/:id/:status',
        component: ConfirmDetailComponent,
        canActivate: [AppRouteGuard],
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class ReportsRoutingModule { }
