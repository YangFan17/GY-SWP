import { Injectable, Inject, Optional } from "@angular/core";
import { API_BASE_URL } from "@shared/service-proxies/service-proxies";
import { Observable } from "rxjs";
import { SystemData } from 'entities'
import { CommonHttpClient } from "services/common-httpclient";
import { PagedResultDto } from "@shared/component-base";
import { map } from "rxjs/operators";

@Injectable()
export class DataConfigServiceProxy {
    private _commonhttp: CommonHttpClient;
    baseUrl: string;

    constructor(@Inject(CommonHttpClient) commonhttp: CommonHttpClient
        , @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this._commonhttp = commonhttp;
        this.baseUrl = baseUrl ? baseUrl : "";
    }

    //获取分页数据
    getAll(params: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/SystemData/GetPagedAsync";
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
    getById(id: any): Observable<SystemData> {
        let _url = "/api/services/app/SystemData/GetByIdAsync";
        let param = { 'id': id };
        return this._commonhttp.get(_url, param).pipe(map(data => {
            return SystemData.fromJS(data);
        }));
    }


    /**
     * 更新与创建配置
     * @param input 
     */
    createOrUpdate(input: SystemData | null): Observable<any> {
        let _url = "/api/services/app/SystemData/CreateOrUpdateAsync";
        return this._commonhttp.post(_url, { "SystemData": input }).pipe(map(data => {
            return data;
        }))
    }


    /**
     * 删除配置
     * @param id 
     */
    delete(id: string): Observable<any> {
        let _url = "/api/services/app/SystemData/DeleteAsync";
        let param = { 'id': id };
        return this._commonhttp.delete(_url, param);
    }
}