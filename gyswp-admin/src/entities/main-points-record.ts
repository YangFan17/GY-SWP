export class MainPointsRecord {
    id: string;
    positionInfoId: string;
    documentId: string;
    mainPoint: string;
    creationTime: Date;
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
            this.positionInfoId = data["positionInfoId"];
            this.documentId = data["documentId"];
            this.mainPoint = data["mainPoint"];
            this.creationTime = data["creationTime"];
        }
    }
    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["positionInfoId"] = this.positionInfoId;
        data["documentId"] = this.documentId;
        data["mainPoint"] = this.mainPoint;
        data["creationTime"] = this.creationTime;
        return data;
    }
    static fromJS(data: any): MainPointsRecord {
        let result = new MainPointsRecord();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): MainPointsRecord[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new MainPointsRecord();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new MainPointsRecord();
        result.init(json);
        return result;
    }
}