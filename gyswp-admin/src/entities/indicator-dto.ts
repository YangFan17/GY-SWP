export class IndicatorShowDto {
    id: string;
    title: string;
    paraphrase: string;
    measuringWay: string;
    // creationTime: Date;
    // creatorEmpeeId: string;
    // creatorEmpName: string;
    // creatorDeptId: number;
    creatorDeptName: string;
    // deptIds: string;
    // deptNames: string;
    expectedValue: number;
    cycleTime: number;
    cycleTimeName: string;
    actualValue: number;
    indicatorDetailId: string;
    statusName: string;
    status: number;
    achieveType: number;
    achieveTypeName: string;
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
            this.paraphrase = data["paraphrase"];
            this.measuringWay = data["measuringWay"];
            this.creatorDeptName = data["creatorDeptName"];
            this.expectedValue = data["expectedValue"];
            this.cycleTime = data["cycleTime"];
            this.cycleTimeName = data["cycleTimeName"];
            this.actualValue = data["actualValue"];
            this.indicatorDetailId = data["indicatorDetailId"];
            this.statusName = data["statusName"];
            this.status = data["status"];
            this.achieveType = data["achieveType"];
            this.achieveTypeName = data["achieveTypeName"];
        }
    }
    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["title"] = this.title;
        data["paraphrase"] = this.paraphrase;
        data["measuringWay"] = this.measuringWay;
        data["creatorDeptName"] = this.creatorDeptName;
        data["expectedValue"] = this.expectedValue;
        data["cycleTime"] = this.cycleTime;
        data["actualValue"] = this.actualValue;
        data["indicatorDetailId"] = this.indicatorDetailId;
        return data;
    }
    static fromJS(data: any): IndicatorShowDto {
        let result = new IndicatorShowDto();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): IndicatorShowDto[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new IndicatorShowDto();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new IndicatorShowDto();
        result.init(json);
        return result;
    }
}