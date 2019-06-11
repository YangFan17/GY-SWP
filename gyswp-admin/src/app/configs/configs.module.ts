import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { LayoutModule } from "@layout/layout.module";
import { SharedModule } from "@shared/shared.module";
import { DataConfigComponent } from "./data-config/data-config.component";
import { ConfigsRoutingModule } from "./configs-routing.module";

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
    ],
    entryComponents: [
        DataConfigComponent,
    ],
    providers: [],
})
export class ConfigsModule {

}
