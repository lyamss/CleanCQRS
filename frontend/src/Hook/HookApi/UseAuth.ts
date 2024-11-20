import { useCallback, useState } from "react";
import { AuthStatus } from "@/services/AuthStatus";
import { GetUserDto, SetLoginAndRegisterUserClassicDto } from "@/models/Users";
import { apiClient } from "@/services/apiClient";
import { useRouter } from 'next/navigation';

export const UseAuth = () =>
{
    const [UserAllDto, setUserAllDto] = useState<GetUserDto | null>(null);
    const [MessageApiAuth, setMessageApiAuth] = useState<string | null>(null);
    const [isLoadingLoginAndRegister, setIsLoadingLoginAndRegister] = useState(false);
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

    const UserGetAlls = useCallback(async () => {
      const token = localStorage.getItem('token');
      const r = await apiClient.FetchData("/users/GetAllUsers", {
        method: 'GET',
        headers: {
          'Authorization': `Bearer ${token}`
        }
      })

      if(!r.ok)
      {
        setUserAllDto(null);
      }

      if(r.ok)
      {
        const response = await r.json();
        setUserAllDto(response.result);
      }
    }, []);


    const AuthRegister = useCallback(async (SetUserDto: SetLoginAndRegisterUserClassicDto) => {
      setIsLoadingLoginAndRegister(true);
        const r = await apiClient.FetchData("/Auth/register", { json: SetUserDto })
        const response = await r.json();
        if(!r.ok)
        {
          setMessageApiAuth(response.message);
        }

        if(r.ok)
        {
          setMessageApiAuth(response.message);
          const token = response.result["Token Session"].token;
          localStorage.setItem('token', token);
          router.push("/");
        }

        setIsLoadingLoginAndRegister(false);
    }, [router]);


    const AuthLoginClassic = useCallback(async (SetUserDto: SetLoginAndRegisterUserClassicDto) => {
      setIsLoadingLoginAndRegister(true);
        const r = await apiClient.FetchData("/Auth/Login", { json: SetUserDto })
        const response = await r.json();
        if(!r.ok)
        {
          setMessageApiAuth(response.message);
        }

        if(r.ok)
        {
          setMessageApiAuth(response.message);
          const token = response.result["Token Session"].token;
          localStorage.setItem('token', token);
          router.push("/");
        }

        setIsLoadingLoginAndRegister(false);
    }, [router]);



    return {
        AuthRegister,
        MessageApiAuth,
        status,
        AuthLoginClassic,
        isLoadingLoginAndRegister,
        UserAllDto,
        UserGetAlls,
    }
}