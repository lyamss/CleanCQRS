import { AuthStatus } from "./AuthStatus";
import { UseAuthRedirect } from "@/Hook/customsHook/UseAuthRedirect";

type AuthRedirectProps = {
    children: React.ReactNode;
    LoadingComponent: React.ReactNode;
    isProtected: boolean
};  

const AuthRedirect = ({ children, LoadingComponent, isProtected }: AuthRedirectProps) => {
    const status = UseAuthRedirect(isProtected);
  
    if (isProtected && status === AuthStatus.Authenticated) { return children; }

    else if(!isProtected && status === AuthStatus.Unauthenticated) { return children; }

    return LoadingComponent;
  }

export default AuthRedirect;