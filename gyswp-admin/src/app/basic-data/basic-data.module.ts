// Angular Imports

// This Module's Components
import { OrganizationComponent } from './organization/organization.component';
import { LayoutModule } from '@layout/layout.module';
import { SharedModule } from '@shared/shared.module';
import { EmployeeComponent } from './employee/employee.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BasicDataRoutingModule } from './basic-data-routing.module';
import { BasicDataService } from 'services';
import { DocumentComponent } from './document/document/document.component';
import { QrCodeCategoryComponent } from './document/document/qr-code-category/qr-code-category.component';
import { EditCategoryComponent } from './document/document/edit-category/edit-category.component';
import { CreateCategoryComponent } from './document/document/create-category/create-category.component';
import { CategoryComponent } from './document/document/category/category.component';
import { ListComponent } from './document/document/list/list.component';
import { DetailComponent } from './document/document/detail/detail.component';
import { ClauseComponent } from './document/clause/clause.component';
import { ClauseDetailComponent } from './document/clause/clause-detail/clause-detail.component';
import { DeptUserComponent } from './document/document/detail/dept-user/dept-user.component';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        LayoutModule,
        SharedModule,
        BasicDataRoutingModule,
    ],
    declarations: [
        OrganizationComponent,
        EmployeeComponent,
        DocumentComponent,
        QrCodeCategoryComponent,
        EditCategoryComponent,
        CreateCategoryComponent,
        CategoryComponent,
        ListComponent,
        DetailComponent,
        ClauseComponent,
        ClauseDetailComponent,
        DeptUserComponent
    ],
    entryComponents: [
        OrganizationComponent,
        EmployeeComponent,
        DocumentComponent,
        QrCodeCategoryComponent,
        EditCategoryComponent,
        CreateCategoryComponent,
        CategoryComponent,
        ListComponent,
        DetailComponent,
        ClauseComponent,
        ClauseDetailComponent,
        DeptUserComponent
    ],
    providers: [BasicDataService]
})
export class BasicDataModule {
}