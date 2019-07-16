import { Inject, Optional, Injectable } from "@angular/core";
import { Observer, Observable } from "rxjs";
import { CommonHttpClient } from "services/common-httpclient";
import { map } from "rxjs/operators";
import { NzTreeNode } from "ng-zorro-antd";
import { ApiResult, DocumentDto, Clause, Employee, Attachment } from "entities";
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

    getTreesAsync(): Observable<NzTreeNode[]> {
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

    synchronousOrganizationAsync(): Observable<ApiResult> {
        let url_ = "/api/services/app/Organization/SynchronousOrganizationAsync";
        return this._commonhttp.post(url_).pipe(map(data => {
            return data.result;
        }));
    }

    getEmployeeListAsync(params: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/Employee/GetEmployeeListByIdAsync";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    getEmployeeListByDeptIdAsync(params: any): Observable<Employee[]> {
        let url_ = "/api/services/app/Employee/GetEmployeeListByDeptIdAsync";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            return Employee.fromJSArray(data);
        }));
    }

    getDocumentListAsync(param: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/Document/getPaged";
        return this._commonhttp.get(url_, param).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    download(param: any): Observable<ApiResult> {
        let url_ = "/api/services/app/Document/downloadQRCodeZip";
        return this._commonhttp.post(url_, param).pipe(map(data => {
            return ApiResult.fromJS(data);
        }));
    }

    deleteCategoryById(id: number): Observable<ApiResult> {
        let url_ = "/api/services/app/Category/CategoryRemoveById";
        var param = { id: id };
        return this._commonhttp.post(url_, param).pipe(map(data => {
            return data;
        }));
    }

    getTreeByCategoryAsync(deptId: any): Observable<any> {
        let url_ = "/api/services/app/Category/GetTreeAsync";
        return this._commonhttp.get(url_, { deptId: deptId }).pipe(map(data => {
            return data;
        }));
    }

    getParentName(id: number): Observable<string> {
        let url_ = "/api/services/app/Category/GetParentName?id=" + id;
        return this._commonhttp.get(url_).pipe(map(data => {
            return data;
        }));
    }

    createOrUpdateCategory(input: any): Observable<any> {
        let url_ = "/api/services/app/Category/CreateOrUpdate";
        let cat = { category: input };
        return this._commonhttp.post(url_, cat).pipe(map(data => {
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

    createOrUpdateDocumentAsync(input: any): Observable<any> {
        let url_ = "/api/services/app/Document/CreateOrUpdate";
        let cat = { document: input };
        return this._commonhttp.post(url_, cat).pipe(map(data => {
            return data;
        }));
    }

    getDocumentByIdAsync(id: any): Observable<DocumentDto> {
        let url_ = "/api/services/app/Document/getById";
        return this._commonhttp.get(url_, { id: id }).pipe(map(data => {
            return DocumentDto.fromJS(data);
        }));
    }

    createOrUpdateClauseAsync(input: any): Observable<any> {
        let url_ = "/api/services/app/Clause/CreateOrUpdate";
        let cla = { clause: input };
        return this._commonhttp.post(url_, cla).pipe(map(data => {
            return data;
        }));
    }

    getClauseListAsync(params: any): Observable<any> {
        let url_ = "/api/services/app/Clause/GetClauseTreeAsync";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            return data;
        }));
    }

    getClauseByIdAsync(id: any): Observable<Clause> {
        let url_ = "/api/services/app/Clause/getById";
        return this._commonhttp.get(url_, { id: id }).pipe(map(data => {
            return Clause.fromJS(data);
        }));
    }

    deleteClauseById(id: string): Observable<ApiResult> {
        let url_ = "/api/services/app/Clause/ClauseRemoveById";
        var param = { id: id };
        return this._commonhttp.post(url_, param).pipe(map(data => {
            return data;
        }));
    }

    uploadAttachment(input: any): Observable<any> {
        let url_ = "/api/services/app/DocAttachment/CreateOrUpdate";
        let att = { docAttachment: input };
        return this._commonhttp.post(url_, att).pipe(map(data => {
            return data;
        }));
    }

    getAttachmentListByIdAsync(params: any): Observable<Attachment[]> {
        let url_ = "/api/services/app/DocAttachment/GetAttachmentListByIdAsync";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            return Attachment.fromJSArray(data);
        }));
    }

    getClauseAttachmentsById(params: any): Observable<Attachment[]> {
        let url_ = "/api/services/app/DocAttachment/GetClauseAttachmentsByIdAsync";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            return Attachment.fromJSArray(data);
        }));
    }

    getCriterionAttachmentById(params: any): Observable<Attachment[]> {
        let url_ = "/api/services/app/DocAttachment/GetCriterionAttachmentByIdAsync";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            return Attachment.fromJSArray(data);
        }));
    }

    deleteAttachmentByIdAsync(id: string): Observable<any> {
        let url_ = "/api/services/app/DocAttachment/Delete";
        return this._commonhttp.delete(url_, { id: id }).pipe(map(data => {
            return data;
        }));
    }

    documentReadAsync(params: any): Observable<ApiResult> {
        let url_ = "/api/services/app/Document/DocumentReadAsync";
        return this._commonhttp.post(url_, params).pipe(map(data => {
            return data;
        }));
    }
}