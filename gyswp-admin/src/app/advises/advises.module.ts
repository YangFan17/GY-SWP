import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { LayoutModule } from "@layout/layout.module";
import { SharedModule } from "@shared/shared.module";

import { AdvisesRoutingModule } from './advises-routing.module';
import { AdvisesComponent } from './advises/advises.component';
import { AdviseService } from 'services';
import { DetailAdviseComponent } from './advises/detail-advise/detail-advise.component'
import { MyAdviceComponent } from './advises/my-advice/my-advice.component';
import { PublicityManagementComponent } from './advises/publicity-management/publicity-management.component';

@NgModule({
  imports: [
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    LayoutModule,
    SharedModule,
    CommonModule,
    AdvisesRoutingModule
  ],
  declarations: [
    AdvisesComponent,
    DetailAdviseComponent,
    MyAdviceComponent,
    PublicityManagementComponent
  ],
  entryComponents: [
    MyAdviceComponent,
    PublicityManagementComponent
  ],
  providers: [AdviseService]
})
export class AdvisesModule { }
