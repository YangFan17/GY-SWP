import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { InspectComponent } from './inspect/inspect.component';
import { SuperviseComponent } from './supervise/supervise.component';
import { StandardrevisionComponent } from './standardrevision/standardrevision.component';
import { AdviseComponent } from './advise/advise.component'


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
    // {
    //     path: 'standardrevision',
    //     component: StandardrevisionComponent,
    //     canActivate: [AppRouteGuard],
    // },
    {
        path: 'adviseReport',
        component: AdviseComponent,
        canActivate: [AppRouteGuard],
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class ReportsRoutingModule { }
