import { Injectable, Inject, Optional } from "@angular/core";
import { API_BASE_URL } from "@shared/service-proxies/service-proxies";
import { Observable } from "rxjs";
import { Advise } from 'entities'
import { CommonHttpClient } from "services/common-httpclient";
import { PagedResultDto } from "@shared/component-base";
import { map } from "rxjs/operators";
import { NzTreeNode } from "ng-zorro-antd";

@Injectable()
export class AdviseService {
    private _commonhttp: CommonHttpClient;
    baseUrl: string;

    constructor(@Inject(CommonHttpClient) commonhttp: CommonHttpClient
        , @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this._commonhttp = commonhttp;
        this.baseUrl = baseUrl ? baseUrl : "";
    }

    //获取分页数据
    getMyAdviceList(params: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/Advise/GetPagedMyAdviceAsync";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    getPublicityAdviceList(params: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/Advise/GetPagedPublicityAdviceAsync";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    getPublicityManagmentList(params: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/Advise/GetPagedPublicityManagmentAsync";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    /**
     * 获取单条数据详细信息
     * @param id 
    */
    getById(id: any): Observable<Advise> {
        let _url = "/api/services/app/Advise/GetByIdAsync";
        let param = { 'id': id };
        return this._commonhttp.get(_url, param).pipe(map(data => {
            return Advise.fromJS(data);
        }));
    }


    /**
   * 获取合理化建议报表数据
   * @param input 
  */
    getAdviseReports(input: any): Observable<any> {
        let _url = "/api/services/app/Advise/GetAdviseReportsAsync";
        return this._commonhttp.get(_url, input).pipe(map(data => {
            return data;
        }));
    }


    /**
     * 更新与创建配置
     * @param input 
     */
    createOrUpdate(input: Advise | null): Observable<any> {
        let _url = "/api/services/app/Advise/CreateOrUpdateAsync";
        return this._commonhttp.post(_url, { "Advise": input }).pipe(map(data => {
            return data;
        }))
    }


    /**
     * 删除配置
     * @param id 
     */
    delete(id: string): Observable<any> {
        let _url = "/api/services/app/Advise/DeleteAsync";
        let param = { 'id': id };
        return this._commonhttp.delete(_url, param);
    }

    getDeptDocNzTreeNodes(root: any): Observable<NzTreeNode[]> {
        let url_ = "/api/services/app/Document/GetDeptDocNzTreeNodesAsync";
        return this._commonhttp.get(url_, { rootName: root }).pipe(map(data => {
            let arry = [];
            data.map(d => {
                let tree = new NzTreeNode(d);
                arry.push(tree);
            });
            return arry;
        }));
    }
    //更改公示状态方法
    changePubStatus(id: string): Observable<any> {
        let _url = "/api/services/app/Advise/ChangePubStatusAsync";
        let param = { 'id': id };
        return this._commonhttp.post(_url, param).pipe(map(data => {
            return data;
        }))
    }
}