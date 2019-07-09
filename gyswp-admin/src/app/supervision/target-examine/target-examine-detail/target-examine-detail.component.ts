import { Component, OnInit, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/component-base';
import { ActivatedRoute, Router } from '@angular/router';
import { SupervisionService } from 'services';
import { Indicators } from 'entities';
import { TargetDeptComponent } from './target-dept/target-dept.component';

@Component({
    moduleId: module.id,
    selector: 'target-examine-detail',
    templateUrl: 'target-examine-detail.component.html'
})
export class TargetExamineDetailComponent extends AppComponentBase implements OnInit {
    id: string;
    cycleTime = '1';
    deptTags = [];
    indicator: Indicators = new Indicators();
    constructor(injector: Injector
        , private actRouter: ActivatedRoute
        , private router: Router
        , private supervisionService: SupervisionService
    ) {
        super(injector);
        this.id = this.actRouter.snapshot.params['id'];
    }

    ngOnInit(): void {
        if (this.id) {
            this.getById();
        }
    }

    cycleTimeRadioChange(ngmodel: string) {
        this.cycleTime = ngmodel;
    }

    save() {
        if (!this.indicator.id) {
            // // this.document.categoryDesc = this.category.name;
            // this.document.deptIds = this.dept.id;
            // this.document.isAction = true;
        }
        this.indicator.creationTime = this.dateFormat(this.indicator.creationTime);
        this.indicator.cycleTime = this.cycleTime == '1' ? 1 : 2;
        console.log(this.indicator);

        // // this.getUserDepts();
        // this.supervisionService.createOrUpdateDocumentAsync(this.indicator)
        //     .finally(() => { this.saving = false; })
        //     .subscribe(res => {
        //         this.notify.info('保存成功！', '');
        //         if (res.data) {
        //             this.indicator = res.data;
        //             this.id = this.indicator.id;
        //         }
        //     });
    }

    getById() {
        if (this.id) {
            // this.basicDataService.getDocumentByIdAsync(this.id).subscribe(res => {
            //     this.document = res;
            //     if (res.employeeIds || res.employeeDes) {
            //         this.setUserDepts(this.document);
            //     }
            //     this.isUpdate = true;
            //     this.isAllUser = res.isAllUser == true ? '1' : '0';
            //     this.category.id = res.categoryId.toString();
            //     this.category.name = res.categoryDesc;
            //     this.qrCode.value = res.id;
            //     this.codeStyle = 'block';
            //     this.clause.doc = { id: res.id, name: res.name };
            //     this.clause.getClauseList();
            // });
        }
    }

    showDeptModel() {
        this.modalHelper
            .open(TargetDeptComponent, { selectedDepts: this.deptTags }, 'md', {
                nzMask: true,
                nzClosable: false,
                nzMaskClosable: false,
            })
            .subscribe(isconfirm => {
                if (isconfirm) {
                    let empInfo: any[] = [];
                    this.deptTags.forEach(v => {
                        empInfo.push({ id: v.id, name: v.name });
                    });
                    console.log(empInfo);

                    // this.setUserDepts(this.document);
                }
            });
    }

    handleDeptClose(tag: any) {
        var i = 0;
        for (const item of this.deptTags) {
            if (item.id == tag.id) {
                this.deptTags.splice(i, 1);
                break;
            }
            i++;
        }
    }
}
