import { Inject, Optional, Injectable } from "@angular/core";
import { Observer, Observable } from "rxjs";
import { CommonHttpClient } from "services/common-httpclient";
import { map } from "rxjs/operators";
import { NzTreeNode } from "ng-zorro-antd";
import { ApiResult, DocumentDto, ClauseTree, Clause, Employee } from "entities";
import { PagedResultDto } from "@shared/component-base";
import { API_BASE_URL } from "@shared/service-proxies/service-proxies";

@Injectable()
export class BasicDataService {
    private _commonhttp: CommonHttpClient;
    baseUrl: string;

    constructor(@Inject(CommonHttpClient) commonhttp: CommonHttpClient
        , @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this._commonhttp = commonhttp;
        this.baseUrl = baseUrl ? baseUrl : "";
    }

    GetTreesAsync(): Observable<NzTreeNode[]> {
        let url = "/api/services/app/Organization/GetTreesAsync";
        return this._commonhttp.get(url).pipe(map(data => {
            let arry = [];
            data.map(d => {
                let tree = new NzTreeNode(d);
                arry.push(tree);
            });
            return arry;
        }));
    }

    SynchronousOrganizationAsync(): Observable<ApiResult> {
        let url_ = "/api/services/app/Organization/SynchronousOrganizationAsync";
        return this._commonhttp.post(url_).pipe(map(data => {
            return data.result;
        }));
    }

    GetEmployeeListAsync(params: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/Employee/GetEmployeeListByIdAsync";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    GetEmployeeListByDeptIdAsync(params: any): Observable<Employee[]> {
        let url_ = "/api/services/app/Employee/GetEmployeeListByDeptIdAsync";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            return Employee.fromJSArray(data);
        }));
    }

    GetDocumentListAsync(param: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/Document/getPaged";
        return this._commonhttp.get(url_, param).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    Download(param: any): Observable<ApiResult> {
        let url_ = "/api/services/app/Document/downloadQRCodeZip";
        return this._commonhttp.post(url_, param).pipe(map(data => {
            return ApiResult.fromJS(data);
        }));
    }

    DeleteCategoryById(id: number): Observable<ApiResult> {
        let url_ = "/api/services/app/Category/CategoryRemoveById";
        var param = { id: id };
        return this._commonhttp.post(url_, param).pipe(map(data => {
            return data;
        }));
    }

    GetTreeByCategoryAsync(deptId: any): Observable<any> {
        let url_ = "/api/services/app/Category/GetTreeAsync";
        return this._commonhttp.get(url_, { deptId: deptId }).pipe(map(data => {
            return data;
        }));
    }

    GetParentName(id: number): Observable<string> {
        let url_ = "/api/services/app/Category/GetParentName?id=" + id;
        return this._commonhttp.get(url_).pipe(map(data => {
            return data;
        }));
    }

    CreateOrUpdateCategory(input: any): Observable<any> {
        let url_ = "/api/services/app/Category/CreateOrUpdate";
        let cat = { category: input };
        return this._commonhttp.post(url_, cat).pipe(map(data => {
            return data;
        }));
    }

    GetDeptDocNzTreeNodes(): Observable<NzTreeNode[]> {
        let url_ = "/api/services/app/Document/GetDeptDocNzTreeNodesAsync";
        return this._commonhttp.get(url_, {}).pipe(map(data => {
            let arry = [];
            data.map(d => {
                let tree = new NzTreeNode(d);
                arry.push(tree);
            });
            return arry;
        }));
    }

    CreateOrUpdateDocumentAsync(input: any): Observable<any> {
        let url_ = "/api/services/app/Document/CreateOrUpdate";
        let cat = { document: input };
        return this._commonhttp.post(url_, cat).pipe(map(data => {
            return data;
        }));
    }

    GetDocumentByIdAsync(id: any): Observable<DocumentDto> {
        let url_ = "/api/services/app/Document/getById";
        return this._commonhttp.get(url_, { id: id }).pipe(map(data => {
            return DocumentDto.fromJS(data);
        }));
    }

    CreateOrUpdateClauseAsync(input: any): Observable<any> {
        let url_ = "/api/services/app/Clause/CreateOrUpdate";
        let cla = { clause: input };
        return this._commonhttp.post(url_, cla).pipe(map(data => {
            return data;
        }));
    }

    GetClauseListAsync(params: any): Observable<any> {
        let url_ = "/api/services/app/Clause/GetClauseTreeAsync";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            return data;
        }));
    }

    GetClauseByIdAsync(id: any): Observable<Clause> {
        let url_ = "/api/services/app/Clause/getById";
        return this._commonhttp.get(url_, { id: id }).pipe(map(data => {
            return Clause.fromJS(data);
        }));
    }

    DeleteClauseById(id: string): Observable<ApiResult> {
        let url_ = "/api/services/app/Clause/ClauseRemoveById";
        var param = { id: id };
        return this._commonhttp.post(url_, param).pipe(map(data => {
            return data;
        }));
    }
}