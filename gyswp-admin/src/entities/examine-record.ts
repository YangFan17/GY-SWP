export class ExamineRecord {
    id: string;
    documentName: string;
    clauseInfo: string;
    status: number;
    result: number;
    employeeName: string;
    checked: boolean;
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
            this.documentName = data["documentName"];
            this.clauseInfo = data["clauseInfo"];
            this.status = data["status"];
            this.employeeName = data["employeeName"];
            this.result = data["result"];
            this.checked = data["checked"];
        }
    }
    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["documentName"] = this.documentName;
        data["clauseInfo"] = this.clauseInfo;
        data["status"] = this.status;
        data["employeeName"] = this.employeeName;
        return data;
    }
    static fromJS(data: any): ExamineRecord {
        let result = new ExamineRecord();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): ExamineRecord[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new ExamineRecord();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new ExamineRecord();
        result.init(json);
        return result;
    }
}