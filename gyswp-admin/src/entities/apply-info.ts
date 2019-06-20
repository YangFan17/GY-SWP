export class ApplyInfo {
    id: string;
    documentId: string;
    type: number;
    employeeId: string;
    employeeName: string;
    creationTime: Date;
    status: number;
    handleTime: Date;
    reason: string;
    content: string;
    processInstanceId: string;
    operateType: number;
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
            this.documentId = data["documentId"];
            this.type = data["type"];
            this.employeeId = data["employeeId"];
            this.employeeName = data["employeeName"];
            this.creationTime = data["creationTime"];
            this.status = data["status"];
            this.handleTime = data["handleTime"];
            this.reason = data["reason"];
            this.content = data["content"];
            this.processInstanceId = data["processInstanceId"];
            this.operateType = data["operateType"];
        }
    }
    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["documentId"] = this.documentId;
        data["type"] = this.type;
        data["employeeId"] = this.employeeId;
        data["employeeName"] = this.employeeName;
        data["creationTime"] = this.creationTime;
        data["status"] = this.status;
        data["handleTime"] = this.handleTime;
        data["reason"] = this.reason;
        data["content"] = this.content;
        data["processInstanceId"] = this.processInstanceId;
        data["operateType"] = this.operateType;
        return data;
    }
    static fromJS(data: any): ApplyInfo {
        let result = new ApplyInfo();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): ApplyInfo[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new ApplyInfo();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new ApplyInfo();
        result.init(json);
        return result;
    }
}