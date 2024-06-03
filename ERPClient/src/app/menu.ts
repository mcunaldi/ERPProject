export class MenuModel{
    name: string = "";
    icon: string = "";
    url: string = "";
    isTitle: boolean = false;
    subMenus: MenuModel[] = []; 
}

export const Menus: MenuModel[] = [
    {
        name: "Ana Sayfa",
        icon: "fas fa-solid fa-home",
        url: "/",
        isTitle: false,
        subMenus: []
    },
    {
        name: "Ana Grup",
        icon: "far fa-solid fa-trowel-bricks",
        url: "",
        isTitle : false,
        subMenus: [
            {
                name: "Müşteriler",
                icon: "far fa-solid fa-users",
                url: "/customers",
                isTitle: false,
                subMenus: []
            },
            {
                name: "Depo",
                icon: "far fa-solid fa-warehouse",
                url: "/depo",
                isTitle: false,
                subMenus: []
            }
        ]
    }
]