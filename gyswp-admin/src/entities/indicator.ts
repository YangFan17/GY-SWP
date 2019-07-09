export class Indicators {
    id: string;
    title: string;
    paraphrase: string;
    measuringWay: string;
    creationTime: string;
    creatorEmpeeId: string;
    creatorEmpName: string;
    creatorDeptId: number;
    creatorDeptName: string;
    deptId: number;
    deptName: string;
    expectedValue: number;
    cycleTime: number;
    cycleTimeName: string;
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
            this.title = data["title"];
            this.paraphrase = data["paraphrase"];
            this.measuringWay = data["measuringWay"];
            this.creationTime = data["creationTime"];
            this.creatorEmpeeId = data["creatorEmpeeId"];
            this.creatorEmpName = data["creatorEmpName"];
            this.creatorDeptId = data["creatorDeptId"];
            this.creatorDeptName = data["creatorDeptName"];
            this.deptId = data["deptId"];
            this.deptName = data["deptName"];
            this.expectedValue = data["expectedValue"];
            this.cycleTime = data["cycleTime"];
            this.cycleTimeName = data["cycleTimeName"];
        }
    }
    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["title"] = this.title;
        data["paraphrase"] = this.paraphrase;
        data["measuringWay"] = this.measuringWay;
        data["creationTime"] = this.creationTime;
        data["creatorEmpeeId"] = this.creatorEmpeeId;
        data["creatorEmpName"] = this.creatorEmpName;
        data["creatorDeptId"] = this.creatorDeptId;
        data["creatorDeptName"] = this.creatorDeptName;
        data["deptId"] = this.deptId;
        data["deptName"] = this.deptName;
        data["expectedValue"] = this.expectedValue;
        data["cycleTime"] = this.cycleTime;
        return data;
    }
    static fromJS(data: any): Indicators {
        let result = new Indicators();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): Indicators[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new Indicators();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new Indicators();
        result.init(json);
        return result;
    }
}