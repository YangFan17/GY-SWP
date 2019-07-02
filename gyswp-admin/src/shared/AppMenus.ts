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
            icon: { type: "icon", value: "home" },// 图标
            link: "/app/home", // url 地址
            // hide: true,  // 强制隐藏
            // ...还有更多选项，请查看 Menu成员
        },
        {
            text: "基础数据",
            icon: { type: "icon", value: "dingding" },
            reuse: false,
            link: "/app/basic",
            group: true,
            children: [
                {
                    text: "组织架构",
                    link: "/app/basic/organization",
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
            link: "/app/criterion/criterion",
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
            ]
        },
        {
            text: "监督检查",
            icon: { type: "icon", value: "audit" },
            reuse: false,
            link: "/app/supervision",
            group: true,
            children: [
                {
                    text: "标准监督",
                    link: "/app/supervision/supervision",
                    reuse: false
                },
                {
                    text: "考核指标",
                    link: "/app/supervision/supervision1",
                    reuse: false
                },
            ]
        },
        {
            text: "合理化建议",
            icon: { type: "icon", value: "form" },
            reuse: false,
            link: "/app/criterion/criterion",
            group: true,
            children: [
            ]
        },
        {
            text: "数据统计",
            icon: { type: "icon", value: "bar-chart" },
            reuse: false,
            link: "/app/criterion/criterion",
            group: true,
            children: [
                {
                    text: "自查统计",
                    link: "/app/reports/inspect",
                    reuse: false
                },
                {
                    text: "标准统计",
                    link: "/app/reports/standardrevision",
                    reuse: false
                },
            ]
        },
        {
            text: "设备管理",
            icon: { type: "icon", value: "control" },
            reuse: false,
            link: "/app/criterion/criterion",
            group: true,
            children: [
            ]
        },
        {
            text: "配置管理",
            icon: { type: "icon", value: "tool" },
            link: "/app/config",
            group: true,
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