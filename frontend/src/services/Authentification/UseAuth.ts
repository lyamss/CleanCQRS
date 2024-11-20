import { UseUser } from "@/services/UseUser";
import React from "react";
import { useRouter } from 'next/navigation'


export enum AuthStatus
{
    Authenticated,
    Unauthenticated
}

export const UseAuth = (isProtected: boolean) =>
{

  const { UserGetAlls, UserAllDto } = UseUser();
  const router = useRouter();


  let status: AuthStatus;

  switch (UserAllDto) 
  {
      case null:
        status = AuthStatus.Unauthenticated;
        break;
      default:
        status = AuthStatus.Authenticated;
        break;
  }

  React.useEffect(() => {
    UserGetAlls();
  }, [UserGetAlls]);

  React.useEffect(() => {
      let timer: NodeJS.Timeout | undefined;
      if (isProtected && status === AuthStatus.Unauthenticated) {
        timer = setTimeout(() => {
          router.push("/Login");
        }, 3000);
      } if (!isProtected && status === AuthStatus.Authenticated) {
        timer = setTimeout(() => {
          router.push("/");
        }, 3000);
      }
      return () => clearTimeout(timer);
    }, [status, isProtected, router]);


    return status
}