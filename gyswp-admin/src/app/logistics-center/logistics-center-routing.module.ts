import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { Routes, RouterModule } from '@angular/router';
import { InStorageComponent } from './in-storage/in-storage.component';
import { OutStorageComponent } from './out-storage/out-storage.component';
import { OutStorageClassifyComponent } from './out-storage-classify/out-storage-classify.component';
import { StorageCustodyComponent } from './storage-custody/storage-custody.component';

const routes: Routes = [
    {
        path: 'in-storage',
        component: InStorageComponent,
        canActivate: [AppRouteGuard],
    },
    {
        path: 'storage-custody',
        component: StorageCustodyComponent,
        canActivate: [AppRouteGuard],
    },
    {
        path: 'out-storage-classify',
        component: OutStorageClassifyComponent,
        canActivate: [AppRouteGuard],
    },
    {
        path: 'out-storage',
        component: OutStorageComponent,
        canActivate: [AppRouteGuard],
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class LogisticsCenterRoutingModule { }
