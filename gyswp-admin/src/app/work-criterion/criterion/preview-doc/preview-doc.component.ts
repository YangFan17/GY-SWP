import { Component, OnInit, Injector } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AppComponentBase } from '@shared/component-base';
import { WorkCriterionService } from 'services';

@Component({
    moduleId: module.id,
    selector: 'preview-doc',
    templateUrl: 'preview-doc.component.html',
    styleUrls: ['preview-doc.component.less']
})
export class PreviewDocComponent extends AppComponentBase implements OnInit {
    id: string;
    title: string = '标准预览'
    // pdfSrc: string = 'http://gy.hechuangcd.com/docfiles/04c69728-7543-4aa8-917d-d8fc39280efd.pdf';
    pdfSrc: string = '';
    constructor(injector: Injector
        , private actRouter: ActivatedRoute
        , private router: Router
        , private workCriterionService: WorkCriterionService
    ) {
        super(injector);
        this.id = this.actRouter.snapshot.params['id'];
    }
    ngOnInit(): void {
        if (this.id) {
            this.getById();
        }
    }

    getById() {
        this.workCriterionService.getPreviewDocByIdAsync(this.id).subscribe(res => {
            if (res.code == 0) {
                this.pdfSrc = res.data;
            } else {

            }
        });
    }

    return() {
        this.router.navigate(['app/criterion/criterion']);
    }
}