export class Category {
    id: number;
    name: string;
    parentId: number;
    desc: string;
    parentName: string;
    deptId: number;

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
            this.parentId = data["parentId"];
            this.desc = data["desc"];
            this.parentName = data["parentName"];
            this.deptId = data["deptId"];
        }
    }

    static fromJS(data: any): Category {
        let result = new Category();
        result.init(data);
        return result;
    }

    static fromJSArray(dataArray: any[]): Category[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new Category();
            item.init(result);
            array.push(item);
        });

        return array;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["name"] = this.name;
        data["parentId"] = this.parentId;
        data["desc"] = this.desc;
        data["parentName"] = this.parentName;
        data["deptId"] = this.deptId;
        return data;
    }

    clone() {
        const json = this.toJSON();
        let result = new Category();
        result.init(json);
        return result;
    }
}

export class SelectGroup {
    text: string;
    value: string;
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
            this.text = data["text"];
            this.value = data["value"];
        }
    }

    static fromJS(data: any): SelectGroup {
        let result = new SelectGroup();
        result.init(data);
        return result;
    }

    static fromJSArray(dataArray: any[]): SelectGroup[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new SelectGroup();
            item.init(result);
            array.push(item);
        });

        return array;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["text"] = this.text;
        data["value"] = this.value;
        return data;
    }
}