import { Inject, Optional, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CommonHttpClient } from "services/common-httpclient";
import { map } from "rxjs/operators";
import { ApiResult, DocumentDto, Clause, SelectGroup, ClauseRevision, DocRevision, CriterionExamine, ExamineRecord, Attachment, ExamineResult, ExamineFeedback } from "entities";
import { PagedResultDto } from "@shared/component-base";
import { API_BASE_URL } from "@shared/service-proxies/service-proxies";
import { NzTreeNode } from "ng-zorro-antd";

@Injectable()
export class LogisticService {
    private _commonhttp: CommonHttpClient;
    baseUrl: string;

    constructor(@Inject(CommonHttpClient) commonhttp: CommonHttpClient
        , @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this._commonhttp = commonhttp;
        this.baseUrl = baseUrl ? baseUrl : "";
    }

    getSearchInspectReports(input: any): Observable<any> {
        let url_ = "/api/services/app/Inspect/GetSearchInspectReports";
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

    getPagedInStorageRecordAsync(param: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/LC_InStorageRecord/GetPaged";
        return this._commonhttp.get(url_, param).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    getPagedQualityRecordAsync(param: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/LC_QualityRecord/GetPaged";
        return this._commonhttp.get(url_, param).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    getPagedInStorageBillAsync(param: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/LC_InStorageBill/GetPaged";
        return this._commonhttp.get(url_, param).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    getPagedInStorageScanAsync(param: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/LC_ScanRecord/GetPaged";
        return this._commonhttp.get(url_, param).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }
}