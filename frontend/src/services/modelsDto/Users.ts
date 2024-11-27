export type GetUserDto = 
{
    id_User: string;
    email: string;
    accountCreatedAt: Date;
}


export type GetItemsDto = 
{
    name: string;
    description: string,
    price: number,
    id_items: string,
    createdAt: Date
}


export type SetLoginAndRegisterUserClassicDto =
{
    email: string;
    password: string;
}