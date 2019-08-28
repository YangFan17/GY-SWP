export class CriterionExamine {
    id: string;
    title: string;
    type: number;
    creationTime: Date;
    creatorEmpeeId: string;
    creatorEmpName: string;
    creatorDeptId: number;
    deptId: number;
    creatorDeptName: string;
    deptName: string;
    typeName: string;
    endTime: Date;
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
            this.type = data["type"];
            this.creationTime = data["creationTime"];
            this.creatorEmpeeId = data["creatorEmpeeId"];
            this.creatorEmpName = data["creatorEmpName"];
            this.creatorDeptId = data["creatorDeptId"];
            this.deptId = data["deptId"];
            this.creatorDeptName = data["creatorDeptName"];
            this.deptName = data["deptName"];
            this.typeName = data["typeName"];
            this.endTime = data["endTime"];
        }
    }
    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["title"] = this.title;
        data["type"] = this.type;
        data["creationTime"] = this.creationTime;
        data["creatorEmpeeId"] = this.creatorEmpeeId;
        data["creatorEmpName"] = this.creatorEmpName;
        data["creatorDeptId"] = this.creatorDeptId;
        data["deptId"] = this.deptId;
        data["creatorDeptName"] = this.creatorDeptName;
        data["deptName"] = this.deptName;
        return data;
    }
    static fromJS(data: any): CriterionExamine {
        let result = new CriterionExamine();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): CriterionExamine[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new CriterionExamine();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new CriterionExamine();
        result.init(json);
        return result;
    }
}