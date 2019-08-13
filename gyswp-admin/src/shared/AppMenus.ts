import { Menu } from '@delon/theme';
import { type } from 'os';

// 全局的左侧导航菜单
export class AppMenus {
    // new
    static Menus: Menu[] = [
        {
            text: "主页",// 无本地化显示字符
            //i18n: "HomePage",// 本地化主键(ABP本地化)
            acl: "",// 权限
            reuse: false,
            icon: { type: "icon", value: "home" },// 图标
            link: "/app/home", // url 地址
            // hide: true,  // 强制隐藏
            // ...还有更多选项，请查看 Menu成员
        },
        {
            text: "基础数据",
            icon: { type: "icon", value: "dingding" },
            reuse: false,
            acl: ["QiGuanAdmin", "Admin", "StandardAdmin"],// 权限
            link: "",
            group: true,
            children: [
                {
                    text: "组织架构",
                    link: "/app/basic/organization",
                    acl: ["QiGuanAdmin", "Admin"],// 权限
                    reuse: false
                },
                {
                    text: "标准管理",
                    link: "/app/basic/document",
                    reuse: false
                },
            ]
        },
        // {
        //     text: "我的标准",
        //     icon: { type: "icon", value: "read" },
        //     reuse: false,
        //     link: "/app/criterion/criterion",
        //     group: true,
        //     children: [
        //     ]
        // },
        {
            text: "工作中心",
            icon: { type: "icon", value: "laptop" },
            reuse: false,
            link: "",
            group: true,
            children: [
                {
                    text: "我的标准",
                    link: "/app/criterion/criterion",
                    reuse: false
                },
                {
                    text: "标准考核",
                    link: "/app/criterion/my-examine",
                    reuse: false
                },
                {
                    text: "指标考核",
                    link: "/app/criterion/my-indicator",
                    reuse: false
                }
            ]
        },
        {
            text: "监督检查",
            icon: { type: "icon", value: "audit" },
            reuse: false,
            acl: ["QiGuanAdmin", "Admin", "CountyAdmin"],// 权限
            link: "",
            group: true,
            children: [
                {
                    text: "标准监督",
                    link: "/app/supervision/supervision",
                    reuse: false
                },
                {
                    text: "考核指标",
                    link: "/app/supervision/indicators",
                    reuse: false
                },
            ]
        },
        {
            text: "合理化建议",
            icon: { type: "icon", value: "form" },
            reuse: false,
            link: "/app/advises/advises",
            group: true,
            children: [
            ]
        },
        {
            text: "数据统计",
            icon: { type: "icon", value: "bar-chart" },
            reuse: false,
            acl: ["QiGuanAdmin", "Admin"],// 权限
            link: "",
            group: true,
            children: [
                {
                    text: "标准统计",
                    link: "/app/reports/standardrevision",
                    reuse: false
                },
                {
                    text: "自查统计",
                    link: "/app/reports/inspect",
                    reuse: false
                },
                {
                    text: "监督统计",
                    link: "/app/reports/supervise",
                    reuse: false
                },
                {
                    text: "指标统计",
                    link: "/app/reports/indicator-supervise",
                    reuse: false
                },
                {
                    text: "合理化建议统计",
                    link: "/app/reports/adviseReport",
                    reuse: false
                },
            ]
        },
        {
            text: "物流中心",
            icon: { type: "icon", value: "control" },
            reuse: false,
            acl: ["LogisticsEmployee", "Admin"],// 权限
            link: "",
            group: true,
            children: [
                {
                    text: "入库作业",
                    link: "/app/logistic/in-storage",
                    reuse: false
                },
                {
                    text: "在库保管",
                    link: "/app/logistic/storage-custody",
                    reuse: false
                },
                {
                    text: "出库分拣",
                    link: "/app/logistic/out-storage-classify",
                    reuse: false
                },
                {
                    text: "领货出库",
                    link: "/app/logistic/out-storage",
                    reuse: false
                }
            ]
        },
        {
            text: "配置管理",
            icon: { type: "icon", value: "tool" },
            link: "",
            group: true,
            acl: "Admin",// 权限
            children: [
                {
                    text: "数据配置",
                    link: "/app/config/data-config",
                    reuse: false
                },
            ]
        },
        {
            text: "系统管理",
            icon: { type: "icon", value: "setting" },
            link: "/app/system",
            group: true,
            acl: "Admin",// 权限
            children: [
                {
                    text: "用户管理",
                    link: "/app/system/users",
                    reuse: false
                },
                {
                    text: "角色管理",
                    link: "/app/system/roles",
                    reuse: false
                }
            ]
        }
    ];
}