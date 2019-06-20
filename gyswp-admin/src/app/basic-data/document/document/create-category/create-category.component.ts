import { Component, OnInit, Output, EventEmitter, Input, Injector } from '@angular/core';
import { ModalFormComponentBase } from '@shared/component-base';
import { Category } from 'entities';
import { Validators, FormControl } from '@angular/forms';
import { BasicDataService } from 'services';

@Component({
    moduleId: module.id,
    selector: 'create-category',
    templateUrl: 'create-category.component.html',
    styleUrls: ['create-category.component.less']
})
export class CreateCategoryComponent extends ModalFormComponentBase<Category> implements OnInit {
    @Output() modalSelect: EventEmitter<any> = new EventEmitter<any>();
    category: Category = new Category();

    @Input() pid: number = 0;
    @Input() pname: string;
    @Input() deptId: any;
    @Input() names: any[];

    constructor(
        injector: Injector
        , private basicDataService: BasicDataService
    ) {
        super(injector);
    }

    ngOnInit() {
        this.validateForm = this.formBuilder.group({
            name: ['', [Validators.required]]
        });
        this.category.parentId = this.pid;
    }


    duplicateValidator = (control: FormControl): { [s: string]: boolean } => {
        if (!control.value) {
            return { required: true };
        } else if (this.names.indexOf(control.value) > -1) {
            return { duplicate: true, error: true };
        }
    }


    protected submitExecute(finisheCallback: Function): void {
        this.category.deptId = this.deptId;
        this.basicDataService.createOrUpdateCategory(this.category)
            .finally(() => { this.saving = false; })
            .subscribe(res => {
                this.notify.info(this.l('SavedSuccessfully'), '');
                this.success(true);
            });
    }

    protected setFormValues(entity: Category): void {

    }

    protected getFormValues(): void {
        this.category.name = this.getControlVal('name');
    }
}
