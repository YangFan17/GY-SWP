import { Inject, Optional, Injectable } from "@angular/core";
import { Observer, Observable } from "rxjs";
import { CommonHttpClient } from "services/common-httpclient";
import { map } from "rxjs/operators";
import { API_BASE_URL } from "@shared/service-proxies/service-proxies";
import { NzTreeNode } from "ng-zorro-antd";
import { Employee, ApiResult, CriterionExamine, ExamineRecord, ExamineResult, Attachment, ExamineFeedback, Indicators, IndicatorShowDto } from "entities";
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

    getAllEmployeeByDeptIdAsync(params: any): Observable<Employee[]> {
        let url_ = "/api/services/app/Employee/GetEmployeeListByDeptIdAsync";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            return Employee.fromJSArray(data);
        }));
    }

    createInternalExamineAsync(params: any): Observable<ApiResult> {
        let url_ = "/api/services/app/CriterionExamine/CreateInternalExamineAsync";
        return this._commonhttp.post(url_, params).pipe(map(data => {
            return ApiResult.fromJS(data);
        }));
    }

    createCountryDeptInternalExamineAsync(params: any): Observable<ApiResult> {
        let url_ = "/api/services/app/CriterionExamine/CreateCountryDeptInternalExamineAsync";
        return this._commonhttp.post(url_, params).pipe(map(data => {
            return ApiResult.fromJS(data);
        }));
    }

    createExamineByQiGuanAsync(params: any): Observable<ApiResult> {
        let url_ = "/api/services/app/CriterionExamine/CreateExamineByQiGuanAsync";
        return this._commonhttp.post(url_, params).pipe(map(data => {
            return ApiResult.fromJS(data);
        }));
    }

    getDeptExamineRecordAsync(param: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/CriterionExamine/GetPaged";
        return this._commonhttp.get(url_, param).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    getPagedCriterionRecordByQGAdmin(param: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/CriterionExamine/GetPagedCriterionRecordByQGAdmin";
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

    getExamineRecordNoPagedByIdAsync(param: any): Observable<ExamineRecord[]> {
        let url_ = "/api/services/app/ExamineDetail/GetExamineRecordNoPagedByIdAsync";
        return this._commonhttp.get(url_, param).pipe(map(data => {
            return ExamineRecord.fromJSArray(data);
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

    getDeptTreeByQGAdminAsync(): Observable<NzTreeNode[]> {
        let url_ = "/api/services/app/Organization/GetDeptTreeByQGAdminAsync";
        return this._commonhttp.get(url_, {}).pipe(map(data => {
            let arry = [];
            let tree = new NzTreeNode(data);
            arry.push(tree);
            return arry;
        }));
    }

    getDeptTreeByCountyAdminAsync(): Observable<NzTreeNode[]> {
        let url_ = "/api/services/app/Organization/GetDeptTreeByCountyAdminAsync";
        return this._commonhttp.get(url_, {}).pipe(map(data => {
            let arry = [];
            let tree = new NzTreeNode(data);
            arry.push(tree);
            return arry;
        }));
    }

    getTargetExamineDeptTreeAsync(): Observable<NzTreeNode[]> {
        let url_ = "/api/services/app/Organization/GetTargetExamineDeptTreeAsync";
        return this._commonhttp.get(url_, {}).pipe(map(data => {
            let arry = [];
            let tree = new NzTreeNode(data);
            arry.push(tree);
            return arry;
        }));
    }

    getIndicatorListAsync(param: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/Indicator/GetPaged";
        return this._commonhttp.get(url_, param).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    getTreesAsync(): Observable<NzTreeNode[]> {
        let url = "/api/services/app/Organization/GetTargetTreesAsync";
        return this._commonhttp.get(url).pipe(map(data => {
            let arry = [];
            data.map(d => {
                let tree = new NzTreeNode(d);
                arry.push(tree);
            });
            return arry;
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

    getDeptDocNzTreeNodesNoPermission(root: any): Observable<NzTreeNode[]> {
        let url_ = "/api/services/app/Document/GetDeptDocNzTreeNodesNoPermissionAsync";
        return this._commonhttp.get(url_, { rootName: root }).pipe(map(data => {
            let arry = [];
            data.map(d => {
                let tree = new NzTreeNode(d);
                arry.push(tree);
            });
            return arry;
        }));
    }

    getDeptId(): Observable<string> {
        let url_ = "/api/services/app/Organization/GetDeptIdAsync";
        return this._commonhttp.get(url_).pipe(map(data => {
            return data;
        }));
    }

    createOrUpdateIndicatorAsync(params: any, deptInfo: any[]): Observable<ApiResult> {
        let url_ = "/api/services/app/Indicator/CreateOrUpdate";
        let input = { Indicator: params, DeptInfo: deptInfo }
        return this._commonhttp.post(url_, input).pipe(map(data => {
            return ApiResult.fromJS(data);
        }));
    }

    getIndicatorByIdAsync(id: any): Observable<Indicators> {
        let url_ = "/api/services/app/Indicator/getById";
        return this._commonhttp.get(url_, { id: id }).pipe(map(data => {
            return Indicators.fromJS(data);
        }));
    }

    getIndicatorListById(id: string): Observable<any[]> {
        let url_ = "/api/services/app/Indicator/GetDeptIndicatorDetailByIdAsync";
        return this._commonhttp.get(url_, { id: id }).pipe(map(data => {
            return data;
        }));
    }

    publishCriterionExamineAsync(input: any): Observable<ApiResult> {
        let url_ = "/api/services/app/CriterionExamine/PublishCriterionExamineAsync";
        return this._commonhttp.post(url_, input).pipe(map(data => {
            return ApiResult.fromJS(data);
        }));
    }

    deleteExamineDetailByIdAsync(id: string): Observable<any> {
        let url_ = "/api/services/app/ExamineDetail/Delete";
        return this._commonhttp.delete(url_, { id: id }).pipe(map(data => {
            return data;
        }));
    }

    getIndicatorDetailByIdAsync(id: any): Observable<IndicatorShowDto> {
        let url_ = "/api/services/app/Indicator/GetIndicatorDetailByIdAsync";
        return this._commonhttp.get(url_, { id: id }).pipe(map(data => {
            return IndicatorShowDto.fromJS(data);
        }));
    }

    changeIndicatorStatusByIdAsync(input: any): Observable<ApiResult> {
        let url_ = "/api/services/app/IndicatorsDetail/ChangeStatusByIdAsync";
        return this._commonhttp.post(url_, input).pipe(map(data => {
            return ApiResult.fromJS(data);
        }));
    }

    getCurDeptDocListAsync(param: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/Document/GetPagedCurDeptDocListAsync";
        return this._commonhttp.get(url_, param).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }
}