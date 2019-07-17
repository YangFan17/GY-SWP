import { Component } from '@angular/core';
import { WorkCriterionService } from 'services';

@Component({
    moduleId: module.id,
    selector: 'position-info',
    templateUrl: 'position-info.component.html',
    providers: [WorkCriterionService]
})
export class PositionInfoComponent {

}