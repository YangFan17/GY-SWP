export class Parameter {
    key: string;
    value: any;

    init(data?: any) {
        if (data) {
            this.key = data["key"];
            this.value = data["value"];
        }
    }

    static fromJS(data: any): Parameter {
        let result = new Parameter();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["key"] = this.key;
        data["value"] = this.value;

        return data;
    }
}