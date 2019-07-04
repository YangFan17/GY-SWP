export class SystemData {
    id: string;
    modelId: number;
    type: number;
    code: string;
    desc: string;
    remark: string;
    seq: number;
    creationTime: Date;
    modelName: string;
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
            this.modelId = data["modelId"];
            this.type = data["type"];
            this.code = data["code"];
            this.desc = data["desc"];
            this.remark = data["remark"];
            this.seq = data["seq"];
            this.creationTime = data["creationTime"];
            this.modelName = data["modelName"];
        }
    }
    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["modelId"] = this.modelId;
        data["type"] = this.type;
        data["code"] = this.code;
        data["desc"] = this.desc;
        data["remark"] = this.remark;
        data["seq"] = this.seq;
        data["creationTime"] = this.creationTime;
        data["modelName"] = this.modelName;
        return data;
    }
    static fromJS(data: any): SystemData {
        let result = new SystemData();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): SystemData[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new SystemData();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new SystemData();
        result.init(json);
        return result;
    }
}