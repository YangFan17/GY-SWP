export class DocRevision {
    id: string;
    name: string;
    categoryId: string;
    isDeleted: boolean;
    applyInfoId: string;
    employeeId: string;
    employeeName: string;
    revisionType: number;
    status: number;
    creationTime: Date;
    creatorUserId: number;
    lastModificationTime: Date;
    lastModifierUserId: number;
    deletionTime: Date;
    deleterUserId: number;
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
            this.name = data["name"];
            this.categoryId = data["categoryId"];
            this.applyInfoId = data["applyInfoId"];
            this.employeeId = data["employeeId"];
            this.employeeName = data["employeeName"];
            this.revisionType = data["revisionType"];
            this.status = data["status"];
            this.isDeleted = data["isDeleted"];
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
        data["name"] = this.name;
        data["categoryId"] = this.categoryId;
        data["applyInfoId"] = this.applyInfoId;
        data["employeeId"] = this.employeeId;
        data["employeeName"] = this.employeeName;
        data["revisionType"] = this.revisionType;
        data["status"] = this.status;
        data["isDeleted"] = this.isDeleted;
        data["creationTime"] = this.creationTime;
        data["creatorUserId"] = this.creatorUserId;
        data["lastModificationTime"] = this.lastModificationTime;
        data["lastModifierUserId"] = this.lastModifierUserId;
        data["deletionTime"] = this.deletionTime;
        data["deleterUserId"] = this.deleterUserId;
        return data;
    }
    static fromJS(data: any): DocRevision {
        let result = new DocRevision();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): DocRevision[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new DocRevision();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new DocRevision();
        result.init(json);
        return result;
    }
}