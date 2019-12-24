import { Inject, Optional, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CommonHttpClient } from "services/common-httpclient";
import { map } from "rxjs/operators";
import { API_BASE_URL } from "@shared/service-proxies/service-proxies";
import { NzTreeNode } from "ng-zorro-antd";
import { ApiResult } from "entities";

@Injectable()
export class SuperviseService {
    private _commonhttp: CommonHttpClient;
    baseUrl: string;

    constructor(@Inject(CommonHttpClient) commonhttp: CommonHttpClient
        , @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this._commonhttp = commonhttp;
        this.baseUrl = baseUrl ? baseUrl : "";
    }

    GetSupervises(input: any): Observable<any> {
        let url_ = "/api/services/app/Supervise/GetSupervisesAsync";
        return this._commonhttp.get(url_, input).pipe(map(data => {
            return data;
        }));
    }

    getSuperviseReportData(input: any): Observable<any> {
        let url_ = "/api/services/app/Supervise/GetSuperviseReportDataAsync";
        return this._commonhttp.get(url_, input).pipe(map(data => {
            return data;
        }));
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

    getQGSuperviseSummaryAsync(input: any): Observable<any> {
        let url_ = "/api/services/app/Supervise/GetQGSuperviseSummaryAsync";
        return this._commonhttp.get(url_, input).pipe(map(data => {
            return data;
        }));
    }

    exportQGSuperviseSummary(param: any): Observable<ApiResult> {
        var _url = '/api/services/app/Supervise/ExportQGSuperviseSummary';
        return this._commonhttp.post(_url, param).pipe(map(data => {
            return ApiResult.fromJS(data);
        }));
    }
}