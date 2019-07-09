export class IndicatorsDetail {
    id: string;
    indicatorsId: string;
    actualValue: number;
    status: number;
    employeeId: string;
    employeeName: string;
    creationTime: Date;
    statusName: string;
    deptId: number;
    deptName: string;
    completeTime: Date;
    constructor(data?: any) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }
    init(data?: any) {
        if (data) {
            this.id = data["id"];
            this.indicatorsId = data["indicatorsId"];
            this.actualValue = data["actualValue"];
            this.status = data["status"];
            this.employeeId = data["employeeId"];
            this.employeeName = data["employeeName"];
            this.creationTime = data["creationTime"];
            this.statusName = data["statusName"];
            this.deptId = data["deptId"];
            this.deptName = data["deptName"];
            this.completeTime = data["completeTime"];
        }
    }
    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["indicatorsId"] = this.indicatorsId;
        data["actualValue"] = this.actualValue;
        data["status"] = this.status;
        data["employeeId"] = this.employeeId;
        data["employeeName"] = this.employeeName;
        data["creationTime"] = this.creationTime;
        data["deptId"] = this.deptId;
        data["deptName"] = this.deptName;
        data["completeTime"] = this.completeTime;
        return data;
    }
    static fromJS(data: any): IndicatorsDetail {
        let result = new IndicatorsDetail();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): IndicatorsDetail[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new IndicatorsDetail();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new IndicatorsDetail();
        result.init(json);
        return result;
    }
}