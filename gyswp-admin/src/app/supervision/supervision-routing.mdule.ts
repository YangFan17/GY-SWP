import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CriterionExamineComponent } from './criterion-examine/criterion-examine.component';


const routes: Routes = [
    {
        path: 'supervision',
        component: CriterionExamineComponent,
        // canActivate: [AppRouteGuard, ACLGuard],
        // data: { guard: 'CityAdmin' },
        canActivate: [AppRouteGuard],
        data: { title: '标准考核' },
    },
    // {
    //     path: 'pre-doc/:id',
    //     component: PreviewDocComponent,
    //     // canActivate: [AppRouteGuard, ACLGuard],
    //     // data: { guard: 'CityAdmin' },
    //     canActivate: [AppRouteGuard],
    //     data: { title: '标准预览' },
    // },
    // {
    //     path: 'self-learning/:id',
    //     component: SelfLearningComponent,
    //     // canActivate: [AppRouteGuard, ACLGuard],
    //     // data: { guard: 'CityAdmin' },
    //     canActivate: [AppRouteGuard],
    //     data: { title: '自查学习' },
    // },
    // {
    //     path: 'draft-doc/:id',
    //     component: DraftDocComponent,
    //     canActivate: [AppRouteGuard],
    //     data: { title: '制定标准' },
    // },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class SupervisionRoutingModule { }
