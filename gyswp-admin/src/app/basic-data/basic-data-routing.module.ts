import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { OrganizationComponent } from './organization/organization.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DocumentComponent } from './document/document/document.component';
import { DetailComponent } from './document/document/detail/detail.component';
import { ACLGuard } from '@delon/acl';


const routes: Routes = [
    {
        path: 'organization',
        component: OrganizationComponent,
        // canActivate: [AppRouteGuard, ACLGuard],
        // data: { guard: 'CityAdmin' },
        canActivate: [AppRouteGuard, ACLGuard],
        data: { title: '组织架构' },
    },
    // {
    //     path: 'employee',
    //     component: EmployeeComponent,
    //     canActivate: [AppRouteGuard],
    // },
    {
        path: 'document',
        component: DocumentComponent,
        // canActivate: [AppRouteGuard, ACLGuard],
        // data: { guard: 'CityAdmin' },
        canActivate: [AppRouteGuard],
        data: { title: '标准管理' },
    },
    {
        path: 'doc-detail',
        component: DetailComponent,
        canActivate: [AppRouteGuard, ACLGuard],
    },
    {
        path: 'doc-detail/:id',
        component: DetailComponent,
        canActivate: [AppRouteGuard, ACLGuard],
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class BasicDataRoutingModule { }
