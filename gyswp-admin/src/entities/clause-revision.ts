export class ClauseRevision {
    id: string;
    clauseId: string;
    content: string;
    employeeId: string;
    employeeName: string;
    applyInfoId: string;
    status: number;
    revisionType: number;
    revisionTypeName: string;
    statusName: string;
    clauseNo: string;
    title: string;
    creatorUserId: string;
    creationTime: Date;
    lastModificationTime: Date;
    lastModifierUserId: string;
    deletionTime: Date;
    deleterUserId: string;
    documentId: string;
    isDeleted: boolean;
    parentId: string;
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
            this.clauseId = data["clauseId"];
            this.content = data["content"];
            this.employeeId = data["employeeId"];
            this.employeeName = data["employeeName"];
            this.creationTime = data["creationTime"];
            this.applyInfoId = data["applyInfoId"];
            this.status = data["status"];
            this.revisionType = data["revisionType"];
            this.revisionTypeName = data["revisionTypeName"];
            this.statusName = data["statusName"];
            this.clauseNo = data["clauseNo"];
            this.title = data["title"];
            this.creatorUserId = data["creatorUserId"];
            this.lastModificationTime = data["lastModificationTime"];
            this.lastModifierUserId = data["lastModifierUserId"];
            this.deletionTime = data["deletionTime"];
            this.deleterUserId = data["deleterUserId"];
            this.documentId = data["documentId"];
            this.isDeleted = data["isDeleted"];
            this.parentId = data["parentId"];
        }
    }
    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["clauseId"] = this.clauseId;
        data["content"] = this.content;
        data["employeeId"] = this.employeeId;
        data["employeeName"] = this.employeeName;
        data["creationTime"] = this.creationTime;
        data["applyInfoId"] = this.applyInfoId;
        data["status"] = this.status;
        data["revisionType"] = this.revisionType;
        data["clauseNo"] = this.clauseNo;
        data["title"] = this.title;
        data["creatorUserId"] = this.creatorUserId;
        data["lastModificationTime"] = this.lastModificationTime;
        data["lastModifierUserId"] = this.lastModifierUserId;
        data["deletionTime"] = this.deletionTime;
        data["deleterUserId"] = this.deleterUserId;
        data["documentId"] = this.documentId;
        data["isDeleted"] = this.isDeleted;
        data["parentId"] = this.parentId;
        return data;
    }
    static fromJS(data: any): ClauseRevision {
        let result = new ClauseRevision();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): ClauseRevision[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new ClauseRevision();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new ClauseRevision();
        result.init(json);
        return result;
    }
}