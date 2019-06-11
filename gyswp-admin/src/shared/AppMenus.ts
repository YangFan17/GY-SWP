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
        {
            text: "我的标准",
            icon: { type: "icon", value: "read" },
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