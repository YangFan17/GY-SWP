export class DocumentDto {
    id: string;
    name: string;
    docNo: string;
    categoryId: number;
    categoryDesc: string;
    docThum: string;
    summary: string;
    publishTime: string;
    qrCodeUrl: string;
    deptIds: string;
    deptDesc: string;
    isDeleted: boolean;
    creationTime: Date;
    creatorUserId: string;
    lastModificationTime: Date;
    lastModifierUserId: string;
    deletionTime: Date;
    deleterUserId: string;
    employeeIds: string;
    employeeDes: string;
    isAllUser: boolean;
    deptName: string;
    isAction: boolean;
    stamps: string;
    suitableCode: string;
    bllId: string;
    constructor(data?: any) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    getUsers(): any[] {
        let users = [];
        if (!this.employeeIds) {
            this.employeeIds = '';
        }
        if (!this.employeeDes) {
            this.employeeDes = '';
        }
        let ids = this.employeeIds.split(',');
        let names = this.employeeDes.split(',');
        let i = 0;
        for (let id of ids) {
            if (id) {
                users.push({ id: id, name: names[i] });
            }
            i++;
        }
        return users;
    }

    getDepts(): any[] {
        let depts = [];
        if (!this.deptIds) {
            this.deptIds = '';
        }
        if (!this.deptDesc) {
            this.deptDesc = '';
        }
        let ids = this.deptIds.split(',');
        let names = this.deptDesc.split(',');
        let i = 0;
        for (let id of ids) {
            if (id) {
                depts.push({ id: id, name: names[i] });
            }
            i++;
        }
        return depts;
    }

    init(data?: any) {
        if (data) {
            this.id = data["id"];
            this.name = data["name"];
            this.docNo = data["docNo"];
            this.categoryId = data["categoryId"];
            this.categoryDesc = data["categoryDesc"];
            this.docThum = data["docThum"];
            this.summary = data["summary"];
            this.publishTime = data["publishTime"];
            this.qrCodeUrl = data["qrCodeUrl"];
            this.deptIds = data["deptIds"];
            this.deptDesc = data["deptDesc"];
            this.isDeleted = data["isDeleted"];
            this.creationTime = data["creationTime"];
            this.creatorUserId = data["creatorUserId"];
            this.lastModificationTime = data["lastModificationTime"];
            this.lastModifierUserId = data["lastModifierUserId"];
            this.deletionTime = data["deletionTime"];
            this.deleterUserId = data["deleterUserId"];
            this.employeeIds = data["employeeIds"];
            this.employeeDes = data["employeeDes"];
            this.isAllUser = data["isAllUser"];
            this.deptName = data["deptName"];
            this.isAction = data["isAction"];
            this.stamps = data["stamps"];
            this.suitableCode = data["suitableCode"];
            this.bllId = data["bllId"];
        }
    }
    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["name"] = this.name;
        data["docNo"] = this.docNo;
        data["categoryId"] = this.categoryId;
        data["categoryDesc"] = this.categoryDesc;
        data["docThum"] = this.docThum;
        data["summary"] = this.summary;
        data["publishTime"] = this.publishTime;
        data["qrCodeUrl"] = this.qrCodeUrl;
        data["deptIds"] = this.deptIds;
        data["deptDesc"] = this.deptDesc;
        data["isDeleted"] = this.isDeleted;
        data["creationTime"] = this.creationTime;
        data["creatorUserId"] = this.creatorUserId;
        data["lastModificationTime"] = this.lastModificationTime;
        data["lastModifierUserId"] = this.lastModifierUserId;
        data["deletionTime"] = this.deletionTime;
        data["deleterUserId"] = this.deleterUserId;
        data["employeeIds"] = this.employeeIds;
        data["employeeDes"] = this.employeeDes;
        data["isAllUser"] = this.isAllUser;
        data["isAction"] = this.isAction;
        data["stamps"] = this.stamps;
        data["suitableCode"] = this.suitableCode;
        data["bllId"] = this.bllId;
        return data;
    }
    static fromJS(data: any): DocumentDto {
        let result = new DocumentDto();
        result.init(data);
        return result;
    }
    static fromJSArray(dataArray: any[]): DocumentDto[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new DocumentDto();
            item.init(result);
            array.push(item);
        });
        return array;
    }
    clone() {
        const json = this.toJSON();
        let result = new DocumentDto();
        result.init(json);
        return result;
    }
}