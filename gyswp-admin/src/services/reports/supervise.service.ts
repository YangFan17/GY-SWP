import { Inject, Optional, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CommonHttpClient } from "services/common-httpclient";
import { map } from "rxjs/operators";
import { ApiResult, DocumentDto, Clause, SelectGroup, ClauseRevision, DocRevision, CriterionExamine, ExamineRecord, Attachment, ExamineResult, ExamineFeedback } from "entities";
import { PagedResultDto } from "@shared/component-base";
import { API_BASE_URL } from "@shared/service-proxies/service-proxies";

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
}