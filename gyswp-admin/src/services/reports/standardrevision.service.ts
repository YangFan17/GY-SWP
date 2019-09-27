import { Inject, Optional, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CommonHttpClient } from "services/common-httpclient";
import { map } from "rxjs/operators";
import { API_BASE_URL } from "@shared/service-proxies/service-proxies";
import { NzTreeNode } from "ng-zorro-antd";
import { PagedResultDto } from "@shared/component-base";

@Injectable()
export class StandardRevisionService {
    private _commonhttp: CommonHttpClient;
    baseUrl: string;

    constructor(@Inject(CommonHttpClient) commonhttp: CommonHttpClient
        , @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this._commonhttp = commonhttp;
        this.baseUrl = baseUrl ? baseUrl : "";
    }

    GetSearchStandardRevisions(input: any): Observable<any> {
        let url_ = "/api/services/app/StandardRevision/GetSearchStandardRevisions";
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

    getDocumentConfirmsList(params: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/Document/GetReportDocumentConfirmsListAsync";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    GetEmpConfirmListById(params: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/Document/GetPagedEmpConfirmListByIdAsync";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    getRevisionClauseReportList(params: any): Observable<any> {
        let url_ = "/api/services/app/Clause/GetRevisionClauseReportAsync";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            return data;
        }));
    }
}