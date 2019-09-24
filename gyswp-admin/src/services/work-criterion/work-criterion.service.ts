import { Inject, Optional, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CommonHttpClient } from "services/common-httpclient";
import { map } from "rxjs/operators";
import { ApiResult, DocumentDto, Clause, SelectGroup, ClauseRevision, DocRevision, CriterionExamine, ExamineRecord, Attachment, ExamineResult, ExamineFeedback, Indicators, IndicatorShowDto } from "entities";
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

    getActionDocumentAsync(param: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/Document/GetActionDocumentsAsync";
        return this._commonhttp.get(url_, param).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    getDraftDocCategoryAsync(): Observable<SelectGroup[]> {
        let url_ = "/api/services/app/Category/GetDraftDocCategoryAsync";
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
        let url_ = "/api/services/app/Clause/GetClauseTreeWithCheckedAsync";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            return data;
        }));
    }

    getClauseRevisionListAsync(id: string): Observable<any> {
        let url_ = "/api/services/app/ClauseRevision/GetDraftClauseTreeWithCheckedAsync";
        return this._commonhttp.get(url_, { documentId: id }).pipe(map(data => {
            return data;
        }));
    }

    getDocInfoAsync(params: any): Observable<DocumentDto> {
        let url_ = "/api/services/app/Document/GetDocumentTitleAsync";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            return DocumentDto.fromJS(data);
        }));
    }

    getUserOperateAsync(params: any): Observable<ApiResult> {
        let url_ = "/api/services/app/EmployeeClause/GetUserOperateAsync";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            return ApiResult.fromJS(data);
        }));
    }

    getUserOperateDraftAsync(): Observable<ApiResult> {
        let url_ = "/api/services/app/EmployeeClause/GetUserOperateDraftAsync";
        return this._commonhttp.get(url_).pipe(map(data => {
            return ApiResult.fromJS(data);
        }));
    }

    getDraftOperateDraftAsync(): Observable<ApiResult> {
        let url_ = "/api/services/app/EmployeeClause/GetDraftOperateDraftAsync";
        return this._commonhttp.get(url_).pipe(map(data => {
            return ApiResult.fromJS(data);
        }));
    }

    confirmClauseAsync(ids: string[], docId): Observable<any> {
        let url_ = "/api/services/app/EmployeeClause/ConfirmClauseAsync";
        let input = { clauseIds: ids, docId: docId };
        return this._commonhttp.post(url_, input).pipe(map(data => {
            return data;
        }));
    }

    getClauseByIdAsync(id: any): Observable<Clause> {
        let url_ = "/api/services/app/Clause/getById";
        return this._commonhttp.get(url_, { id: id }).pipe(map(data => {
            return Clause.fromJS(data);
        }));
    }

    selfCheckedClauseAsync(id: string, docId: string): Observable<any> {
        let url_ = "/api/services/app/SelfChekRecord/SelfCheckedClauseAsync";
        let input = { clauseId: id, docId: docId };
        return this._commonhttp.post(url_, input).pipe(map(data => {
            return data;
        }));
    }


    applyDocAsync(input: any): Observable<any> {
        let url_ = "/api/services/app/ApplyInfo/ApplyDocAsync";
        // let info = { applyInfo: input };
        return this._commonhttp.post(url_, input).pipe(map(data => {
            return data;
        }));
    }

    getClauseRevisionListById(params): Observable<any> {
        let url_ = "/api/services/app/ClauseRevision/GetClauseRevisionListByIdAsync";
        // var input = { DocumentId: docId, ApplyInfoId: applyId };
        return this._commonhttp.get(url_, params).pipe(map(data => {
            return data;
        }));
    }

    createRevisionAsync(input: any, type: number, applyId: string): Observable<any> {
        let url_ = "/api/services/app/ClauseRevision/CreateRevisionAsync";
        let cla = { entity: input, RevisionType: type, ApplyId: applyId };
        return this._commonhttp.post(url_, cla).pipe(map(data => {
            return data;
        }));
    }

    createDeleteRevisionAsync(docId: string, applyId: string): Observable<any> {
        let url_ = "/api/services/app/ClauseRevision/CreateDeleteRevisionAsync";
        let cla = { DocumentId: docId, ApplyId: applyId };
        return this._commonhttp.post(url_, cla).pipe(map(data => {
            return data;
        }));
    }


    deleteClauseById(id: string, docId: string, applyId: string): Observable<ApiResult> {
        let url_ = "/api/services/app/ClauseRevision/ClauseRevisionRemoveById";
        var input = { DocumentId: docId, ApplyInfoId: applyId, Id: id };
        return this._commonhttp.post(url_, input).pipe(map(data => {
            return data;
        }));
    }

    removeRevisionById(id: string): Observable<ApiResult> {
        let url_ = "/api/services/app/ClauseRevision/ClauseRevisionDeleteById";
        return this._commonhttp.post(url_, { id: id }).pipe(map(data => {
            return data;
        }));
    }

    removeDraftRevisionById(id: string): Observable<ApiResult> {
        let url_ = "/api/services/app/ClauseRevision/ClauseDraftRemoveById";
        return this._commonhttp.post(url_, { id: id }).pipe(map(data => {
            return data;
        }));
    }

    getClauseRevisionByIdAsync(id: any): Observable<ClauseRevision> {
        let url_ = "/api/services/app/ClauseRevision/getById";
        return this._commonhttp.get(url_, { id: id }).pipe(map(data => {
            return ClauseRevision.fromJS(data);
        }));
    }

    createOrUpdateRevisionAsync(input: any): Observable<any> {
        let url_ = "/api/services/app/ClauseRevision/CreateOrUpdate";
        let cla = { ClauseRevision: input };
        return this._commonhttp.post(url_, cla).pipe(map(data => {
            return data;
        }));
    }

    saveRevised(applyId: string, docId: string): Observable<any> {
        let url_ = "/api/services/app/ApplyInfo/ApplyRevisionAsync";
        var input = { documentId: docId, applyInfoId: applyId };
        return this._commonhttp.post(url_, input).pipe(map(data => {
            return data;
        }));
    }

    createOrUpdateDocRevisionAsync(input: any): Observable<any> {
        let url_ = "/api/services/app/DocRevision/CreateOrUpdate";
        let cat = { docRevision: input };
        return this._commonhttp.post(url_, cat).pipe(map(data => {
            return data;
        }));
    }

    getDocRevisionByIdAsync(id: any): Observable<DocRevision> {
        let url_ = "/api/services/app/DocRevision/getById";
        return this._commonhttp.get(url_, { id: id }).pipe(map(data => {
            return DocRevision.fromJS(data);
        }));
    }

    saveDraftDoc(applyId: string, docId: string): Observable<any> {
        let url_ = "/api/services/app/ApplyInfo/ApplyDraftDocAsync";
        var input = { documentId: docId, applyInfoId: applyId };
        return this._commonhttp.post(url_, input).pipe(map(data => {
            return data;
        }));
    }

    getPagedExamineByCurrentIdAsync(param: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/CriterionExamine/GetPagedExamineByCurrentIdAsync";
        return this._commonhttp.get(url_, param).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    getExamineDetailByCurrentIdAsync(params: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/ExamineDetail/GetExamineDetailByCurrentIdAsync";
        return this._commonhttp.get(url_, params).pipe(map(data => {
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

    uploadAttachment(input: any): Observable<any> {
        let url_ = "/api/services/app/DocAttachment/CreateOrUpdate";
        let att = { docAttachment: input };
        return this._commonhttp.post(url_, att).pipe(map(data => {
            return data;
        }));
    }

    deleteAttachmentByIdAsync(id: string): Observable<any> {
        let url_ = "/api/services/app/DocAttachment/Delete";
        return this._commonhttp.delete(url_, { id: id }).pipe(map(data => {
            return data;
        }));
    }

    getAttachmentListByIdAsync(params: any): Observable<Attachment[]> {
        let url_ = "/api/services/app/DocAttachment/GetAttachmentListByIdAsync";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            return Attachment.fromJSArray(data);
        }));
    }

    createExamineResultAsync(input: any): Observable<any> {
        let url_ = "/api/services/app/ExamineResult/CreateOrUpdate";
        let result = { examineResult: input };
        return this._commonhttp.post(url_, result).pipe(map(data => {
            return data;
        }));
    }

    getExamineResult(id: string): Observable<ExamineResult> {
        let url_ = "/api/services/app/ExamineResult/GetExamineResultByIdAsync";
        return this._commonhttp.get(url_, { id: id }).pipe(map(data => {
            return ExamineResult.fromJS(data);
        }));
    }

    createExamineFeedbackAsync(input: any): Observable<any> {
        let url_ = "/api/services/app/ExamineFeedback/CreateOrUpdate";
        let result = { examineFeedback: input };
        return this._commonhttp.post(url_, result).pipe(map(data => {
            return data;
        }));
    }

    getExamineFeedbackByIdAsync(id: any): Observable<ExamineFeedback> {
        let url_ = "/api/services/app/ExamineFeedback/GetExamineFeedbackByIdAsync";
        return this._commonhttp.get(url_, { id: id }).pipe(map(data => {
            return ExamineFeedback.fromJS(data);
        }));
    }

    getPagedCurrentIndicatorAsync(param: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/Indicator/GetPagedCurrentIndicatorAsync";
        return this._commonhttp.get(url_, param).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    getIndicatorByIdAsync(id: any): Observable<IndicatorShowDto> {
        let url_ = "/api/services/app/Indicator/GetIndicatorDetailByIdAsync";
        return this._commonhttp.get(url_, { id: id }).pipe(map(data => {
            return IndicatorShowDto.fromJS(data);
        }));
    }

    changeStatusByIdAsync(input: any): Observable<ApiResult> {
        let url_ = "/api/services/app/IndicatorsDetail/ChangeStatusByIdAsync";
        return this._commonhttp.post(url_, input).pipe(map(data => {
            return ApiResult.fromJS(data);
        }));
    }

    getClauseAttachmentsById(params: any): Observable<Attachment[]> {
        let url_ = "/api/services/app/DocAttachment/GetClauseAttachmentsByIdAsync";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            return Attachment.fromJSArray(data);
        }));
    }

    getPreviewDocByIdAsync(id: string): Observable<ApiResult> {
        let url_ = "/api/services/app/DocAttachment/GetPreviewDocByIdAsync";
        return this._commonhttp.get(url_, { id: id }).pipe(map(data => {
            return ApiResult.fromJS(data);
        }));
    }
}