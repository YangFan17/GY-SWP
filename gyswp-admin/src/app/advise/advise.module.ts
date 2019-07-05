import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { LayoutModule } from "@layout/layout.module";
import { SharedModule } from "@shared/shared.module";

import { AdviseRoutingModule } from './advise-routing.module';
import { AdviseComponent } from './advise/advise.component';
import { AdviseService } from 'services'

@NgModule({
  imports: [
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    LayoutModule,
    SharedModule,
    CommonModule,
    AdviseRoutingModule
  ],
  declarations: [AdviseComponent],
  entryComponents: [
  ],
  providers: [AdviseService],
})
export class AdviseModule { }
