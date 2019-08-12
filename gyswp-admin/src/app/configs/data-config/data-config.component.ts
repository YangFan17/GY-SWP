import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { STColumn, STComponent, STPage, STChange } from '@delon/abc';
import { DataConfigServiceProxy } from 'services'
import { PagedResultDto } from '@shared/component-base';
import { ModifyConfigComponent } from './modify-config/modify-config.component'
import { SystemData } from 'entities';
import { type } from 'os';

@Component({
    moduleId: module.id,
    selector: 'data-config',
    templateUrl: 'data-config.component.html',
    styleUrls: ['data-config.component.less']
})
export class DataConfigComponent extends AppComponentBase implements OnInit {
    configModelId: number = 1;
    configDingType: number;
    standardizations: any;
    standardizationTotal: number = 0;
    logisticss: any;
    logisticsTotal: number = 0;
    configDing = [
        { value: 1, text: '钉钉配置', selected: true },
        { value: 2, text: '企业标准库管理系统', selected: false },
        { value: 3, text: '设备管理', selected: false }
    ]
    loading = false;
    pages: STPage = {
        total: true,//分页显示多少条数据，字符串型
        show: true,//显示分页
        front: false, //关闭前端分页，true是前端分页，false后端控制分页
        showSize: true,
        pageSizes: [10, 20, 30, 40]
    };
    @ViewChild('st')
    st: STComponent;
    configData: STColumn[] = [
        { title: '所属模块', index: 'modelName' },
        { title: '编码', index: 'code' },
        { title: '配置项', index: 'desc' },
        { title: '备注', index: 'remark' },
        { title: '序号', index: 'seq', type: 'number' },
        {
            title: '操作', className: 'text-center',
            buttons: [
                {
                    text: '编辑',
                    click: (item: any) => this.editDing(item.id),
                },
                {
                    text: '删除',
                    click: (item: any) => this.delete(item),
                }
            ],
        },
    ];

    constructor(injector: Injector, private dataConfigServiceProxy: DataConfigServiceProxy) {
        super(injector);
    }

    ngOnInit() {
        this.getconfigs();
    }

    getconfigs() {  //modelId:1-企业标准库管理系统,2-物流中心,3-钉钉配置  type:1-钉钉配置,2-企业标准库管理系统
        this.loading = true;
        let params: any = {};
        params.SkipCount = (this.st.pi - 1) * this.st.ps;
        params.MaxResultCount = this.st.ps;
        params.ModelId = this.configModelId;
        if (this.configModelId === 3 && !this.configDingType)
            params.Type = 1;
        else
            params.Type = this.configDingType;
        if (this.configModelId === 1 || this.configModelId === 2)
            params.Type = '';
        this.dataConfigServiceProxy.getAll(params).subscribe((result: PagedResultDto) => {
            this.loading = false;
            if (this.configModelId === 1) {
                this.standardizations = result.items;
                this.standardizationTotal = result.totalCount;
            } else if (this.configModelId === 2) {
                this.logisticss = result.items;
                this.logisticsTotal = result.totalCount;
            } else {
                this.query.data = result.items;
                this.query.total = result.totalCount;
            }
        });
    }

    checkChangeconfigDing(tyep: number) {
        this.configDingType = tyep;
        this.getconfigs();
    }

    selectedChange(index: number) {
        switch (index) {
            case 0: this.configModelId = index + 1;
                if (this.standardizationTotal <= 0)
                    this.getconfigs();
                break;
            case 1: this.configModelId = index + 1;
                if (this.logisticsTotal <= 0)
                    this.getconfigs();
                break;
            case 2: this.configModelId = index + 1;
                if (this.query.total <= 0)
                    this.getconfigs();
                break;
            default: break;
        }
    }

    //编辑
    editDing(id: any) {
        this.modalHelper.open(ModifyConfigComponent, { id: id, modelId: this.configModelId }, 'md', {
            nzMask: true,
            nzClosable: false,
            nzMaskClosable: false,
        }).subscribe(isSave => {
            if (isSave) {
                this.getconfigs();
            }
        });
    }

    //新增
    create() {
        this.modalHelper.open(ModifyConfigComponent, { modelId: this.configModelId }, 'md', {
            nzMask: true,
            nzClosable: false,
            nzMaskClosable: false,
        }).subscribe(isSave => {
            if (isSave) {
                this.getconfigs();
            }
        });
    }


    //删除
    delete(entity: SystemData) {
        this.message.confirm(
            "是否删除该配置下的:'" + entity.code + "'?",
            "信息确认",
            (result: boolean) => {
                if (result) {
                    this.dataConfigServiceProxy.delete(entity.id).subscribe(() => {
                        this.notify.success('删除成功！');
                        this.getconfigs();
                    });
                }
            }
        )
    }


    stChange(e: STChange) {
        switch (e.type) {
            case 'pi':
                this.getconfigs();
                break;
            case 'ps':
                this.getconfigs();
                break;
        }
    }
}
