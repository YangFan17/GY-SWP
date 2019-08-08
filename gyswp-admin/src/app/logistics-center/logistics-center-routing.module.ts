import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { Routes, RouterModule } from '@angular/router';
import { InStorageComponent } from './in-storage/in-storage.component';

const routes: Routes = [
    {
        path: 'in-storage',
        component: InStorageComponent,
        canActivate: [AppRouteGuard],
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class LogisticsCenterRoutingModule { }
