import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    moduleId: module.id,
    selector: 'preview-doc',
    templateUrl: 'preview-doc.component.html',
    styleUrls: ['preview-doc.component.less']
})
export class PreviewDocComponent {
    title: string = '标准预览'
    pdfSrc: string = 'http://gy.hechuangcd.com/docfiles/04c69728-7543-4aa8-917d-d8fc39280efd.pdf';
    constructor(private router: Router
    ) {
    }

    return() {
        this.router.navigate(['app/criterion/criterion']);
    }
}
