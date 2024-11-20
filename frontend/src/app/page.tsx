'use client'

import 'tailwindcss/tailwind.css';
import AuthProvider from "@/services/Authentification/AuthProvider"
import Loader1 from "@/components/Loading/Loader1"
import { GetUserDto } from '@/services/modelsDto/Users';
import { useEffect, useState } from 'react';
import Skeleton from '@mui/material/Skeleton';
import { UserIcon, CalendarIcon, AtSignIcon as AtSymbolIcon } from 'lucide-react'
import { LoaderCustombg } from '@/components/ui/LoaderCustombg';
import { UseUser } from '@/services/UseUser';

const HomePage = () => {

  const { UserAllDto, UserGetAlls } = UseUser();

useEffect(() => {
  UserGetAlls()
}, [UserGetAlls])

const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const timer = setTimeout(() => {
      setIsLoading(false);
    }, 5000);

    return () => clearTimeout(timer);
  }, []);

  return (
    <>
    <AuthProvider
    LoadingComponent={<Loader1/>}
    isProtected={true}
    >
        {isLoading ? 
        
          <LoadingPagePrincipale />  :   
              
         <VuePage getAllUsers={UserAllDto || []}/>
        }
    </AuthProvider>
    </>
  );
}


const VuePage = ({ getAllUsers }: { getAllUsers: GetUserDto[] }) => {
  return (
    <div className="min-h-screen bg-gray-100 py-12 px-4 sm:px-6 lg:px-8">
      <div className="max-w-7xl mx-auto">
        <h1 className="text-3xl font-extrabold text-gray-900 text-center mb-8">User List</h1>
        {getAllUsers.length > 0 ? (
          <div className="grid grid-cols-1 gap-6 sm:grid-cols-2 lg:grid-cols-3">
            {getAllUsers.map((user) => (
              <div key={user.id_User} className="bg-white overflow-hidden shadow rounded-lg">
                <div className="px-4 py-5 sm:p-6">
                  <div className="flex items-center">
                  <UserIcon className="h-10 w-10 text-gray-400" />
                    <div className="ml-5 w-0 flex-1">
                      <dl>
                        <dt className="text-sm font-medium text-gray-500 truncate">User ID</dt>
                        <dd className="text-lg font-bold text-gray-900">{user.id_User}</dd>
                      </dl>
                    </div>
                  </div>
                  <div className="mt-5">
                    <div className="flex items-center">
                    <AtSymbolIcon className="h-5 w-5 text-gray-400" />
                      <span className="ml-2 text-sm text-gray-500">{user.email}</span>
                    </div>
                    <div className="mt-2 flex items-center">
                      <CalendarIcon className="h-5 w-5 text-gray-400" />
                      <span className="ml-2 text-sm text-gray-500">
                        {new Date(user.accountCreatedAt).toLocaleDateString()}
                      </span>
                    </div>
                  </div>
                </div>
              </div>
            ))}
          </div>
        ) : (
          <>
            <LoadingPagePrincipale/>
          </>
        )}
      </div>
    </div>
  )
}

const LoadingPagePrincipale = () =>
{
  return (
    <div className="min-h-screen bg-gray-100 py-12 px-4 sm:px-6 lg:px-8">
      <div className="max-w-7xl mx-auto">
    <div className='items-center'>
     <LoaderCustombg/>
    </div>
  <div className="flex items-stretch space-x-4">
    <Skeleton variant="rectangular" className='py-4' width={410} height={218} />
    <Skeleton variant="rectangular" className='py-12' width={410} height={218} />
    <Skeleton variant="rectangular" className='py-8' width={410} height={218} />
  </div>
  </div>
  </div>
  );
}

export default HomePage