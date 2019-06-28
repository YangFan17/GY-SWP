export class ExamineFeedback {
    id: string;
    type: number;
    businessId: string;
    courseType: string;
    reason: string;
    solution: string;
    creationTime: Date;
    employeeId: string;
    employeeName: string;
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
            this.type = data["type"];
            this.businessId = data["businessId"];
            this.courseType = data["courseType"];
            this.reason = data["reason"];
            this.solution = data["solution"];
            this.creationTime = data["creationTime"];
            this.employeeId = data["employeeId"];
            this.employeeName = data["employeeName"];
        }
    }
    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["type"] = this.type;
        data["businessId"] = this.businessId;
        data["courseType"] = this.courseType;
        data["reason"] = this.reason;
        data["solution"] = this.solution;
        data["creationTime"] = this.creationTime;
        data["employeeId"] = this.employeeId;
        data["employeeName"] = this.employeeName;
        return data;
    }
    static fromJS(data: any): ExamineFeedback {
        let result = new ExamineFeedback();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): ExamineFeedback[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new ExamineFeedback();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new ExamineFeedback();
        result.init(json);
        return result;
    }
}