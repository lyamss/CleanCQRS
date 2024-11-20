import { useCallback, useState } from "react";
import { AuthStatus } from "@/services/AuthStatus";
import { GetUserDto, SetLoginAndRegisterUserClassicDto } from "@/models/Users";
import { apiClient, ApiError } from "@/services/apiClient";
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
      await apiClient.FetchData<{message: string, result: GetUserDto}>("/users/GetAllUsers", {
        method: 'GET',
        headers: {
          'Authorization': `Bearer ${token}`
        }
      })
      .then((response) => {
        setUserAllDto(response.result);
      })
      .catch(() => {
        setUserAllDto(null);
      })
    }, []);


    const AuthRegister = useCallback(async (SetUserDto: SetLoginAndRegisterUserClassicDto) => {
      setIsLoadingLoginAndRegister(true);
        await apiClient.FetchData<{message: string, result: { User: any, "Token Session": { token: string, expirationDate: string } }}>("/Auth/register", { json: SetUserDto })
        .then((response) => {
          const token = response.result["Token Session"].token;
          localStorage.setItem('token', token);
          router.push("/");
        })
        .catch(error => {
          if (error instanceof ApiError) {
              setMessageApiAuth(error.message);
          }
        })
        .finally(() => {
          setIsLoadingLoginAndRegister(false);
        })
    }, [router]);


    const AuthLoginClassic = useCallback(async (SetLoginUserDto: SetLoginAndRegisterUserClassicDto) => {
      setIsLoadingLoginAndRegister(true);
        await apiClient.FetchData<{message: string, result: { User: any, "Token Session": { token: string, expirationDate: string } }}>("/Auth/Login", { json: SetLoginUserDto })
        .then((response) => {
          const token = response.result["Token Session"].token;
          localStorage.setItem('token', token);
          setMessageApiAuth(response.message);
          router.push("/");
        })
        .catch((response) => {
            if(response instanceof ApiError) 
            { setMessageApiAuth(response.message); }
        })
        .finally(() => {
          setIsLoadingLoginAndRegister(false);
        });
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