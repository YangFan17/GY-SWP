import { Inject, Optional, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CommonHttpClient } from "services/common-httpclient";
import { map } from "rxjs/operators";
import { ApiResult, DocumentDto, Clause, SelectGroup, ClauseRevision, DocRevision, CriterionExamine, ExamineRecord, Attachment, ExamineResult, ExamineFeedback } from "entities";
import { PagedResultDto } from "@shared/component-base";
import { API_BASE_URL } from "@shared/service-proxies/service-proxies";

@Injectable()
export class InspectService {
    private _commonhttp: CommonHttpClient;
    baseUrl: string;

    constructor(@Inject(CommonHttpClient) commonhttp: CommonHttpClient
        , @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this._commonhttp = commonhttp;
        this.baseUrl = baseUrl ? baseUrl : "";
    }
}