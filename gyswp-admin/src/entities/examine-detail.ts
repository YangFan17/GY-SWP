export class ExamineDetail {
    id: string;
    criterionExamineId: string;
    clauseId: string;
    status: number;
    result: number;
    employeeId: string;
    employeeName: string;
    creationTime: Date;
    creatorEmpeeId: string;
    creatorEmpName: string;
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
            this.criterionExamineId = data["criterionExamineId"];
            this.clauseId = data["clauseId"];
            this.status = data["status"];
            this.result = data["result"];
            this.employeeId = data["employeeId"];
            this.employeeName = data["employeeName"];
            this.creationTime = data["creationTime"];
            this.creatorEmpeeId = data["creatorEmpeeId"];
            this.creatorEmpName = data["creatorEmpName"];
        }
    }
    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["criterionExamineId"] = this.criterionExamineId;
        data["clauseId"] = this.clauseId;
        data["status"] = this.status;
        data["result"] = this.result;
        data["employeeId"] = this.employeeId;
        data["employeeName"] = this.employeeName;
        data["creationTime"] = this.creationTime;
        data["creatorEmpeeId"] = this.creatorEmpeeId;
        data["creatorEmpName"] = this.creatorEmpName;
        return data;
    }
    static fromJS(data: any): ExamineDetail {
        let result = new ExamineDetail();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): ExamineDetail[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new ExamineDetail();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new ExamineDetail();
        result.init(json);
        return result;
    }
}