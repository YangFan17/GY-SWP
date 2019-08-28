export class Indicators {
    id: string;
    title: string;
    paraphrase: string;
    measuringWay: string;
    creationTime: Date;
    creatorEmpeeId: string;
    creatorEmpName: string;
    creatorDeptId: number;
    creatorDeptName: string;
    deptIds: string;
    deptNames: string;
    expectedValue: number;
    cycleTime: number;
    cycleTimeName: string;
    achieveType: number = 1;
    sourceDocId: string;
    sourceDocName: string;
    endTime: Date;
    getDepts(): any[] {
        let depts = [];
        if (!this.deptIds) {
            this.deptIds = '';
        }
        if (!this.deptNames) {
            this.deptNames = '';
        }
        let ids = this.deptIds.split(',');
        let names = this.deptNames.split(',');
        let i = 0;
        for (let id of ids) {
            if (id) {
                depts.push({ id: id, name: names[i] });
            }
            i++;
        }
        return depts;
    }

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
            this.deptIds = data["deptIds"];
            this.deptNames = data["deptNames"];
            this.expectedValue = data["expectedValue"];
            this.cycleTime = data["cycleTime"];
            this.cycleTimeName = data["cycleTimeName"];
            this.achieveType = data["achieveType"];
            this.sourceDocId = data["sourceDocId"];
            this.sourceDocName = data["sourceDocName"];
            this.endTime = data["endTime"];
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
        data["deptIds"] = this.deptIds;
        data["deptNames"] = this.deptNames;
        data["expectedValue"] = this.expectedValue;
        data["cycleTime"] = this.cycleTime;
        data["achieveType"] = this.achieveType;
        data["sourceDocId"] = this.sourceDocId;
        data["endTime"] = this.endTime;
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