export class PositionInfo {
    id: string;
    position: string;
    duties: string;
    employeeId: string;
    employeeName: string;
    creationTime: Date;
    creatorUserId: string;
    lastModificationTime: Date;
    lastModifierUserId: string;
    deletionTime: Date;
    deleterUserId: string;
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
            this.position = data["position"];
            this.duties = data["duties"];
            this.employeeId = data["employeeId"];
            this.employeeName = data["employeeName"];
            this.creationTime = data["creationTime"];
            this.creatorUserId = data["creatorUserId"];
            this.lastModificationTime = data["lastModificationTime"];
            this.lastModifierUserId = data["lastModifierUserId"];
            this.deletionTime = data["deletionTime"];
            this.deleterUserId = data["deleterUserId"];
        }
    }
    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["position"] = this.position;
        data["duties"] = this.duties;
        data["employeeId"] = this.employeeId;
        data["employeeName"] = this.employeeName;
        data["creationTime"] = this.creationTime;
        data["creatorUserId"] = this.creatorUserId;
        data["lastModificationTime"] = this.lastModificationTime;
        data["lastModifierUserId"] = this.lastModifierUserId;
        data["deletionTime"] = this.deletionTime;
        data["deleterUserId"] = this.deleterUserId;
        return data;
    }
    static fromJS(data: any): PositionInfo {
        let result = new PositionInfo();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): PositionInfo[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new PositionInfo();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new PositionInfo();
        result.init(json);
        return result;
    }
}