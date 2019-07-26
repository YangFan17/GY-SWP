import { Component, OnInit, Output, EventEmitter, Input, Injector } from '@angular/core';
import { Category } from 'entities';
import { Validators, FormControl } from '@angular/forms';
import { ModalFormComponentBase } from '@shared/component-base';
import { BasicDataService } from 'services';

@Component({
    moduleId: module.id,
    selector: 'edit-category',
    templateUrl: 'edit-category.component.html',
    styleUrls: ['edit-category.component.less']
})
export class EditCategoryComponent extends ModalFormComponentBase<Category> implements OnInit {
    @Output() modalSelect: EventEmitter<any> = new EventEmitter<any>();
    @Input() category: Category = new Category();
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
        this.setFormValues(this.category);
    }


    duplicateValidator = (control: FormControl): { [s: string]: boolean } => {
        if (!control.value) {
            return { required: true };
        } else if (this.names.indexOf(control.value) > -1) {
            return { duplicate: true, error: true };
        }
    }

    protected submitExecute(finisheCallback: Function): void {
        this.basicDataService.createOrUpdateCategory(this.category)
            .finally(() => { this.saving = false; })
            .subscribe(res => {
                this.notify.success('保存成功', '');
                this.success(true);
            });
    }

    protected setFormValues(entity: Category): void {
        this.setControlVal('name', entity.name);
    }

    protected getFormValues(): void {
        this.category.name = this.getControlVal('name');

    }
}
