import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CriterionExamineComponent } from './criterion-examine/criterion-examine.component';
import { DeptExamineRecordComponent } from './criterion-examine/dept-examine-record/dept-examine-record.component';
import { RecordDetailComponent } from './criterion-examine/dept-examine-record/record-detail/record-detail.component';
import { EmpExamineRecordComponent } from './criterion-examine/emp-list/emp-examine-record/emp-examine-record.component';


const routes: Routes = [
    {
        path: 'supervision',
        component: CriterionExamineComponent,
        // canActivate: [AppRouteGuard, ACLGuard],
        // data: { guard: 'CityAdmin' },
        canActivate: [AppRouteGuard],
        data: { title: '标准考核' },
    },
    {
        path: 'record/:id/:dept',
        component: DeptExamineRecordComponent,
        // canActivate: [AppRouteGuard, ACLGuard],
        // data: { guard: 'CityAdmin' },
        canActivate: [AppRouteGuard]
    },
    {
        path: 'record-detail/:id',
        component: RecordDetailComponent,
        // canActivate: [AppRouteGuard, ACLGuard],
        // data: { guard: 'CityAdmin' },
        canActivate: [AppRouteGuard]
    },
    {
        path: 'emp-record/:id',
        component: EmpExamineRecordComponent,
        canActivate: [AppRouteGuard]
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class SupervisionRoutingModule { }
