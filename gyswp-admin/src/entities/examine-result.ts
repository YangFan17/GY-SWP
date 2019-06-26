export class ExamineResult {
    id: string;
    examineDetailId: string;
    content: string;
    employeeId: string;
    employeeName: string;
    creationTime: Date;

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
            this.examineDetailId = data["examineDetailId"];
            this.content = data["content"];
            this.employeeId = data["employeeId"];
            this.employeeName = data["employeeName"];
            this.creationTime = data["creationTime"];
        }
    }
    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["examineDetailId"] = this.examineDetailId;
        data["content"] = this.content;
        data["employeeId"] = this.employeeId;
        data["employeeName"] = this.employeeName;
        data["creationTime"] = this.creationTime;
        return data;
    }
    static fromJS(data: any): ExamineResult {
        let result = new ExamineResult();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): ExamineResult[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new ExamineResult();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new ExamineResult();
        result.init(json);
        return result;
    }
}