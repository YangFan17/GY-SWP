export class DocAttachment {
    id: string;
    type: number;
    name: string;
    fileType: number;
    fileSize: number;
    path: string;
    bLL: string;
    backUpId: string;
    isDeleted: boolean;
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
            this.type = data["type"];
            this.name = data["name"];
            this.fileType = data["fileType"];
            this.fileSize = data["fileSize"];
            this.path = data["path"];
            this.bLL = data["bLL"];
            this.backUpId = data["backUpId"];
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
        data["type"] = this.type;
        data["name"] = this.name;
        data["fileType"] = this.fileType;
        data["fileSize"] = this.fileSize;
        data["path"] = this.path;
        data["bLL"] = this.bLL;
        data["backUpId"] = this.backUpId;
        data["isDeleted"] = this.isDeleted;
        data["creationTime"] = this.creationTime;
        data["creatorUserId"] = this.creatorUserId;
        data["lastModificationTime"] = this.lastModificationTime;
        data["lastModifierUserId"] = this.lastModifierUserId;
        data["deletionTime"] = this.deletionTime;
        data["deleterUserId"] = this.deleterUserId;
        return data;
    }
    static fromJS(data: any): DocAttachment {
        let result = new DocAttachment();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): DocAttachment[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new DocAttachment();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new DocAttachment();
        result.init(json);
        return result;
    }
}

export class Attachment {
    id: string;
    name: string;
    fileSize: number;
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
            this.fileSize = data["fileSize"];
        }
    }
    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["name"] = this.name;
        data["fileSize"] = this.fileSize;
        return data;
    }
    static fromJS(data: any): Attachment {
        let result = new Attachment();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): Attachment[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new Attachment();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new Attachment();
        result.init(json);
        return result;
    }
}