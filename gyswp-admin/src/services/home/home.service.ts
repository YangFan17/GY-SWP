import { Inject, Optional, Injectable } from "@angular/core";
import { Observer, Observable } from "rxjs";
import { CommonHttpClient } from "services/common-httpclient";
import { map } from "rxjs/operators";
import { PagedResultDto } from "@shared/component-base";
import { PositionInfo, ApiResult, MainPointsRecord } from "entities";

@Injectable()
export class HomeService {
    private _commonhttp: CommonHttpClient;

    constructor(@Inject(CommonHttpClient) commonhttp: CommonHttpClient) {
        this._commonhttp = commonhttp;
    }

    getPositionListByCurrentUser(): Observable<PositionInfo[]> {
        let url_ = "/api/services/app/PositionInfo/GetPositionListByCurrentUserAsync";
        return this._commonhttp.get(url_).pipe(map(data => {
            return PositionInfo.fromJSArray(data);
        }));
    }

    getPositionInfoById(id: any): Observable<PositionInfo> {
        let url_ = "/api/services/app/PositionInfo/getById";
        return this._commonhttp.get(url_, { id: id }).pipe(map(data => {
            return PositionInfo.fromJS(data);
        }));
    }

    createPositionInfo(input: any): Observable<ApiResult> {
        let url_ = "/api/services/app/PositionInfo/CreateOrUpdate";
        let posInfo = { positionInfo: input }
        return this._commonhttp.post(url_, posInfo).pipe(map(data => {
            return ApiResult.fromJS(data);
        }));
    }

    getCategoryDocByCurrentUser(): Observable<any[]> {
        let url_ = "/api/services/app/PositionInfo/GetHomeCategoryOptionsAsync";
        return this._commonhttp.get(url_).pipe(map(data => {
            return data;
        }));
    }

    createMainPointRecord(input: MainPointsRecord): Observable<ApiResult> {
        let url_ = "/api/services/app/MainPointsRecord/CreateMainPointRecordAsync";
        return this._commonhttp.post(url_, input).pipe(map(data => {
            return ApiResult.fromJS(data);
        }));
    }


    getPositionTreeByIdAsync(): Observable<any> {
        let url_ = "/api/services/app/PositionInfo/GetPositionTreeByIdAsync";
        return this._commonhttp.get(url_).pipe(map(data => {
            return data;
        }));
    }

    getCurrentPositionAsync(): Observable<string> {
        let url_ = "/api/services/app/PositionInfo/GetCurrentPositionAsync";
        return this._commonhttp.get(url_).pipe(map(data => {
            return data;
        }));
    }

    deletePositionById(id: string): Observable<any> {
        let url_ = "/api/services/app/PositionInfo/Delete";
        var param = { id: id };
        return this._commonhttp.delete(url_, param).pipe(map(data => {
            return data;
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
}
