export type GetUserDto = 
{
    id_User: string;
    email: string;
    accountCreatedAt: Date;
}


export type SetLoginAndRegisterUserClassicDto =
{
    email: string;
    password: string;
}