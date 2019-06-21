export class Clause {
    id: string;
    parentId: string;
    type: number;
    content: string;
    clauseNo: string;
    title: string;
    documentId: string;
    hasAttchment: boolean;
    creationTime: Date;
    creatorUserId: string;
    lastModificationTime: Date;
    lastModifierUserId: string;
    deletionTime: Date;
    deleterUserId: string;
    bLLId: string;

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
            this.parentId = data["parentId"];
            this.type = data["type"];
            this.documentId = data["documentId"];
            this.content = data["content"];
            this.clauseNo = data["clauseNo"];
            this.title = data["title"];
            this.hasAttchment = data["hasAttchment"];
            this.creationTime = data["creationTime"];
            this.creatorUserId = data["creatorUserId"];
            this.lastModificationTime = data["lastModificationTime"];
            this.lastModifierUserId = data["lastModifierUserId"];
            this.deletionTime = data["deletionTime"];
            this.deleterUserId = data["deleterUserId"];
            this.bLLId = data["bLLId"];
        }
    }
    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["parentId"] = this.parentId;
        data["type"] = this.type;
        data["documentId"] = this.documentId;
        data["creationTime"] = this.creationTime;
        data["creatorUserId"] = this.creatorUserId;
        data["content"] = this.content;
        data["title"] = this.title;
        data["hasAttchment"] = this.hasAttchment;
        data["clauseNo"] = this.clauseNo;
        data["lastModificationTime"] = this.lastModificationTime;
        data["lastModifierUserId"] = this.lastModifierUserId;
        data["deletionTime"] = this.deletionTime;
        data["deleterUserId"] = this.deleterUserId;
        data["bLLId"] = this.bLLId;
        return data;
    }
    static fromJS(data: any): Clause {
        let result = new Clause();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): Clause[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new Clause();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new Clause();
        result.init(json);
        return result;
    }
}