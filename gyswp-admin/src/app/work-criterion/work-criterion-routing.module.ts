import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CriterionComponent } from './criterion/criterion.component';
import { PreviewDocComponent } from './criterion/preview-doc/preview-doc.component';
import { SelfLearningComponent } from './criterion/self-learning/self-learning.component';


const routes: Routes = [
    {
        path: 'criterion',
        component: CriterionComponent,
        // canActivate: [AppRouteGuard, ACLGuard],
        // data: { guard: 'CityAdmin' },
        canActivate: [AppRouteGuard],
        data: { title: '工作标准' },
    },
    {
        path: 'pre-doc/:id',
        component: PreviewDocComponent,
        // canActivate: [AppRouteGuard, ACLGuard],
        // data: { guard: 'CityAdmin' },
        canActivate: [AppRouteGuard],
        data: { title: '标准预览' },
    },
    {
        path: 'self-learning/:id',
        component: SelfLearningComponent,
        // canActivate: [AppRouteGuard, ACLGuard],
        // data: { guard: 'CityAdmin' },
        canActivate: [AppRouteGuard],
        data: { title: '自查学习' },
    },
    // {
    //     path: 'doc-detail',
    //     component: DetailComponent,
    //     canActivate: [AppRouteGuard],
    // },
    // {
    //     path: 'doc-detail/:id',
    //     component: DetailComponent,
    //     canActivate: [AppRouteGuard],
    // },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class WorkCriterionRoutingModule { }
