export class Advise {
    id: string;
    adviseName: string;
    currentSituation: string;
    solution: number;
    isAdoption: boolean;
    employeeId: string;
    employeeName: string;
    creationTime: Date;
    deptId: number;
    deptName: string;
    reviewOpinion: string;
    approvalComments: string;
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
            this.adviseName = data["adviseName"];
            this.currentSituation = data["currentSituation"];
            this.solution = data["solution"];
            this.isAdoption = data["isAdoption"];
            this.employeeId = data["employeeId"];
            this.employeeName = data["employeeName"];
            this.creationTime = data["creationTime"];
            this.deptId = data["deptId"];
            this.deptName = data["deptName"];
            this.reviewOpinion = data["reviewOpinion"];
            this.approvalComments = data["approvalComments"];
        }
    }
    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["adviseName"] = this.adviseName;
        data["currentSituation"] = this.currentSituation;
        data["solution"] = this.solution;
        data["isAdoption"] = this.isAdoption;
        data["employeeId"] = this.employeeId;
        data["employeeName"] = this.employeeName;
        data["creationTime"] = this.creationTime;
        data["deptId"] = this.deptId;
        data["deptName"] = this.deptName;
        data["reviewOpinion"] = this.reviewOpinion;
        data["approvalComments"] = this.approvalComments;
        return data;
    }
    static fromJS(data: any): Advise {
        let result = new Advise();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): Advise[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new Advise();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new Advise();
        result.init(json);
        return result;
    }
}