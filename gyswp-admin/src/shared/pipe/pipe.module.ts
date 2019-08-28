import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HostUrlPipe, DocUrlPipe } from './host-url.pipe';
import { KeyHighlightPipe } from './key-highlight.pipe';
import { LengthLimitPipe } from './length-limit.pipe';

@NgModule({
    imports: [CommonModule],
    exports: [HostUrlPipe,
        KeyHighlightPipe,
        LengthLimitPipe,
        DocUrlPipe
    ],
    declarations: [
        HostUrlPipe,
        KeyHighlightPipe,
        LengthLimitPipe,
        DocUrlPipe,
    ],
    providers: [
    ],
})
export class PipeModule { }
