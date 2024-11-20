import { useCallback, useEffect, useState } from "react";
import { GetUserDto, SetLoginAndRegisterUserClassicDto } from "@/services/modelsDto/Users";
import { useRouter } from 'next/navigation';
import { apiClient } from "@/services/OtherTool/apiClient";

export const UseUser = () =>
{
    const [UserAllDto, setUserAllDto] = useState<GetUserDto | null>(null);
    const [MessageApiAuth, setMessageApiAuth] = useState<string | null>(null);
    const [isLoadingApiFetchLoginAndRegister, setisLoadingApiFetchLoginAndRegister] = useState(false);
    const router = useRouter();

    const UserGetAlls = useCallback(async () => {
        const r = await apiClient.FetchData("/users/GetAllUsers")
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
        setisLoadingApiFetchLoginAndRegister(true);
          const r = await apiClient.FetchData("/Auth/register", { json: SetUserDto })
          const response = await r.json();
          if(!r.ok)
          {
            setMessageApiAuth(response.message);
          }
          if(r.ok)
          {
            setMessageApiAuth(response.message);
            router.push("/");
          }
          setisLoadingApiFetchLoginAndRegister(false);
      }, [router]);
  
  
      const AuthLoginClassic = useCallback(async (SetUserDto: SetLoginAndRegisterUserClassicDto) => {
        setisLoadingApiFetchLoginAndRegister(true);
          const r = await apiClient.FetchData("/Auth/Login", { json: SetUserDto })
          const response = await r.json();
          if(!r.ok)
          {
            setMessageApiAuth(response.message);
          }
          if(r.ok)
          {
            setMessageApiAuth(response.message);
            router.push("/");
          }
          setisLoadingApiFetchLoginAndRegister(false);
      }, [router]);


      useEffect(() => {
        UserGetAlls()
      }, [UserGetAlls])

      return {
        AuthRegister,
        MessageApiAuth,
        AuthLoginClassic,
        isLoadingApiFetchLoginAndRegister,
        UserAllDto,
    }
}