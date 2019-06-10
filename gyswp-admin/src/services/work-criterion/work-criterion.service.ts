import { Inject, Optional, Injectable } from "@angular/core";
import { Observer, Observable } from "rxjs";
import { CommonHttpClient } from "services/common-httpclient";
import { map } from "rxjs/operators";
import { ApiResult, DocumentDto, ClauseTree, Clause, Employee, SelectGroup } from "entities";
import { PagedResultDto } from "@shared/component-base";
import { API_BASE_URL } from "@shared/service-proxies/service-proxies";

@Injectable()
export class WorkCriterionService {
    private _commonhttp: CommonHttpClient;
    baseUrl: string;

    constructor(@Inject(CommonHttpClient) commonhttp: CommonHttpClient
        , @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this._commonhttp = commonhttp;
        this.baseUrl = baseUrl ? baseUrl : "";
    }

    getCategoryTypeAsync(): Observable<SelectGroup[]> {
        let url_ = "/api/services/app/Category/GetCategoryTypeByDeptAsync";
        return this._commonhttp.get(url_).pipe(map(data => {
            return SelectGroup.fromJSArray(data);
        }));
    }

    getDocumentListAsync(param: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/Document/GetPagedWithPermission";
        return this._commonhttp.get(url_, param).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    getClauseListAsync(params: any): Observable<any> {
        let url_ = "/api/services/app/Clause/GetClauseTreeAsync1";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            return data;
        }));
    }

    getDocInfoAsync(params: any): Observable<DocumentDto> {
        let url_ = "/api/services/app/Document/GetDocumentTitleAsync";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            return DocumentDto.fromJS(data);
        }));
    }

    confirmClauseAsync(ids: string[], docId): Observable<any> {
        let url_ = "/api/services/app/EmployeeClause/ConfirmClauseAsync";
        let input = { clauseIds: ids, docId: docId };
        return this._commonhttp.post(url_, input).pipe(map(data => {
            return data;
        }));
    }

    GetClauseByIdAsync(id: any): Observable<Clause> {
        let url_ = "/api/services/app/Clause/getById";
        return this._commonhttp.get(url_, { id: id }).pipe(map(data => {
            return Clause.fromJS(data);
        }));
    }
}