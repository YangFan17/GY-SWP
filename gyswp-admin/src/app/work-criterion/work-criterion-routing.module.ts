import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CriterionComponent } from './criterion/criterion.component';
import { PreviewDocComponent } from './criterion/preview-doc/preview-doc.component';
import { SelfLearningComponent } from './criterion/self-learning/self-learning.component';
import { DraftDocComponent } from './criterion/draft-doc/draft-doc/draft-doc.component';
import { MyExamineComponent } from './my-examine/my-examine.component';
import { ExamineDetailComponent } from './my-examine/examine-detail/examine-detail.component';


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
    {
        path: 'draft-doc/:id',
        component: DraftDocComponent,
        canActivate: [AppRouteGuard],
        data: { title: '制定标准' },
    },
    {
        path: 'my-examine',
        component: MyExamineComponent,
        canActivate: [AppRouteGuard],
        data: { title: '标准考核' },
    },
    {
        path: 'examine-detail/:id',
        component: ExamineDetailComponent,
        canActivate: [AppRouteGuard],
    },

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class WorkCriterionRoutingModule { }
