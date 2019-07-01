import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';

const routes: Routes = [
    /*{
        path: 'roles',
        component: RolesComponent,
        canActivate: [AppRouteGuard],
    },
    {
        path: 'users',
        component: UsersComponent,
        canActivate: [AppRouteGuard],
    },*/
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class ReportsRoutingModule { }
