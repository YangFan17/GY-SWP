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
import { RevisionDocComponent } from './standardrevision/revision-doc/revision-doc.component';
import { RevisionDraftComponent } from './standardrevision/revision-draft/revision-draft.component';
import { RevisionDetailComponent } from './standardrevision/revision-draft/revision-detail/revision-detail.component';
import { ActiveCategoryComponent } from './standardrevision/active-category/active-category.component';




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
    {
        path: 'revision-doc/:deptId/:type/:date',
        component: RevisionDocComponent,
        canActivate: [AppRouteGuard],
    },
    {
        path: 'revision-draft/:id/:deptId/:type/:date',
        component: RevisionDraftComponent,
        canActivate: [AppRouteGuard]
    },
    {
        path: 'acitve-statistics',
        component: ActiveCategoryComponent,
        canActivate: [AppRouteGuard]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class ReportsRoutingModule { }
