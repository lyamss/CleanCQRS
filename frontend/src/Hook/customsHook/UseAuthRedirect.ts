import { UseAuth } from "@/Hook/HookApi/UseAuth";
import { useEffect } from 'react';
import { AuthStatus } from "@/services/AuthStatus";
import { useRouter } from 'next/navigation'


export const UseAuthRedirect = (isProtected: boolean) =>
{
    const { status, UserGetAlls } = UseAuth();
    const router = useRouter();

    useEffect(() => {
      UserGetAlls();
    }, [UserGetAlls]);

    useEffect(() => {
        let timer: NodeJS.Timeout | undefined;
        if (isProtected && status === AuthStatus.Unauthenticated) {
          timer = setTimeout(() => {
            router.push("/Login");
          }, 3000);
        } else if (!isProtected && status === AuthStatus.Authenticated) {
          timer = setTimeout(() => {
            router.push("/");
          }, 3000);
        }
        return () => clearTimeout(timer);
      }, [status, isProtected, router]);

    return status;
}