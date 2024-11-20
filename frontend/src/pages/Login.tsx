'use client'

import * as React from 'react';
import ButtonLoading1 from "@/components/button/ButtonLoading1";
import Loader1 from "@/components/Loading/Loader1";
import { SetLoginAndRegisterUserClassicDto } from "@/services/modelsDto/Users";
import AuthProvider from "@/services/Authentification/AuthProvider";
import { useState } from "react";
import { Lock, AtSignIcon as AtSymbolIcon } from 'lucide-react'
import Snackbar from '@mui/material/Snackbar';
import { UseUser } from '@/services/UseUser';

const LoginPage = () =>
{
    return (
        <>
            <AuthProvider
            LoadingComponent={<Loader1 />} 
            isProtected={false}>
                <VuePage/>
            </AuthProvider>
        </>
    );
}















const VuePage = () =>
{
  const { isLoadingLoginAndRegister, AuthLoginClassic, MessageApiAuth } = UseUser();
  const [open, setOpen] = React.useState(false);

  React.useEffect(() => {
    if (MessageApiAuth) {
      setOpen(true);
      const timer = setTimeout(() => {
        setOpen(false);
      }, 5000);

      return () => clearTimeout(timer);
    }
  }, [MessageApiAuth]);

    const [formData, setFormData] = useState<SetLoginAndRegisterUserClassicDto>({
      password: "",
      email: "",
    })

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
      setFormData({ ...formData, [e.target.name]: e.target.value })
    }

    const handleLogin = () => {
        AuthLoginClassic(formData);
      };

      return (
        <div className="min-h-screen flex items-center justify-center bg-gray-100">
          <div className="bg-white p-8 rounded-xl shadow-2xl w-full max-w-md">
            <h2 className="text-3xl font-bold mb-6 text-center text-gray-800">Login</h2>
            <div className="space-y-4">
              <div className="relative">
                <AtSymbolIcon className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 h-5 w-5" />
                <input
                  type="email"
                  name='email'
                  placeholder="Email"
                  value={formData.email} 
                  onChange={handleInputChange}
                  required
                  className="w-full pl-10 pr-4 py-2 border border-gray-300 rounded-xl focus:outline-none focus:border-b-slate-800"
                />
              </div>
              <div className="relative">
                <Lock className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 h-5 w-5" />
                <input
                  type="password"
                  name='password'
                  placeholder="Password"
                  value={formData.password} 
                  onChange={handleInputChange}
                  required
                  className="w-full pl-10 pr-4 py-2 border border-gray-300 rounded-xl focus:outline-none focus:border-b-slate-800"
                />
              </div>
            </div>
            <div className="mt-6">
              <ButtonLoading1
                classCSS="w-full rounded-xl group inline-flex items-center justify-center bg-black text-white hover:bg-gray-800 transition-colors duration-300 font-roboto py-2 px-4 sm:py-3 sm:px-6 text-sm sm:text-base"
                nameButton="Se connecter"
                onClick={handleLogin}
                isLoading={isLoadingLoginAndRegister}
              />
            </div>
            <p className="mt-4 text-center text-sm text-gray-600">
              {"Pas de compte? "}
              <a href="#" className="font-medium text-black hover:underline">
                Inscription
              </a>
            </p>
          </div>
          <Snackbar
              open={open}
              autoHideDuration={5000}
              message={MessageApiAuth}
            />
        </div>
      )
}

export default LoginPage;