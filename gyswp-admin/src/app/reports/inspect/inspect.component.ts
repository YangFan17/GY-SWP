import { Component, OnInit, ViewChild, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/component-base';


@Component({
    moduleId: module.id,
    selector: 'inspect',
    templateUrl: 'inspect.component.html',
    styleUrls: ['inspect.component.less']
})
export class InspectComponent extends AppComponentBase implements OnInit {

    constructor(injector: Injector

    ) {
        super(injector);
    }

    ngOnInit(): void {

    }




}
