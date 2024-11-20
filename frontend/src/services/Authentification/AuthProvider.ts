import { AuthStatus, UseAuth } from "@/services/Authentification/UseAuth";

type AuthRedirectProps = {
  children: React.ReactNode;
  LoadingComponent: React.ReactNode;
  isProtected: boolean
};  

const AuthProvider = ({ children, LoadingComponent, isProtected }: AuthRedirectProps) => {
    const status = UseAuth(isProtected);
  
    if (isProtected && status === AuthStatus.Authenticated) { return children; }

    else if(!isProtected && status === AuthStatus.Unauthenticated) { return children; }

    return LoadingComponent;
  }

export default AuthProvider;