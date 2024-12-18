import { UseUser } from "@/services/UseUser";
import { useRouter } from 'next/navigation'
import { useEffect } from "react";


enum AuthStatus
{
    Authenticated,
    Unauthenticated
}

type AuthRedirectProps = 
{
  children: React.ReactNode;
  LoadingComponent: React.ReactNode;
  isProtected: boolean
};

export const AuthProvider = ({ children, LoadingComponent, isProtected }: AuthRedirectProps) =>
{

  const { UserAllDto } = UseUser();
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


  useEffect(() => {
    let timer: NodeJS.Timeout | undefined;
    if (isProtected && status === AuthStatus.Unauthenticated) 
    {
        timer = setTimeout(() => {
            router.push("/Login");
        }, 3000);
    }
    if (!isProtected && status === AuthStatus.Authenticated) 
    {
        timer = setTimeout(() => {
            router.push("/");
        }, 3000);
    }
    return () => clearTimeout(timer);
}, [isProtected, status, router]);
    

  if (isProtected && status === AuthStatus.Authenticated) { return children; }

  else if(!isProtected && status === AuthStatus.Unauthenticated) { return children; }

  return LoadingComponent;

}