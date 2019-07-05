import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { LayoutModule } from "@layout/layout.module";
import { SharedModule } from "@shared/shared.module";
import { DataConfigComponent } from "./data-config/data-config.component";
import { ConfigsRoutingModule } from "./configs-routing.module";
import { ModifyConfigComponent } from './data-config/modify-config/modify-config.component';
import { DataConfigServiceProxy } from 'services'

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        LayoutModule,
        SharedModule,
        ConfigsRoutingModule,
    ],
    declarations: [
        DataConfigComponent,
        ModifyConfigComponent,
    ],
    entryComponents: [
        ModifyConfigComponent,
    ],
    providers: [DataConfigServiceProxy],
})
export class ConfigsModule {

}
