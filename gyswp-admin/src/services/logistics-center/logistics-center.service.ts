import { Inject, Optional, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CommonHttpClient } from "services/common-httpclient";
import { map } from "rxjs/operators";
import { ApiResult, DocumentDto, Clause, SelectGroup, ClauseRevision, DocRevision, CriterionExamine, ExamineRecord, Attachment, ExamineResult, ExamineFeedback } from "entities";
import { PagedResultDto } from "@shared/component-base";
import { API_BASE_URL } from "@shared/service-proxies/service-proxies";
import { NzTreeNode } from "ng-zorro-antd";

@Injectable()
export class LogisticService {
    private _commonhttp: CommonHttpClient;
    baseUrl: string;

    constructor(@Inject(CommonHttpClient) commonhttp: CommonHttpClient
        , @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this._commonhttp = commonhttp;
        this.baseUrl = baseUrl ? baseUrl : "";
    }

    //分页获取入库记录
    getPagedInStorageRecordAsync(param: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/LC_InStorageRecord/GetPaged";
        return this._commonhttp.get(url_, param).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    //分页获取入库验收质量记录
    getPagedQualityRecordAsync(param: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/LC_QualityRecord/GetPaged";
        return this._commonhttp.get(url_, param).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    //分页获取到货凭证记录
    getPagedInStorageBillAsync(param: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/LC_InStorageBill/GetPaged";
        return this._commonhttp.get(url_, param).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    //分页获取入库扫码记录
    getPagedInStorageScanAsync(param: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/LC_ScanRecord/GetPaged";
        return this._commonhttp.get(url_, param).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    exportInstoreRecord(param: any): Observable<ApiResult> {
        var _url = '/api/services/app/LC_InStorageRecord/ExportInStorageRecord';
        return this._commonhttp.post(_url, param).pipe(map(data => {
            return ApiResult.fromJS(data);
        }));
    }

    exportQualityRecord(param: any): Observable<ApiResult> {
        var _url = '/api/services/app/LC_QualityRecord/ExportQualityRecord';
        return this._commonhttp.post(_url, param).pipe(map(data => {
            return ApiResult.fromJS(data);
        }));
    }

    ///在库保管
    //

    ///出库分拣
    //卷烟分拣领用出库单
    getPagedUseOutStorageAsync(param: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/LC_UseOutStorage/GetPaged";
        return this._commonhttp.get(url_, param).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }
    //导出卷烟分拣领用出库单
    exportUseOutStorage(param: any): Observable<ApiResult> {
        var _url = '/api/services/app/LC_UseOutStorage/ExportUseOutStorage';
        return this._commonhttp.post(_url, param).pipe(map(data => {
            return ApiResult.fromJS(data);
        }));
    }
    //残损卷烟调换
    getPagedCigaretExchangeAsync(param: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/LC_CigaretExchange/GetPaged";
        return this._commonhttp.get(url_, param).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }
    //导出残损卷烟调换表
    exportCigaretExchange(param: any): Observable<ApiResult> {
        var _url = '/api/services/app/LC_CigaretExchange/ExportCigaretExchange';
        return this._commonhttp.post(_url, param).pipe(map(data => {
            return ApiResult.fromJS(data);
        }));
    }

    ///出库扫码
    //分页获取出库扫码记录
    getPagedOutStorageScanAsync(param: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/LC_OutScanRecord/GetPaged";
        return this._commonhttp.get(url_, param).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }
    //导出出库扫码记录表
    exportOutScanRecord(param: any): Observable<ApiResult> {
        var _url = '/api/services/app/LC_OutScanRecord/ExportOutScanRecord';
        return this._commonhttp.post(_url, param).pipe(map(data => {
            return ApiResult.fromJS(data);
        }));
    }

    getPagedConveyorCheckAsync(param: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/LC_ConveyorCheck/GetPaged";
        return this._commonhttp.get(url_, param).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    exportConveyorChecksRecord(param: any): Observable<ApiResult> {
        var _url = '/api/services/app/LC_ConveyorCheck/ExportConveyorChecksRecord';
        return this._commonhttp.post(_url, param).pipe(map(data => {
            return ApiResult.fromJS(data);
        }));
    }
}