import { Component, OnInit, ViewChild, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/component-base';
import { NzTreeComponent } from 'ng-zorro-antd';
import { BasicDataService } from 'services';


@Component({
    moduleId: module.id,
    selector: 'inspect',
    templateUrl: 'inspect.component.html',
    styleUrls: ['inspect.component.less']
})
export class InspectComponent extends AppComponentBase implements OnInit {

    @ViewChild('detpTree') detpTree: NzTreeComponent;
    nodes: any[];
    selectedDept: any = { id: '', name: '' };

    constructor(injector: Injector,
        private basicDataService: BasicDataService) {
        super(injector);
    }

    ngOnInit(): void {
        this.getTrees();
    }

    getTrees() {
        this.basicDataService.getDeptDocNzTreeNodes().subscribe((data) => {
            this.nodes = data;
            if (data.length > 0) {
                var selectedNode = data[0].children[0];
                if (selectedNode && selectedNode.isSelected) {
                    this.selectedDept = { id: selectedNode.key, name: selectedNode.title };
                }
            }
        });
    }


}
