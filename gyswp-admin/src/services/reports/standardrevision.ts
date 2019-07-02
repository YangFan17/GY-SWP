import { Inject, Optional, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CommonHttpClient } from "services/common-httpclient";
import { map } from "rxjs/operators";
import { API_BASE_URL } from "@shared/service-proxies/service-proxies";

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
}