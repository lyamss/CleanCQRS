'use client'

import 'tailwindcss/tailwind.css';
import Loader1 from "@/components/Loading/Loader1"
import { GetItemsDto, GetUserDto } from '@/services/modelsDto/Users';
import { useEffect, useState } from 'react';
import Skeleton from '@mui/material/Skeleton';
import { UserIcon, CalendarIcon, AtSignIcon as AtSymbolIcon, ShoppingBagIcon } from 'lucide-react'
import { LoaderCustombg } from '@/components/ui/LoaderCustombg';
import { UseUser } from '@/services/UseUser';
import { AuthProvider } from '@/services/AuthProvider';
import { Pagination, Tab, Tabs } from '@mui/material';

const ITEMS_PER_PAGE = 6;

const HomePage = () => {

  const { UserAllDto, ItemsAllDto, AddItems } = UseUser();
  const [UserAllDtoInArray, SetUserAllDtoInArray] = useState<GetUserDto[] | null>(null);
  const [ItemsAllDtoInArray, SetItemsAllDtoInArray] = useState<GetItemsDto[] | null>(null);
  const [currentPage, setCurrentPage] = useState(1);
  const [activeTab, setActiveTab] = useState(0);

  useEffect(() => {
    if(Array.isArray(UserAllDto)) {
      SetUserAllDtoInArray(UserAllDto);
    }
  }, [UserAllDto]);

  useEffect(() => {
    if(Array.isArray(ItemsAllDto)) {
      SetItemsAllDtoInArray(ItemsAllDto);
    }
  }, [ItemsAllDto]);

  const handleChangePage = (event: React.ChangeEvent<unknown>, value: number) => {
    setCurrentPage(value);
  };

  const handleChangeTab = (event: React.SyntheticEvent, newValue: number) => {
    setActiveTab(newValue);
    setCurrentPage(1);
  };

  const getCurrentPageItems = () => {
    const startIndex = (currentPage - 1) * ITEMS_PER_PAGE;
    const endIndex = startIndex + ITEMS_PER_PAGE;
    return activeTab === 0
      ? UserAllDtoInArray?.slice(startIndex, endIndex)
      : ItemsAllDtoInArray?.slice(startIndex, endIndex);
  };

  const totalPages = Math.ceil((activeTab === 0 ? UserAllDtoInArray?.length : ItemsAllDtoInArray?.length) / ITEMS_PER_PAGE) || 1;

  return (
    <AuthProvider LoadingComponent={<Loader1/>} isProtected={true}>
      <div className="min-h-screen bg-gray-100 py-12 px-4 sm:px-6 lg:px-8">
        <div className="max-w-7xl mx-auto">
          <Tabs value={activeTab} onChange={handleChangeTab} centered className="mb-8">
            <Tab label="Users" />
            <Tab label="Items" />
          </Tabs>
          
          {(UserAllDtoInArray && ItemsAllDtoInArray) ? (
            <>
              <div className="grid grid-cols-1 gap-6 sm:grid-cols-2 lg:grid-cols-3">
                {getCurrentPageItems()?.map((item) => (
                  activeTab === 0 ? (
                    <UserCard key={(item as GetUserDto).id_User} user={item as GetUserDto} />
                  ) : (
                    <ItemCard key={(item as GetItemsDto).id_items} item={item as GetItemsDto} />
                  )
                ))}
              </div>
              <div className="mt-8 flex justify-center">
                <Pagination 
                  count={totalPages} 
                  page={currentPage} 
                  onChange={handleChangePage} 
                  color="primary" 
                />
              </div>
            </>
          ) : (
            <LoadingPagePrincipale />
          )}
        </div>
      </div>
    </AuthProvider>
  );
}

const UserCard = ({ user }: { user: GetUserDto }) => (
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
);

const ItemCard = ({ item }: { item: GetItemsDto }) => (
  <div key={item.id_items} className="bg-white overflow-hidden shadow rounded-lg">
    <div className="px-4 py-5 sm:p-6">
      <div className="flex items-center">
        <ShoppingBagIcon className="h-10 w-10 text-gray-400" />
        <div className="ml-5 w-0 flex-1">
          <dl>
            <dt className="text-sm font-medium text-gray-500 truncate">Item Name</dt>
            <dd className="text-lg font-bold text-gray-900">{item.name}</dd>
          </dl>
        </div>
      </div>
      <div className="mt-5">
        <p className="text-sm text-gray-500">{item.description}</p>
        <div className="mt-2 flex items-center">
          <span className="text-sm font-medium text-gray-900">${item.price.toFixed(2)}</span>
        </div>
        <div className="mt-2 flex items-center">
          <CalendarIcon className="h-5 w-5 text-gray-400" />
          <span className="ml-2 text-sm text-gray-500">
            {new Date(item.createdAt).toLocaleDateString()}
          </span>
        </div>
      </div>
    </div>
  </div>
);


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