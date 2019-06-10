export class ClauseTree {
    id: string;
    parentId: string;
    content: string;
    clauseNo: string;
    title: string;
    expand: boolean;
    level: number;
    children?: ClauseTree[];
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
            this.parentId = data["parentId"];
            this.content = data["content"];
            this.clauseNo = data["clauseNo"];
            this.title = data["title"];
            this.expand = data["expand"];
            this.level = data["level"];
        }
    }
    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["parentId"] = this.parentId;
        data["content"] = this.content;
        data["clauseNo"] = this.clauseNo;
        data["title"] = this.title;
        data["expand"] = this.expand;
        data["level"] = this.level;
        return data;
    }
    static fromJS(data: any): ClauseTree {
        let result = new ClauseTree();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): ClauseTree[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new ClauseTree();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new ClauseTree();
        result.init(json);
        return result;
    }
}