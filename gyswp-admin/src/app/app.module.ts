import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppRoutingModule } from '@app/app-routing.module';
import { LayoutModule } from '@layout/layout.module';
import { HomeComponent } from '@app/home/home.component';
import { SharedModule } from '@shared/shared.module';
import { HttpClientModule } from '@angular/common/http';
import { G2GroupBarModule } from '../../packages/group-bar';
import { PositionInfoComponent } from './home/position-info/position-info.component';
import { CreatePositionInfoComponent } from './home/position-info/create-position-info/create-position-info.component';
import { AddDocumentComponent } from './home/position-info/add-document/add-document.component';
import { EmpDocListComponent } from './home/position-info/emp-doc-list/emp-doc-list.component';
import { ConfirmLearningComponent } from './work-criterion/criterion/self-learning/confirm-learning/confirm-learning.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule,
    LayoutModule,
    SharedModule,
    G2GroupBarModule,
  ],
  declarations: [
    HomeComponent,
    PositionInfoComponent,
    CreatePositionInfoComponent,
    AddDocumentComponent,
    EmpDocListComponent,
    ConfirmLearningComponent
  ],
  entryComponents: [
    CreatePositionInfoComponent,
    AddDocumentComponent,
    EmpDocListComponent,
    ConfirmLearningComponent
  ],
  providers: [],
})
export class AppModule { }
