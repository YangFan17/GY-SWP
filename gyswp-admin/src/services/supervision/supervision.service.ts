import { Inject, Optional, Injectable } from "@angular/core";
import { Observer, Observable } from "rxjs";
import { CommonHttpClient } from "services/common-httpclient";
import { map } from "rxjs/operators";
import { API_BASE_URL } from "@shared/service-proxies/service-proxies";
import { NzTreeNode } from "ng-zorro-antd";
import { Employee, ApiResult, CriterionExamine, ExamineRecord, ExamineResult, Attachment, ExamineFeedback } from "entities";
import { PagedResultDto } from "@shared/component-base";
@Injectable()
export class SupervisionService {
    private _commonhttp: CommonHttpClient;
    baseUrl: string;

    constructor(@Inject(CommonHttpClient) commonhttp: CommonHttpClient
        , @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this._commonhttp = commonhttp;
        this.baseUrl = baseUrl ? baseUrl : "";
    }

    getDeptExamineNzTreeNodes(): Observable<NzTreeNode[]> {
        let url_ = "/api/services/app/Organization/GetDeptExamineTreeAsync";
        return this._commonhttp.get(url_, {}).pipe(map(data => {
            let arry = [];
            // data.map(d => {
            //     console.log(d);
            //     let tree = new NzTreeNode(d);
            //     arry.push(tree);
            // });
            let tree = new NzTreeNode(data);
            arry.push(tree);
            return arry;
        }));
    }

    getEmployeeListByDeptIdAsync(params: any): Observable<Employee[]> {
        let url_ = "/api/services/app/Employee/GetEmployeeListByExamineAsync";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            return Employee.fromJSArray(data);
        }));
    }

    createExamineAsync(params: any): Observable<ApiResult> {
        let url_ = "/api/services/app/CriterionExamine/CreateExamineAsync";
        return this._commonhttp.post(url_, params).pipe(map(data => {
            return ApiResult.fromJS(data);
        }));
    }

    getDeptExamineRecorAsync(param: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/CriterionExamine/GetPaged";
        return this._commonhttp.get(url_, param).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    getExamineRecordByIdAsync(param: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/ExamineDetail/GetExamineRecordByIdAsync";
        return this._commonhttp.get(url_, param).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    getExamineInfo(params: any): Observable<CriterionExamine> {
        let url_ = "/api/services/app/CriterionExamine/GetById";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            return CriterionExamine.fromJS(data);
        }));
    }

    getExamineDetailByIdAsync(id: any): Observable<ExamineRecord> {
        let url_ = "/api/services/app/ExamineDetail/GetExamineDetailByIdAsync";
        return this._commonhttp.get(url_, { id: id }).pipe(map(data => {
            return ExamineRecord.fromJS(data);
        }));
    }

    getExamineResult(id: string): Observable<ExamineResult> {
        let url_ = "/api/services/app/ExamineResult/GetExamineResultByIdAsync";
        return this._commonhttp.get(url_, { id: id }).pipe(map(data => {
            return ExamineResult.fromJS(data);
        }));
    }

    getAttachmentListByIdAsync(params: any): Observable<Attachment[]> {
        let url_ = "/api/services/app/DocAttachment/GetAttachmentListByIdAsync";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            return Attachment.fromJSArray(data);
        }));
    }

    changeStatusByIdAsync(params: any): Observable<ApiResult> {
        let url_ = "/api/services/app/ExamineDetail/ChangeStatusByIdAsync";
        return this._commonhttp.post(url_, params).pipe(map(data => {
            return ApiResult.fromJS(data);
        }));
    }

    getExamineDetailByEmpIdAsync(param: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/ExamineDetail/GetExamineDetailByEmpIdAsync";
        return this._commonhttp.get(url_, param).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    getExamineFeedbackByIdAsync(id: any): Observable<ExamineFeedback> {
        let url_ = "/api/services/app/ExamineFeedback/GetExamineFeedbackByIdAsync";
        return this._commonhttp.get(url_, { id: id }).pipe(map(data => {
            return ExamineFeedback.fromJS(data);
        }));
    }
}