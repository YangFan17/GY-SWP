import { Inject, Optional, Injectable } from "@angular/core";
import { Observer, Observable } from "rxjs";
import { CommonHttpClient } from "services/common-httpclient";
import { map } from "rxjs/operators";
import { API_BASE_URL } from "@shared/service-proxies/service-proxies";
import { NzTreeNode } from "ng-zorro-antd";
import { Employee } from "entities";
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
}