import Link from "next/link";
import { useState } from "react";


export const LikAllPages = () => {

  const [isLoadingRegister, setIsLoadingRegister] = useState(false);
const [isLoadingLogin, setIsLoadingLogin] = useState(false);
const [isLoadingWelcome, setIsLoadingWelcome] = useState(false);

  const LinkRedirectLogin = () => {
    setIsLoadingLogin(true);
    return (
        <Link href="/Login">
        </Link>
    );
  }


  const LinkRedirectWelcome = () => {
    setIsLoadingWelcome(true);
    return (
        <Link href="/Welcome">
        </Link>
    );
  }
  
  
const LinkRedirectRegister = () => {
  setIsLoadingRegister(true);
    return (
        <Link href="/register">
        </Link>
    );
  }

  return {
    LinkRedirectLogin,
    LinkRedirectRegister,
    isLoadingRegister,
    isLoadingLogin,
    isLoadingWelcome,
    LinkRedirectWelcome
  }

}